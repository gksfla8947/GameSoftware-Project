using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    private float damage = 20f;



    void Start()
    {
        damage = GameManager.instance.player.atk;
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

            //상대방의 피격 위치와 피격 방향을 근삿값으로 계산
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            Vector3 hitnomal = transform.position - other.transform.position;

            //공격 실행
            if (attackTarget != null)
            {
                attackTarget.OnDamage(damage, hitPoint, hitnomal);
            }

 
            Destroy(gameObject);
        }
    }
}
