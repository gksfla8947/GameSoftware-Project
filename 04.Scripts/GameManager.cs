using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Wave[] waves;

    [HideInInspector] 
    public Wave currentWave;
    [HideInInspector]
    public Player player;

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
    // Start is called before the first frame update
    void Start()
    {
        isGameStart = false;
        isWaveStart = false;
        killCount = 0;
        activeItemSlot = 2;
        player = GameObject.Find("Player").GetComponent<Player>();
        currentWave = waves[currentWaveNum].GetComponent<Wave>();
    }

    void Update()
    {
        // UI
        UIManager.instance.setScoreUI();
        UIManager.instance.setWaveUI();
        UIManager.instance.setitemUI();
        UIManager.instance.setState();

        // Wave
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

        // Item
        ItemManager.instance.GetPreWave();
    }

    public void StartCurrentWave()
    {
        currentWave.gameObject.SetActive(true);
    }
}
