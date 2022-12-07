using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    public float damage = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        //damage = GameManager.instance.player.atk;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }
}
