using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Structure : LivingEntity
{
    public override void Awake()
    {
        base.Awake();
        HP_slider = transform.GetChild(0).GetComponentInChildren<Slider>();//��
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);

        if (!dead)
        {
            //���ݹ��� ������ �������� ��ƼŬ ȿ�� ���
            //hitEffect.transform.position = hitPoint;
            //hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            //hitEffect.PLay();

            //mosterAudioPlayer.PlayOneShot(hitSound); //�ǰ� ȿ���� ���
        }
    }

    public override void Die()
    {
        //�ڽ��� ��� �ݶ��̴� ��Ȱ��ȭ
        Collider[] mosterColliders = GetComponents<Collider>();
        for (int i = 0; i < mosterColliders.Length; i++)
        {
            mosterColliders[i].enabled = false;
        }

        base.Die();

        //��� �ִϸ��̼� ���
        //mosterAnimator.Settrigger("Die");
        //��� ȿ���� ���
        //mosterAudioPlayer.PlayOneShot(deathSound);

    }
}
