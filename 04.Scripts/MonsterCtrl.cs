using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq; //LINQ 사용


public class MonsterCtrl : LivingEntity
{
    public class Target
    {
        public GameObject targetGameobject { get; set; }
        public float targetDistance { get; set; }
    }

    public LayerMask whatIstarget; //추적 대상 레이어

    private LivingEntity targetEntity;//추적 대상
    private NavMeshAgent navMeshAgent;//경로 계산 AI

    List<Target> targets = new List<Target>();

    //public ParticleSystem hitEffect;//피격시 재생할 파티클
    //public AudioClip deathSound;//사망시 재생할 소리
    //public AudioClip hitSound;//피격시 재생할 소리

    //private Animator mosterAnimator;//애니메이터 컴포넌트
    //private AudioSource monsterAudioPlayer;//오디오 소스 컴포넌트
    private Renderer mosterRenderer;//렌더러 컴포넌트

    public float damage = 20f;//공격력
    public float timeBetAttack = 0.5f;//공격 간격
    private float lastAttackTime;//마지막 공격 시점



    private void Awake()
    {
        // 게임 오브젝트로부터 사용할 컴포넌트 가져오기
        navMeshAgent = GetComponent<NavMeshAgent>();
        //monsterAnimator = GetComponent<Animator>();  애니메이터, 지금 없음
        //monsterAudioPlayer = GetComponent<AudioSource>();   오디오 플레이어, 지금 없음

        //렌더러 컴포넌트는 자식 오브젝트에 있으므로 GetComponentInChildren 사용
        mosterRenderer = GetComponentInChildren<Renderer>();

    }
    //좀비 AI의 초기 스펙을 결정하는 셋업 메서드  아직 MonsterData 안만듬
    /*
     public void Setup(MosterData monsterData)
    {
        startingHealth = mosterData.health; //체력 설정
        health = mosterData.health; //체력 설정
        
        damage = mosterData.damage; //공격력 설정
        pathMeshAgent.speed = mosterData.speed;//이동속도 설정

        //렌더러가 사용 중인 머테리얼의 컬러를 변경, 외형 색이 변함,  우리 프로젝트엔 필요 없는 부분일듯함
        mosterRenderer.material.color = monsterData.skinColor;
    }
    */


    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(UpdatePath());

        navMeshAgent.enabled = false;
        navMeshAgent.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //추적 대상의 존재 여부에 따라 다른 애니메이션 재생
        //monsterAnimator.SetBool("HasTarget", hasTarget);
    }
    //@TODO : hi
    private IEnumerator UpdatePath()
    {
        while (!dead)
        {
            if (hasTarget)//추적 대상이 존재
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);
            }
            else
            {
                navMeshAgent.isStopped = true;

                //1000유닛의 반지름을 가진 가상의 구를 그렸을 때 구와 겹치는 모든 콜라이더를 가져옴
                //단, whatIsTarget 레이어를 가진 콜라이더만 가져오도록 필터링
                targets.Clear();
                Collider[] colliders = Physics.OverlapSphere(transform.position, 1000f, whatIstarget);

                //모든 콜라이더를 순회하면서 살아 있는 LivingEntity 찾기
                for (int i = 0; i < colliders.Length; i++)
                {
                    GameObject target = colliders[i].gameObject;

                    float dstToTarget = Vector3.Distance(transform.position, target.transform.position); //타겟과의 거리 계산

                    targets.Add(new Target() { targetGameobject = target, targetDistance = dstToTarget });
                }
                var result = from sortDistance in targets orderby sortDistance.targetDistance select sortDistance;

                LivingEntity livingEntity = null;
                foreach (Target aa in result)
                {
                    livingEntity = aa.targetGameobject.GetComponent<LivingEntity>();
                    break;
                }


                if (livingEntity != null && !livingEntity.dead)
                {
                    targetEntity = livingEntity; //발견한 대상을 추적하는 코드인데 변경이 필요함
                                                 //break;
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    private bool hasTarget//추적 대상이 존재하는지 알려주는 프로퍼티
    {
        get
        {
            if (targetEntity != null && !targetEntity.dead)
            {
                return true;
            }
            return false;
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!dead)
        {
            //공격받은 지점과 방향으로 파티클 효과 재생
            //hitEffect.transform.position = hitPoint;
            //hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            //hitEffect.PLay();

            //mosterAudioPlayer.PlayOneShot(hitSound); //피격 효과음 재생
        }

        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();

        //자신의 모든 콜라이더 비활성화
        Collider[] mosterColliders = GetComponents<Collider>();
        for (int i = 0; i < mosterColliders.Length; i++)
        {
            mosterColliders[i].enabled = false;
        }

        //추적 중지, 내비메시 비활성화
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        //사망 애니메이션 재생
        //mosterAnimator.Settrigger("Die");
        //사망 효과음 재생
        //mosterAudioPlayer.PlayOneShot(deathSound);

    }

    //트리거 충돌한 상대방 게임 오브젝트가 추적 대상이라면 공격 실행
    private void OnTriggerStay(Collider other)
    {
        // 상대방의 livingentity 가져옴
        LivingEntity attackTarget = other.GetComponent<LivingEntity>();

        //추적 대상이면
        if (attackTarget != null && attackTarget == targetEntity)
        {
            navMeshAgent.isStopped = true;

            //자신이 사망하지 않았고 공격 딜레이가 지났으면 공격
            if (!dead && Time.time >= lastAttackTime + timeBetAttack)
            {
                lastAttackTime = Time.time;

                //상대방의 피격 위치와 피격 방향을 근삿값으로 계산
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitnomal = transform.position - other.transform.position;

                //공격 실행
                attackTarget.OnDamage(damage, hitPoint, hitnomal);

            }
        }
    }
}
