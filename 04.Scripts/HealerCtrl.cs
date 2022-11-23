using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq; //LINQ ���


public class HealerCtrl : LivingEntity
{
    private GameObject Hair;
    private GameObject Player;

    private float DistHair; //�Ӹ�ī������ �Ÿ�
    private float DistPlayer; //�÷��̾���� �Ÿ�
    private float DistTarget; //Ÿ�ٰ��� �Ÿ�


    public float attackRange = 5; //���� �����Ÿ�


    private LivingEntity targetEntity;//���� ���
    private NavMeshAgent navMeshAgent;//��� ��� AI

    public LayerMask Monster;

    public float recoveryAmount = 1; //����


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
        Monster = LayerMask.GetMask("Monster");
    }


    // Start is called before the first frame update
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Hair = GameObject.FindGameObjectWithTag("Hair");
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        //���� ����� ���� ���ο� ���� �ٸ� �ִϸ��̼� ���
        //monsterAnimator.SetBool("HasTarget", hasTarget);
    }
    private IEnumerator UpdatePath()
    {
        while (!dead)
        {
            DistHair = Vector3.Distance(transform.position, Hair.transform.position);
            DistPlayer = Vector3.Distance(transform.position, Player.transform.position);

            if (DistPlayer < DistHair) //�Ӹ�ī������ �÷��̾ ������
            {
                targetEntity = Player.GetComponent<LivingEntity>(); //Ÿ�� = �÷��̾�
                DistTarget = DistPlayer; //Ÿ�ٰ��� �Ÿ� = �÷��̾���� �Ÿ�
            }
            else
            {
                targetEntity = Hair.GetComponent<LivingEntity>();
                DistTarget = DistHair;
            }

            if (DistTarget <= attackRange) //Ÿ�ٰ��� �Ÿ��� ���ݹ��� �����̸� ����
            {
                navMeshAgent.isStopped = true;
                heal();
            }
            else //�ƴϸ� Ÿ���� ���� �̵�
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);
            }

            yield return new WaitForSeconds(0.25f);
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
        //�ڽ��� ��� �ݶ��̴� ��Ȱ��ȭ
        Collider[] mosterColliders = GetComponents<Collider>();
        for (int i = 0; i < mosterColliders.Length; i++)
        {
            mosterColliders[i].enabled = false;
        }

        base.Die();

        //���� ����, ����޽� ��Ȱ��ȭ
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        //��� �ִϸ��̼� ���
        //mosterAnimator.Settrigger("Die");
        //��� ȿ���� ���
        //mosterAudioPlayer.PlayOneShot(deathSound);

    }
    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
    }

    private void heal()
    {
        //�ڽ��� ������� �ʾҰ� ���� �����̰� �������� ����
        if (!dead && Time.time >= lastAttackTime + timeBetAttack)
        {
            lastAttackTime = Time.time;

            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, Monster);
            
            for (int i = 0; i < colliders.Length; i++)
            {
                LivingEntity monster = colliders[i].GetComponent<LivingEntity>();
                
                if (monster.gameObject.layer == 6 && monster != null && !monster.dead)
                {
                    monster.GetComponent<LivingEntity>().RestoreHealth(recoveryAmount);
                }
            }

        }
    }
}