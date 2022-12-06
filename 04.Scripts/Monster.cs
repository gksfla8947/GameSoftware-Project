using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Monster : LivingEntity
{
    private GameObject Hair;
    private GameObject Player;
    private Slider HP_Slider; //��

    private float DistHair; //�Ӹ�ī������ �Ÿ�
    private float DistPlayer; //�÷��̾���� �Ÿ�
    private float DistTarget; //Ÿ�ٰ��� �Ÿ�


    public float attackRange = 5; //���� �����Ÿ�
    public GameObject bulletPrefab;


    private LivingEntity targetEntity;//���� ���
    private NavMeshAgent navMeshAgent;//��� ��� AI

    //public AudioClip deathSound;//����� ����� �Ҹ�
    public AudioClip hitSound;//�� �ǰݽ� ����� �Ҹ�
    private AudioSource monsterhit;//�� ����� �ҽ� ������Ʈ

    private Animator monsterAnimator;//�ִϸ����� ������Ʈ
    private Renderer mosterRenderer;//������ ������Ʈ

    public float damage = 20f;//���ݷ�
    public float timeBetAttack = 0.5f;//���� ����
    private float lastAttackTime;//������ ���� ����


    private void Awake()
    {
        // ���� ������Ʈ�κ��� ����� ������Ʈ ��������
        navMeshAgent = GetComponent<NavMeshAgent>();
        monsterAnimator = GetComponent<Animator>();
        //monsterAudioPlayer = GetComponent<AudioSource>();   ����� �÷��̾�, ���� ����

        //������ ������Ʈ�� �ڽ� ������Ʈ�� �����Ƿ� GetComponentInChildren ���
        mosterRenderer = GetComponentInChildren<Renderer>();
        monsterhit = GetComponent<AudioSource>(); //��
    }

    // Start is called before the first frame update
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Hair = GameObject.FindGameObjectWithTag("Hair");
        StartCoroutine(UpdatePath());
        HP_Slider = GetComponentInChildren<Slider>(); //��
    }

    // Update is called once per frame
    void Update()
    {
        HP_Slider.value = curHealth / health;//��
    }
    private IEnumerator UpdatePath()
    {
        while (!dead)
        {
            monsterAnimator.CrossFade("Walk", 0f);

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
        //�ڽ��� ��� �ݶ��̴� ��Ȱ��ȭ
        Collider[] mosterColliders = GetComponents<Collider>();
        for (int i = 0; i < mosterColliders.Length; i++)
        {
            mosterColliders[i].enabled = false;
        }
        if (GameManager.instance.IsWaveStart)
        {
            GameManager.instance.KillCount = GameManager.instance.KillCount + 1;
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
        monsterAnimator.CrossFade("Hit", 0f);
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
