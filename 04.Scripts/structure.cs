using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class structure : LivingEntity
{
    private Slider HP_slider;//�� ü�¹� �̹���(�����̴�)

    // Start is called before the first frame update
    void Start()
    {
        HP_slider = GetComponentInChildren<Slider>();//��
    }

    // Update is called once per frame
    void Update()
    {
        HP_slider.value = curHealth / health;//��
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
