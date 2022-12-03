using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public Wave[] waves;

    private int currentWaveNum = 0;
    [HideInInspector] 
    public Wave currentWave;

    private bool isGameStart;
    private bool isWaveStart;
    private int killCount;
    public int hp = 100;
    public int power = 10;
    public int speed = 5;

    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        isGameStart = false;
        isWaveStart = false;
        killCount = 0;
        player = GetComponent<Player>();
        currentWave = waves[currentWaveNum].GetComponent<Wave>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStart)
        {
            if (currentWaveNum < waves.Length)
            {
                currentWave = waves[currentWaveNum].GetComponent<Wave>();
                if (!isWaveStart)
                {
                    StartCurrentWave();
                }
            }
        }     
    }

    public void setIsGameStart(bool t)
    {
        isGameStart = t;
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
