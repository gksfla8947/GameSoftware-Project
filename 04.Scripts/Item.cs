using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float amount;
    public string itemName;
    public bool isActiveItem;

    public string description;

    public void ApplyItem()
    {
        switch (itemName)
        {
            case "AttackRateUp":
                AttackRateUp(amount);
                break;
            case "AttackDamageUp":
                AttackDamageUp(amount);
                break;
            case "SpeedUp":
                SpeedUp(amount);
                break;
            case "HealthUp":
                HealthUp(amount);
                break;
            case "RestoreHealth":
                RestoreHealth(amount);
                break;
            case "ElimateAllEnemy":
                ElimateAllEnemy();
                break;
            case "SuperAttackRateUp":
                SuperAttackRateUp(amount);
                break;

        }
    }

    public void AttackRateUp(float amount)
    {
        GameManager.instance.player.attackRate -= amount;
    }

    public void AttackDamageUp(float amount)
    {
        GameManager.instance.player.atk += (int)amount;
    }

    public void SpeedUp(float amount)
    {
        GameManager.instance.player.speed += amount;
    }

    public void HealthUp(float mount)
    {
        GameManager.instance.player.health += amount;
    }

    public void RestoreHealth(float amount)
    {
    }

    public void ElimateAllEnemy()
    {
        Monster[] monsters = GameObject.Find("Object Pool").transform.GetChild(0).GetComponentsInChildren<Monster>();
        foreach(Monster monster in monsters) { Destroy(monster.gameObject); }
    }

    public void SuperAttackRateUp(float amount)
    {

    }
}
