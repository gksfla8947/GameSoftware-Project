using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 300f;
    private Rigidbody bulletRigidbody;

    public float damage = 20f;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody.velocity = transform.forward * speed * Time.deltaTime;

        Destroy(gameObject, 2f); //2�� �� �ڵ� �ı�
    }

    public void DebugLoc(Vector3 playerPos)
    {
        //Debug.Log("attack pos : " + playerPos.ToString());
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) //Target ���̾��̸�
        {
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();

            //������ �ǰ� ��ġ�� �ǰ� ������ �ٻ����� ���
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            Vector3 hitnomal = transform.position - other.transform.position;

            //���� ����
            attackTarget.OnDamage(damage, hitPoint, hitnomal);

            Destroy(gameObject);
        }
    }
}
