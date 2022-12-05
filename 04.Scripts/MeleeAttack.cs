using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    public Rigidbody meleeAttackRigidbody;
    public float damage { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        meleeAttackRigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        transform.Translate(Vector3.forward * 1);
        Destroy(gameObject, 0.1f);
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
