using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Wave[] waves;

    [HideInInspector] 
    public Wave currentWave;

    public Player[] playerTypes;
    //[HideInInspector]
    public Player player;
    public GameObject playerPrefab;

    public GameObject ProtectedObject;

    private int playerCode;

    private int currentWaveNum = 0;
    public int CurrentWaveNum
    {
        get { return currentWaveNum; }
        set { currentWaveNum = value; }
    }


    private bool isGameStart;
    public bool IsGameStart
    {
        get { return isGameStart; }
        set { isGameStart = value; }
    }

    private bool isWaveStart;
    public bool IsWaveStart
    {
        get { return isWaveStart; }
        set { isWaveStart = value; }
    }
    private int killCount;
    public int KillCount
    {
        get { return killCount; }
        set { killCount = value; }
    }
    private int activeItemSlot;
    public int ActiveItemSlot
    {
        get { return activeItemSlot; }
        set { activeItemSlot = value; }
    }

    private int numOfMonster = 0;
    public int NumOfMonster
    {
        get { return numOfMonster; }
        set { numOfMonster = value; }
    }

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this; 
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            if (instance != this) 
                Destroy(this.gameObject); 
        }
        //playerCode = 0;
        playerCode = SelectCharacter.Instance.characterNum;
        playerPrefab = Instantiate(playerTypes[playerCode].GetComponent<Player>().gameObject);
        player = playerPrefab.GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isGameStart = false;
        isWaveStart = false;
        killCount = 0;
        activeItemSlot = 2;
        currentWave = waves[currentWaveNum].GetComponent<Wave>();
        UIManager.instance.setScoreUI();
        UIManager.instance.setWaveUI();
        UIManager.instance.setitemUI();
        UIManager.instance.setState();
    }

    void Update()
    {
        
        if (isGameStart)
        {
            UIManager.instance.setScoreUI();
            UIManager.instance.setWaveUI();
            UIManager.instance.setitemUI();
            UIManager.instance.setState();

            numOfMonster = GameObject.Find("Object Pool").transform.GetChild(0).childCount;


            if (currentWaveNum < waves.Length)
            {
                currentWave = waves[currentWaveNum].GetComponent<Wave>();
                if (!isWaveStart)
                {
                    StartCurrentWave();
                }
            }
            else if(currentWaveNum == waves.Length)
            {
                isGameStart = false;
                SceneManager.LoadScene("EndScene");
            }
            
            if(ProtectedObject.IsDestroyed() || player.IsDestroyed())
            {
                isGameStart = false;
                SceneManager.LoadScene("GameOverScene");
            }
        }
        ItemManager.instance.GetPreWave();


        // Item
    }

    public void StartCurrentWave()
    {
        currentWave.gameObject.SetActive(true);
    }
}
