using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private GameManager gm;

    public GameObject[] items;
    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject item in items)
        {
            item.SetActive(false);
        }
    }
    
    public void ActiveItem(int number)
    {
        items[number].SetActive(true);
    }
}
