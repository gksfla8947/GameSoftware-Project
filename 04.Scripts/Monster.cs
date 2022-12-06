using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Monster : LivingEntity
{
    private GameObject Hair;
    private GameObject Player;
    private Slider HP_Slider; //민

    private float DistHair; //머리카락과의 거리
    private float DistPlayer; //플레이어와의 거리
    private float DistTarget; //타겟과의 거리


    public float attackRange = 5; //공격 사정거리
    public GameObject bulletPrefab;


    private LivingEntity targetEntity;//추적 대상
    private NavMeshAgent navMeshAgent;//경로 계산 AI

    //public AudioClip deathSound;//사망시 재생할 소리
    public AudioClip hitSound;//민 피격시 재생할 소리
    private AudioSource monsterhit;//민 오디오 소스 컴포넌트

    private Animator monsterAnimator;//애니메이터 컴포넌트
    private Renderer mosterRenderer;//렌더러 컴포넌트

    public float damage = 20f;//공격력
    public float timeBetAttack = 0.5f;//공격 간격
    private float lastAttackTime;//마지막 공격 시점


    private void Awake()
    {
        // 게임 오브젝트로부터 사용할 컴포넌트 가져오기
        navMeshAgent = GetComponent<NavMeshAgent>();
        monsterAnimator = GetComponent<Animator>();
        //monsterAudioPlayer = GetComponent<AudioSource>();   오디오 플레이어, 지금 없음

        //렌더러 컴포넌트는 자식 오브젝트에 있으므로 GetComponentInChildren 사용
        mosterRenderer = GetComponentInChildren<Renderer>();
        monsterhit = GetComponent<AudioSource>(); //민
    }

    // Start is called before the first frame update
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Hair = GameObject.FindGameObjectWithTag("Hair");
        StartCoroutine(UpdatePath());
        HP_Slider = GetComponentInChildren<Slider>(); //민
    }

    // Update is called once per frame
    void Update()
    {
        HP_Slider.value = curHealth / health;//민
    }
    private IEnumerator UpdatePath()
    {
        while (!dead)
        {
            monsterAnimator.CrossFade("Walk", 0f);

            DistHair = Vector3.Distance(transform.position, Hair.transform.position);
            DistPlayer = Vector3.Distance(transform.position, Player.transform.position);

            if (DistPlayer < DistHair) //머리카락보다 플레이어가 가까우면
            {
                targetEntity = Player.GetComponent<LivingEntity>(); //타겟 = 플레이어
                DistTarget = DistPlayer; //타겟과의 거리 = 플레이어와의 거리
            }
            else
            {
                targetEntity = Hair.GetComponent<LivingEntity>();
                DistTarget = DistHair;
            }
            if (DistTarget <= attackRange) //타겟과의 거리가 공격범위 이하이면 공격
            {
                navMeshAgent.isStopped = true;
                attack(targetEntity);
            }
            else //아니면 타겟을 향해 이동
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        monsterhit.PlayOneShot(hitSound);
        monsterAnimator.CrossFade("Damage", 0f);

        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        //자신의 모든 콜라이더 비활성화
        Collider[] mosterColliders = GetComponents<Collider>();
        for (int i = 0; i < mosterColliders.Length; i++)
        {
            mosterColliders[i].enabled = false;
        }
        if (GameManager.instance.IsWaveStart)
        {
            GameManager.instance.KillCount = GameManager.instance.KillCount + 1;
        }
        //추적 중지, 내비메시 비활성화
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        base.Die();

    }

    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
    }

    private void attack(LivingEntity target)
    {
        monsterAnimator.CrossFade("Hit", 0f);
        //자신이 사망하지 않았고 공격 딜레이가 지났으면 공격
        if (!dead && Time.time >= lastAttackTime + timeBetAttack)
        {
            lastAttackTime = Time.time;

            // GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Vector3 curPos = transform.position;
            curPos.y += 1f;
            GameObject bullet = Instantiate(bulletPrefab, curPos, Quaternion.identity);
            bullet.transform.LookAt(targetEntity.transform);

            MeleeAttack temp = bullet.GetComponent<MeleeAttack>();

            if (temp != null)
            {
                temp.damage = damage;
            }

            Bullet temp2 = bullet.GetComponent<Bullet>();

            if (temp2 != null)
            {
                temp2.damage = damage;
            }


        }
    }
}
