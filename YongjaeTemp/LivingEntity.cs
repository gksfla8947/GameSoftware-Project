using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LivingEntity : MonoBehaviour
{
    public float startingHealth = 100f; //���� ü��
    public float health { get; protected set; } //���� ü��
    public bool dead { get; protected set; } //�������
    public event Action onDeath; //��� �� �ߵ��� �̺�Ʈ

    public float tempHp;
    
    //����ü�� Ȱ��ȭ�� �� ���¸� ����
    protected virtual void OnEnable()
    {
        //������� ���� ���·� ����
        dead = false;
        //ü�� �ʱ�ȭ
        health = startingHealth;
    }

    // ����� �Դ� ���
    //OnDamage(�����, ���ݴ��� ��ġ, ���ݴ��� ǥ���� ����)
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        health -= damage;

        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    //ü�� ȸ��
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
        {
            return;
        }
        health += newHealth;
    }

    //���ó��
    public virtual void Die()
    {
        Debug.Log("Die");
        if(onDeath != null)
        {
            //onDeath();    //?
            gameObject.SetActive(false);
        }
        dead = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        tempHp = health;
    }
}
