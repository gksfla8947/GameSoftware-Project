using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float spawnRate = 3f; //���� �ֱ�
    public GameObject spawnPosition;

    private float timeAfterSpawn; //�ֱ� ���� ���� ���� �ð�

    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Data.isWaveStart)
        {
            timeAfterSpawn += Time.deltaTime;

            if (timeAfterSpawn >= spawnRate)
            {
                timeAfterSpawn = 0f;

                GameObject monster = Instantiate(monsterPrefab, transform.position, transform.rotation);
                monster.transform.position = spawnPosition.transform.position;
            }
        }
        
    }
}
