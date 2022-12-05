using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 600f;
    private Rigidbody bulletRigidbody;

    public float damage { get; set; }
    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody.velocity = transform.forward * speed * Time.deltaTime;

        Destroy(gameObject, 5f); //5�� �� �ڵ� �ı�
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
            if (attackTarget != null)
            {
                attackTarget.OnDamage(damage, hitPoint, hitnomal);
            }
            Destroy(gameObject);
        }
    }
}
