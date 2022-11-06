using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float spawnRate = 3f; //생성 주기

    private float timeAfterSpawn; //최근 생성 이후 지난 시간

    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f;
        GameObject monster = Instantiate(monsterPrefab, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn += Time.deltaTime;

        if (timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0f;

            GameObject monster = Instantiate(monsterPrefab, transform.position, transform.rotation);
        }
    }
}
