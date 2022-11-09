using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static bool isWaveStart;
    void Awake() {
        DontDestroyOnLoad(this);
    }

    public void setIsWaveStart(bool t) {
        isWaveStart = t;
    }
    public bool getIsWaveStart() {
        return isWaveStart;
    }
}
