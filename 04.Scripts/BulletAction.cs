using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public float damage = 100f;


    void Start()
    {
    }
    void Update()
    {
        this.transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
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
