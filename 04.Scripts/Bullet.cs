using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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

        Destroy(gameObject, 2f); //2초 뒤 자동 파괴
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) //Target 레이어이면
        {
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();

            //상대방의 피격 위치와 피격 방향을 근삿값으로 계산
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            Vector3 hitnomal = transform.position - other.transform.position;

            //공격 실행
            attackTarget.OnDamage(damage, hitPoint, hitnomal);

            Destroy(gameObject);
        }
    }
}