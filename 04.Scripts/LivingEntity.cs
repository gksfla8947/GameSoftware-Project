using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;

public class LivingEntity : MonoBehaviour
{
    public AudioClip DmgClip;

    public float health = 100f; //시작 체력
    public float curHealth; //현재 체력
    public bool dead { get; protected set; } //사망상태

    public GameObject hitEffect;
    public GameObject dyingEffect;

    private AudioSource DmgSource;
    protected Slider HP_slider;//민 체력바 이미지(슬라이더)

    public virtual void Awake()
    {
        //사망하지 않은 상태로 시작
        dead = false;
        //체력 초기화
        curHealth = health;
        HP_slider = GetComponentInChildren<Slider>();//민
    }

    public virtual void Update()
    {
        HP_slider.value = curHealth / health;//민
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
        Instantiate(dyingEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    public virtual void OnDestroy()
    {
    }
}