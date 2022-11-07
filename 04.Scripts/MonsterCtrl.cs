using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq; //LINQ ���


public class MonsterCtrl : LivingEntity
{
    public class Target
    {
        public GameObject targetGameobject { get; set; }
        public float targetDistance { get; set; }
    }

    public LayerMask whatIstarget; //���� ��� ���̾�

    private LivingEntity targetEntity;//���� ���
    private NavMeshAgent navMeshAgent;//��� ��� AI

    List<Target> targets = new List<Target>();

    //public ParticleSystem hitEffect;//�ǰݽ� ����� ��ƼŬ
    //public AudioClip deathSound;//����� ����� �Ҹ�
    //public AudioClip hitSound;//�ǰݽ� ����� �Ҹ�

    //private Animator mosterAnimator;//�ִϸ����� ������Ʈ
    //private AudioSource monsterAudioPlayer;//����� �ҽ� ������Ʈ
    private Renderer mosterRenderer;//������ ������Ʈ

    public float damage = 20f;//���ݷ�
    public float timeBetAttack = 0.5f;//���� ����
    private float lastAttackTime;//������ ���� ����



    private void Awake()
    {
        // ���� ������Ʈ�κ��� ����� ������Ʈ ��������
        navMeshAgent = GetComponent<NavMeshAgent>();
        //monsterAnimator = GetComponent<Animator>();  �ִϸ�����, ���� ����
        //monsterAudioPlayer = GetComponent<AudioSource>();   ����� �÷��̾�, ���� ����

        //������ ������Ʈ�� �ڽ� ������Ʈ�� �����Ƿ� GetComponentInChildren ���
        mosterRenderer = GetComponentInChildren<Renderer>();

    }
    //���� AI�� �ʱ� ������ �����ϴ� �¾� �޼���  ���� MonsterData �ȸ���
    /*
     public void Setup(MosterData monsterData)
    {
        startingHealth = mosterData.health; //ü�� ����
        health = mosterData.health; //ü�� ����
        
        damage = mosterData.damage; //���ݷ� ����
        pathMeshAgent.speed = mosterData.speed;//�̵��ӵ� ����

        //�������� ��� ���� ���׸����� �÷��� ����, ���� ���� ����,  �츮 ������Ʈ�� �ʿ� ���� �κ��ϵ���
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
        //���� ����� ���� ���ο� ���� �ٸ� �ִϸ��̼� ���
        //monsterAnimator.SetBool("HasTarget", hasTarget);
    }
    //@TODO : hi
    private IEnumerator UpdatePath()
    {
        while (!dead)
        {
            if (hasTarget)//���� ����� ����
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);
            }
            else
            {
                navMeshAgent.isStopped = true;

                //1000������ �������� ���� ������ ���� �׷��� �� ���� ��ġ�� ��� �ݶ��̴��� ������
                //��, whatIsTarget ���̾ ���� �ݶ��̴��� ���������� ���͸�
                targets.Clear();
                Collider[] colliders = Physics.OverlapSphere(transform.position, 1000f, whatIstarget);

                //��� �ݶ��̴��� ��ȸ�ϸ鼭 ��� �ִ� LivingEntity ã��
                for (int i = 0; i < colliders.Length; i++)
                {
                    GameObject target = colliders[i].gameObject;

                    float dstToTarget = Vector3.Distance(transform.position, target.transform.position); //Ÿ�ٰ��� �Ÿ� ���

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
                    targetEntity = livingEntity; //�߰��� ����� �����ϴ� �ڵ��ε� ������ �ʿ���
                                                 //break;
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    private bool hasTarget//���� ����� �����ϴ��� �˷��ִ� ������Ƽ
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
            //���ݹ��� ������ �������� ��ƼŬ ȿ�� ���
            //hitEffect.transform.position = hitPoint;
            //hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            //hitEffect.PLay();

            //mosterAudioPlayer.PlayOneShot(hitSound); //�ǰ� ȿ���� ���
        }

        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();

        //�ڽ��� ��� �ݶ��̴� ��Ȱ��ȭ
        Collider[] mosterColliders = GetComponents<Collider>();
        for (int i = 0; i < mosterColliders.Length; i++)
        {
            mosterColliders[i].enabled = false;
        }

        //���� ����, ����޽� ��Ȱ��ȭ
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        //��� �ִϸ��̼� ���
        //mosterAnimator.Settrigger("Die");
        //��� ȿ���� ���
        //mosterAudioPlayer.PlayOneShot(deathSound);

    }

    //Ʈ���� �浹�� ���� ���� ������Ʈ�� ���� ����̶�� ���� ����
    private void OnTriggerStay(Collider other)
    {
        // ������ livingentity ������
        LivingEntity attackTarget = other.GetComponent<LivingEntity>();

        //���� ����̸�
        if (attackTarget != null && attackTarget == targetEntity)
        {
            navMeshAgent.isStopped = true;

            //�ڽ��� ������� �ʾҰ� ���� �����̰� �������� ����
            if (!dead && Time.time >= lastAttackTime + timeBetAttack)
            {
                lastAttackTime = Time.time;

                //������ �ǰ� ��ġ�� �ǰ� ������ �ٻ����� ���
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitnomal = transform.position - other.transform.position;

                //���� ����
                attackTarget.OnDamage(damage, hitPoint, hitnomal);

            }
        }
    }
}
