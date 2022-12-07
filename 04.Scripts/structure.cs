using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class structure : LivingEntity
{
    private Slider HP_slider;//민 체력바 이미지(슬라이더)

    public override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        HP_slider = GetComponentInChildren<Slider>();//민
    }

    // Update is called once per frame
    void Update()
    {
        HP_slider.value = curHealth / health;//민
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);

        if (!dead)
        {
            //공격받은 지점과 방향으로 파티클 효과 재생
            //hitEffect.transform.position = hitPoint;
            //hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            //hitEffect.PLay();

            //mosterAudioPlayer.PlayOneShot(hitSound); //피격 효과음 재생
        }
    }

    public override void Die()
    {
        //자신의 모든 콜라이더 비활성화
        Collider[] mosterColliders = GetComponents<Collider>();
        for (int i = 0; i < mosterColliders.Length; i++)
        {
            mosterColliders[i].enabled = false;
        }

        base.Die();

        //사망 애니메이션 재생
        //mosterAnimator.Settrigger("Die");
        //사망 효과음 재생
        //mosterAudioPlayer.PlayOneShot(deathSound);

    }
}
