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
    public float coefIncAttackRate = 0.8f;
    public float fasterAttackTime = 3f;

    public override void Awake()
    {
        base.Awake();
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

    public virtual void UseActiveItem()
    {
        if (ItemManager.instance.SlotItems[0] != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ItemManager.instance.ActiveSlotItem(0);
                ItemManager.instance.ClearActiveItem(0);
            }
        }
        if (ItemManager.instance.SlotItems[1] != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ItemManager.instance.ActiveSlotItem(1);
                ItemManager.instance.ClearActiveItem(1);
            }
        } 
    }
}