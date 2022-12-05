using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MeleeMonsterCtrl : LivingEntity
{
    private GameObject Hair;
    private GameObject Player;

    private float DistHair; //�Ӹ�ī������ �Ÿ�
    private float DistPlayer; //�÷��̾���� �Ÿ�
    private float DistTarget; //Ÿ�ٰ��� �Ÿ�


    public float attackRange = 5; //���� �����Ÿ�
    public GameObject bulletPrefab;


    private LivingEntity targetEntity;//���� ���
    private NavMeshAgent navMeshAgent;//��� ��� AI

    //public AudioClip deathSound;//����� ����� �Ҹ�
    //public AudioClip hitSound;//�ǰݽ� ����� �Ҹ�

    private Animator monsterAnimator;//�ִϸ����� ������Ʈ
    //private AudioSource monsterAudioPlayer;//����� �ҽ� ������Ʈ
    private Renderer mosterRenderer;//������ ������Ʈ

    public float damage = 20f;//���ݷ�
    public float timeBetAttack = 0.5f;//���� ����
    private float lastAttackTime;//������ ���� ����

    private GameManager gm;

    private bool Work;
    private bool Hit;
    private bool Damage;

    private void Awake()
    {
        // ���� ������Ʈ�κ��� ����� ������Ʈ ��������
        navMeshAgent = GetComponent<NavMeshAgent>();
        monsterAnimator = GetComponent<Animator>();
        //monsterAudioPlayer = GetComponent<AudioSource>();   ����� �÷��̾�, ���� ����

        //������ ������Ʈ�� �ڽ� ������Ʈ�� �����Ƿ� GetComponentInChildren ���
        mosterRenderer = GetComponentInChildren<Renderer>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
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
                attack(targetEntity);
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
        if (gm.getIsWaveStart())
        {
            gm.setKillCount(gm.getKillCount() + 1);
        }
        //���� ����, ����޽� ��Ȱ��ȭ
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
        //�ڽ��� ������� �ʾҰ� ���� �����̰� �������� ����
        if (!dead && Time.time >= lastAttackTime + timeBetAttack)
        {
            lastAttackTime = Time.time;

            // GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Vector3 curPos = transform.position;
            curPos.y += 1f;
            GameObject bullet = Instantiate(bulletPrefab, curPos, Quaternion.identity);
            bullet.transform.LookAt(targetEntity.transform);

            MeleeAttack temp = bullet.GetComponent<MeleeAttack>();
            temp.damage = damage;
        }
    }
    void UpdateMonsterAnimation()
    {
        monsterAnimator.SetBool("Work", Work);
        monsterAnimator.SetBool("Hit", Hit);
        monsterAnimator.SetBool("Damage", Damage);
    }
}