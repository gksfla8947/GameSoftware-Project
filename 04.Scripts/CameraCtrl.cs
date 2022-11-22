using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject Player;            

    public float offsetX = 0f;      
    public float offsetY = 10f;           
    public float offsetZ = 0f;         

    public float CameraSpeed = 10.0f;       
    Vector3 PlayerPos;                      

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerPos = new Vector3(
            Player.transform.position.x + offsetX,
            transform.position.y,
            Player.transform.position.z + offsetZ
            );
        transform.position = Vector3.Lerp(transform.position, PlayerPos, Time.deltaTime * CameraSpeed);
    }
}