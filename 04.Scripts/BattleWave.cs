using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWave : Wave
{
    public int winToKillNum = 10;
    public MonsterSpawner[] spawners;

    public int spawnMaxNum;
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
        if (GameManager.instance.KillCount == winToKillNum)
        {
            EndWave();
        }
    }
    protected override void StartWave()
    {
        base.StartWave();
        foreach (MonsterSpawner spawner in spawners)
        {
            spawner.gameObject.SetActive(true);
        }
    }

    protected override void EndWave()
    {
        GameManager.instance.KillCount = 0;
        battleWaveNum += 1;
        foreach (MonsterSpawner spawner in spawners)
        {
            spawner.gameObject.SetActive(false);
        }
        Monster[] monsters = GameObject.Find("Object Pool").transform.GetChild(0).GetComponentsInChildren<Monster>();
        foreach (Monster monster in monsters) { Destroy(monster.gameObject); }
        base.EndWave();
    }
}
