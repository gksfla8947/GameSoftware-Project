using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFieldControl : MonoBehaviour
{
    public float damage = 20.0f;
    public float damageTime = 1.5f;
    public float resistTime = 11.0f;
    public float inTime = 0.0f;
    private void Start()
    {
        //damage = GameManager.instance.player.atk;
    }

    private void FixedUpdate()
    {
        damageTime += Time.deltaTime;
        inTime += Time.deltaTime;
        if (inTime > resistTime)
            Destroy(gameObject);
    }


    private void OnTriggerStay(Collider other)
    {
        if (damageTime >= 1.4f)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                LivingEntity attackTarget = other.GetComponent<LivingEntity>();

                //������ �ǰ� ��ġ�� �ǰ� ������ �ٻ����� ���
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitnomal = transform.position - other.transform.position;

                //���� ����
                attackTarget.OnDamage(damage, hitPoint, hitnomal);


            }
            damageTime = 0;
        }
        
    }
}
