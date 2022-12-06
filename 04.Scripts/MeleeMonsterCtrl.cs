using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq; //LINQ ���
using UnityEngine.UI;


public class MeleeMonsterCtrl : LivingEntity
{
    private GameObject Hair;
    private GameObject Player;
    private Slider HP_slider;//�� ü�¹� �̹���(�����̴�)

    private float DistHair; //�Ӹ�ī������ �Ÿ�
    private float DistPlayer; //�÷��̾���� �Ÿ�
    private float DistTarget; //Ÿ�ٰ��� �Ÿ�

    public float attackRange = 5; //���� �����Ÿ�
    public GameObject bulletPrefab;

    //public LayerMask whatIstarget; //���� ��� ���̾� �ʿ� ������

    private LivingEntity targetEntity;//���� ���
    private NavMeshAgent navMeshAgent;//��� ��� AI

    //List<Target> targets = new List<Target>(); �ʿ� ������

    //public ParticleSystem hitEffect;//�ǰݽ� ����� ��ƼŬ
    //public AudioClip deathSound;//����� ����� �Ҹ�
    public AudioClip hitSound;//�� �ǰݽ� ����� �Ҹ� 
    private AudioSource monsterhit; //��  ���ͳ��� �߰��� audiosource ���۳�Ʈ ����ϱ� ����

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
        // whatIstarget = LayerMask.GetMask("Target"); �ʿ� ������
        monsterhit = GetComponent<AudioSource>();//�� �ش� ��ũ��Ʈ�� ���� ���Ͱ� ������ �ڵ����� �ҷ�����
    }

    // Start is called before the first frame update
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        //Hair = GameObject.FindGameObjectWithTag("Hair");
        Hair = GameObject.Find("Stage").transform.GetChild(3).gameObject;
        StartCoroutine(UpdatePath());
        HP_slider = GetComponentInChildren<Slider>();//��
    }

    // Update is called once per frame
    void Update()
    {

        //���� ����� ���� ���ο� ���� �ٸ� �ִϸ��̼� ���
        //monsterAnimator.SetBool("HasTarget", hasTarget);
        HP_slider.value = curHealth / health;//��    
    }
/*    private void FixedUpdate()
    {
        HP_slider.transform.rotation.x = -this.transform.rotation.x;
        HP_slider.transform.rotation.y = -this.transform.rotation.y;
        HP_slider.transform.rotation.z = -this.transform.rotation.z;
    }
*/
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
        monsterhit.PlayOneShot(hitSound);//�� ������ ���� �ش� ��ũ��Ʈ���� ������ hitSound �� �÷��� �ϰڴٴ� �ǹ�.
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
        //monsterhit.PlayOneShot(hitSound);
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

        //��� �ִϸ��̼� ���
        //mosterAnimator.Settrigger("Die");
        //��� ȿ���� ���
        //mosterAudioPlayer.PlayOneShot(deathSound);

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

        }
    }
}
