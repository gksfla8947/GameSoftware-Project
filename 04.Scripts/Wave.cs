using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    protected GameManager gm;
    [HideInInspector]
    public bool isBattleWave;
    [HideInInspector]
    static public int battleWaveNum = 1;

    protected virtual void Awake()
    {
        gameObject.SetActive(false);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    protected virtual void StartWave()
    {
        gm.setIsWaveStart(true);
    }

    protected virtual void EndWave()
    {
        gm.addCurrentWaveNum();
        gm.setIsWaveStart(false);
        gameObject.SetActive(false);
    }
}
