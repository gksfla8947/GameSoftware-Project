using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public bool isStart;
    public Text countdownText;
    float curTime;
    // Start is called before the first frame update
    void Start()
    {
        curTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart) {
            curTime += 1 * Time.deltaTime;
            countdownText.text = curTime.ToString();
        }
        
    }
}
