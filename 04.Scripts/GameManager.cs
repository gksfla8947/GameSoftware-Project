using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Wave[] waves;

    [HideInInspector] 
    public Wave currentWave;
    //[HideInInspector]
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
        if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
        {
            instance = this; //���ڽ��� instance�� �־��ݴϴ�.
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ����
        }
        else
        {
            if (instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
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
