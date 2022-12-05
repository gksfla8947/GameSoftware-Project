using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject Player;
    public GameObject CameraPosition;

    public float CameraSpeed = 10.0f;
    Vector3 PlayerPos;
    public float offsetZ;
    private void Start()
    {
        offsetZ = Player.transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        PlayerPos = new Vector3(
                Player.transform.position.x + CameraPosition.transform.position.x,
                Player.transform.position.y + CameraPosition.transform.position.y,
                Player.transform.position.z - offsetZ + CameraPosition.transform.position.z
                );
        transform.position = Vector3.Lerp(transform.position, PlayerPos, Time.deltaTime * CameraSpeed);
    }
}