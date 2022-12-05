using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
//using UnityEngine.UI;//민

public class LivingEntity : MonoBehaviour
{
    //Image hpBarImage;//민
    public float health = 100f; //시작 체력
    //[HideInInspector]
    public float curHealth; //현재 체력
    public bool dead { get; protected set; } //사망상태

    public GameObject hitEffect;
    public GameObject dyingEffect;

    //생명체가 활성화될 때 상태를 리셋
    protected virtual void OnEnable()
    {
        //사망하지 않은 상태로 시작
        dead = false;
        //체력 초기화
        curHealth = health;
    }

    // 대미지 입는 기능
    //OnDamage(대미지, 공격당한 위치, 공격당한 표면의 방향)
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        curHealth -= damage;

        if (curHealth <= 0)
        {
            dead = true;
            Die();
        }
        Instantiate(hitEffect, hitPoint, transform.rotation);
    }

    //체력 회복
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
        {
            return;
        }
        Debug.Log("heal");
        curHealth += newHealth;

        if (curHealth > health)
        {
            curHealth = health;
        }
    }

    //사망처리
    public virtual void Die()
    {
        Destroy(gameObject);
        Instantiate(dyingEffect, transform.position, transform.rotation);
    }


    // Start is called before the first frame update
    void Start()
    {
        // health = startingHealth;
        //hpBarImage = GetComponentInChildren<Image>();//민
    }

    // Update is called once per frame
    void Update()
    {
        //hpBarImage.fillAmount = curHealth / health;//민
    }
}