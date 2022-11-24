using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStartButton : MonoBehaviour
{
    GameManager gm;
    
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void onClick() {
        gm.currentWave.isBattleWave = true;
    }
}
