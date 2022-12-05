using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreWave : Wave
{
    public Item[] items;
    private ItemManager im;

    protected override void Awake()
    {
        base.Awake();
        im = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        isBattleWave = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBattleWave)
        {
            EndWave();
        }
    }

    protected override void StartWave()
    {
        base.StartWave();
        im.SelectItems();
        im.InstantiateItems();
        Time.timeScale = 0;
    }

    protected override void EndWave()
    {
        im.ClearItems();
        preWaveNum += 1;
        Time.timeScale = 1;
        base.EndWave();
    }
}
