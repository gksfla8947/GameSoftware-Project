using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameManager gm;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI speedText;
    //public TextMeshProUGUI hpText;
    public GameObject ItemSelectUI;
    //public TextMeshProUGUI mpText;
    //public TextMeshProUGUI 
    public GameObject StartButton;
    private int itemflag = 0;//itemUI swich
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
        setstate();
        setitemUI();
    }

    public void setScoreUI()
    {
        if(gm.currentWave.isBattleWave)
        {
            scoreText.text = gm.getKillCount().ToString();
        }
        else
        {
            scoreText.text = "0";
        }
    }

    public void setWaveUI()
    {
        if (gm.currentWave.isBattleWave)
        {
            waveText.text = "웨이브 : " + Wave.battleWaveNum;
        }
        else
        {
            waveText.text = "프리웨이브";
            StartButton.SetActive(true);
        }
        
    }
    public void setstate()
    {
        hpText.text = "HP : " + gm.hp;
        powerText.text = "power : " + gm.power;
        speedText.text = "speed : " + gm.speed;
    }
    public void setitemUI()
    {
        if (gm.currentWave.isBattleWave)
        {
            itemflag=1;
        }
        else
        {
            if(itemflag == 1)
            {
                ItemSelectUI.SetActive(true);
            }
            itemflag=0;
        }
    }
}
