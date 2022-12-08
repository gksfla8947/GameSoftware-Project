using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class structure : LivingEntity
{
    private Slider HP_slider;//민 체력바 이미지(슬라이더)

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
    }
}
