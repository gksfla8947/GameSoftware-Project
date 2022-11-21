using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWave : Wave
{
    public int winToKillNum = 10;
    public monsterSpawner[] spawners;

    protected override void Awake()
    {
        base.Awake();
        isBattleWave = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.getKillCount() == winToKillNum)
        {
            EndWave();
        }
    }
    protected override void StartWave()
    {
        base.StartWave();
        foreach (monsterSpawner spawner in spawners)
        {
            spawner.gameObject.SetActive(true);
        }
    }

    protected override void EndWave()
    {
        gm.setKillCount(0);
        battleWaveNum += 1;
        foreach (monsterSpawner spawner in spawners)
        {
            spawner.gameObject.SetActive(false);
        }
        base.EndWave();
    }
}
