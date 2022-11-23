using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public float damage = 100f;

    private GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        this.transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();

            //상대방의 피격 위치와 피격 방향을 근삿값으로 계산
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            Vector3 hitnomal = transform.position - other.transform.position;

            //공격 실행
            attackTarget.OnDamage(damage, hitPoint, hitnomal);

            if (gm.getIsWaveStart())
            {
                gm.setKillCount(gm.getKillCount() + 1);
            }
            Destroy(gameObject);
        }
    }
}
