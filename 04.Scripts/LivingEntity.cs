using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LivingEntity : MonoBehaviour
{
    public float startingHealth = 100f; //시작 체력
    public float health { get; protected set; } //현재 체력
    public bool dead { get; protected set; } //사망상태

    public float tempHp; //유니티 창에서 현재 체력 볼려고 임시로 넣은거
    
    //생명체가 활성화될 때 상태를 리셋
    protected virtual void OnEnable()
    {
        //사망하지 않은 상태로 시작
        dead = false;
        //체력 초기화
        health = startingHealth;
    }

    // 대미지 입는 기능
    //OnDamage(대미지, 공격당한 위치, 공격당한 표면의 방향)
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        health -= damage;

        if (health <= 0)
        {
            dead = true;
            Die();
        }
    }

    //체력 회복
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
        {
            return;
        }
        health += newHealth;
        if (health > startingHealth)
        {
            health = startingHealth;
        }
    }

    //사망처리
    public virtual void Die()
    {
        gameObject.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        // health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        tempHp = health;
    }
}