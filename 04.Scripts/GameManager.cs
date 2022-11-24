using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public Wave[] waves;

    private int currentWaveNum = 0;
    [HideInInspector] 
    public Wave currentWave;

    private bool isWaveStart = false;
    private int killCount = 0;
    public int hp = 100;
    public int power = 10;
    public int speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        currentWave = waves[currentWaveNum].GetComponent<Wave>();
        StartCurrentWave();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWaveNum < waves.Length)
        {
            currentWave = waves[currentWaveNum].GetComponent<Wave>();
            if (!isWaveStart)
            {
                StartCurrentWave();
            }
        }
    }

    public void StartCurrentWave()
    {
        currentWave.gameObject.SetActive(true);
    }

    public void setIsWaveStart(bool t)
    {
        isWaveStart = t;
    }

    public bool getIsWaveStart()
    {
        return isWaveStart;
    }
    public void setKillCount(int t)
    {
        killCount = t;
    }
    public int getKillCount()
    {
        return killCount;
    }

    public void addCurrentWaveNum()
    {
        currentWaveNum += 1;
    }

    public int getCurrentWaveNum()
    {
        return currentWaveNum;
    }
    

    
}
