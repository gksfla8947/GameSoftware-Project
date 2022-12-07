using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI attackrateText;

    public GameObject activeItemsUI;
    public GameObject ItemSelectUI;
    public GameObject StartButton;



    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }


    public void setScoreUI()
    {
        if(GameManager.instance.currentWave.isBattleWave)
        {
            scoreText.text = GameManager.instance.KillCount.ToString();
        }
        else
        {
            scoreText.text = "0";
        }
    }

    public void setWaveUI()
    {
        if (GameManager.instance.currentWave.isBattleWave)
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
        hpText.text = "HP : " + GameManager.instance.player.curHealth;
        powerText.text = "Power : " + GameManager.instance.player.atk;
        speedText.text = "Speed : " + (int)GameManager.instance.player.speed;
        attackrateText.text = "AttackRate : " + GameManager.instance.player.attackRate;
        //attackrateText.text = "AttackRate : "+ (float)GameManager.instance.player.attackRate;
    }
    public void setitemUI()
    {
        if (!GameManager.instance.currentWave.isBattleWave)
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
        GameManager.instance.IsGameStart = true;
        StartButton.SetActive(false);
    }

    public void nextWaveOnClick()
    {
        GameManager.instance.currentWave.isBattleWave = true;
        ItemSelectUI.SetActive(false);
    }
}
