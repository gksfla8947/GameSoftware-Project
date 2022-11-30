using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private GameManager gm;
    public float amount;
    public string itemName;
    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        switch(itemName)
        {
            case "AttackRateUp":
                AttackRateUp(amount);
                break;
        }
    }

    public void AttackRateUp(float amount)
    {
        gm.player.attackRate -= amount;
    }
}
