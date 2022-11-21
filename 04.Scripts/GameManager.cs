using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI waveUI;
    public Wave[] waves;

    private int currentWaveNum = 0;
    [HideInInspector] public Wave currentWave;

    private bool isWaveStart = false;
    private int killCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentWave = waves[currentWaveNum].GetComponent<Wave>();
        StartCurrentWave();
        setWaveUI();
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

    public void setScoreUI()
    {
        scoreUI.text = "Score : " + killCount;
    }

    public void setWaveUI()
    {
        if (currentWave.isBattleWave)
        {
            waveUI.text = "현재 웨이브 : " + currentWave.battleWaveNum;
        }
        else
        {
            waveUI.text = "프리 웨이브";
        }
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
