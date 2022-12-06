using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : LivingEntity
{
    public float speed = 5.0f;
    public Camera mainCamera;
    public GameObject mousePointer;
    public int atk = 20;
    public float attackRate = 0.5f;
    public float fasterAttackRate = 0.2f;
    public float fasterAttackTime = 3f;


    

    public virtual void StartInit()
    {

    }
    public virtual void AwakeInit()
    {
        
    }

    public virtual void UpdateVirtual()
    {

    }

    private void Awake()
    {
        StartInit();
    }
    private void Start()
    {
        StartInit();
    }

    void Update()
    {
        UpdateVirtual();
    }

    

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();
        Time.timeScale = 0;
    }
}