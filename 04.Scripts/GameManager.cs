using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isWaveStart = false;
    public GameObject[] wave;
    private Time timer;
    private int killCount;
    private Canvas ItemUI;

    public GameObject monsterSpawner;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
