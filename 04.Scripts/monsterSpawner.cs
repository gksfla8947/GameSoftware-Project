using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float spawnRate = 3f;

    private float timeAfterSpawn;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.NumOfMonster < ((BattleWave)GameManager.instance.currentWave).spawnMaxNum)
        {
            timeAfterSpawn += Time.deltaTime;
            if (timeAfterSpawn >= spawnRate)
            {
                timeAfterSpawn = 0f;

                GameObject monster = Instantiate(monsterPrefab.gameObject, transform.position, transform.rotation);
                Transform monsterPool = GameObject.Find("Object Pool").transform.GetChild(0);
                monster.transform.SetParent(monsterPool);
            }
        }
    }             
}
