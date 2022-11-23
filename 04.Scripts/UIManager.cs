using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameManager gm;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
    //public TextMeshProUGUI hpText;
    //public Canvas ItemSelectUI;
    //public TextMeshProUGUI mpText;
    //public TextMeshProUGUI 

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        setScoreUI();
        setWaveUI();
    }

    public void setScoreUI()
    {
        if(gm.currentWave.isBattleWave)
        {
            scoreText.text = "Score : " + gm.getKillCount();
        }
        else
        {
            scoreText.text = "Score : 0";
        }
    }

    public void setWaveUI()
    {
        if (gm.currentWave.isBattleWave)
        {
            waveText.text = "현재 웨이브 : " + Wave.battleWaveNum;
        }
        else
        {
            waveText.text = "프리 웨이브";
        }
    }
}
