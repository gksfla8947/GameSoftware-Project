using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreWave : Wave
{

    protected override void Awake()
    {
        base.Awake();
        isBattleWave = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBattleWave)
        {
            EndWave();
        }
    }

    protected override void StartWave()
    {
        base.StartWave();
        Time.timeScale = 0;
    }

    protected override void EndWave()
    {
        Time.timeScale = 1;
        base.EndWave();
    }
}
