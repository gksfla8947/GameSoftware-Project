using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
<<<<<<< Updated upstream
    public float spawnRate = 3f; //���� �ֱ�
=======
    public float spawnRate = 3f; //���� �ֱ�
    public GameObject spawnPosition;
>>>>>>> Stashed changes

    private float timeAfterSpawn; //�ֱ� ���� ���� ���� �ð�

    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f;
<<<<<<< Updated upstream
        GameObject monster = Instantiate(monsterPrefab, transform.position, transform.rotation);
=======
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        if(Data.isStartWave) {
            timeAfterSpawn += Time.deltaTime;

            if (timeAfterSpawn >= spawnRate) {
                timeAfterSpawn = 0f;

<<<<<<< Updated upstream
            GameObject monster = Instantiate(monsterPrefab, transform.position, transform.rotation);
=======
                GameObject monster = Instantiate(monsterPrefab, transform.position, transform.rotation);
                monster.transform.position = spawnPosition.transform.position;
            }
>>>>>>> Stashed changes
        }
    }
}
