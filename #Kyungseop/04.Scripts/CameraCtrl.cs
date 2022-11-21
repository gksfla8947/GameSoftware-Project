using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject Player;               // ī�޶� ����ٴ� Ÿ��

    /*    public float offsetX = 0f;            // ī�޶��� x��ǥ
        public float offsetY = 10f;           // ī�޶��� y��ǥ
        public float offsetZ = -9f;          // ī�޶��� z��ǥ*/

    Vector3 cameraPosition = new Vector3(0, 10, -9);

    public float CameraSpeed = 10.0f;       // ī�޶��� �ӵ�
    Vector3 PlayerPos;                      // Ÿ���� ��ġ

    // Update is called once per frame
    void FixedUpdate()
    {

        /*   // Ÿ���� x, y, z ��ǥ�� ī�޶��� ��ǥ�� ���Ͽ� ī�޶��� ��ġ�� ����
           PlayerPos = new Vector3(
               Player.transform.position.x + offsetX,
               transform.position.y,
               Player.transform.position.z + offsetZ
               );

           // ī�޶��� �������� �ε巴�� �ϴ� �Լ�(Lerp)
           transform.position = Vector3.Lerp(transform.position, PlayerPos, Time.deltaTime * CameraSpeed);*/

        transform.position = Vector3.Lerp(transform.position, Player.transform.position + cameraPosition, Time.deltaTime * CameraSpeed);

    }
}