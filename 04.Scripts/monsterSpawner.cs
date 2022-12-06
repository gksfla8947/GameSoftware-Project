using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSpawner : MonoBehaviour
{
    public Monster monsterPrefab;
    public float spawnRate = 3f;

    private float timeAfterSpawn;
    private GameManager gm;

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
        timeAfterSpawn += Time.deltaTime;
        if (timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0f;

            GameObject monster = Instantiate(monsterPrefab.gameObject, transform.position, transform.rotation);
        }
    }             
}
