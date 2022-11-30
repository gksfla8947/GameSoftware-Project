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
        setitemUI();
        setState();
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
        }
        
    }
    public void setState()
    {
        hpText.text = "HP : " + gm.hp;
        powerText.text = "power : " + gm.power;
        speedText.text = "speed : " + gm.speed;
    }
    public void setitemUI()
    {
        if (!gm.currentWave.isBattleWave)
        {
            ItemSelectUI.SetActive(true);
        }
        else
        {
            ItemSelectUI.SetActive(false);
        }
    }
    public void GameStartOnClick()
    {
        gm.setIsGameStart(true);
        StartButton.SetActive(false);
    }

    public void nextWaveOnClick()
    {
        gm.currentWave.isBattleWave = true;
        ItemSelectUI.SetActive(false);
    }
}
