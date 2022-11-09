using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static bool isStartWave;

    void Awake() {
       DontDestroyOnLoad(this);
    }

    public void getIsStartWave(bool t) {
        isStartWave = t;
    }
    public bool setIsStartWave() {
        return isStartWave;
    }
}
