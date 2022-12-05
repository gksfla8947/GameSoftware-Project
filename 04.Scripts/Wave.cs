using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Wave : MonoBehaviour
{
    [HideInInspector]
    public bool isBattleWave;
    [HideInInspector]
    static public int battleWaveNum = 1;
    static public int preWaveNum = 1;


    protected virtual void Awake()
    {
        gameObject.SetActive(false);
    }

    protected virtual void StartWave()
    {
        GameManager.instance.IsWaveStart = true;
    }

    protected virtual void EndWave()
    {
        GameManager.instance.IsWaveStart = false;
        GameManager.instance.CurrentWaveNum += 1;
        gameObject.SetActive(false);
    }
}
