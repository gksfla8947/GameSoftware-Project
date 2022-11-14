using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 5.0f;   // 캐릭터 움직임 스피드
    public CharacterController characterController; // 캐릭터 컨트롤러
    public Camera mainCamera; // 메인 카메라
    //public GameObject mousePoint;


    Vector3 movePoint; // 이동 위치 저장
    Vector3 temp;

    void Start()
    {
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        // 좌클릭 이벤트가 들어왔다면
        if (Input.GetMouseButtonUp(0))
        {
            // 카메라에서 레이저를 쏜다.
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            // Scence 에서 카메라에서 나오는 레이저 눈으로 확인하기
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f);

            // 레이저가 뭔가에 맞았다면
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                // 맞은 위치를 목적지로 저장
                movePoint = raycastHit.point;
                //mousePoint.transform.position = movePoint;
                Debug.Log("movePoint : " + movePoint.ToString());
                Debug.Log("맞은 객체 : " + raycastHit.transform.name);

                //temp = mousePoint.transform.position - transform.position;
                //temp -= new Vector3(0, temp.y, 0);
            }
        }

        //if (temp.magnitude < 0.01f)
        //{
        //    temp = mousePoint.transform.position - transform.position;
        //    temp -= new Vector3(0, temp.y, 0);
        //    transform.position += temp.normalized * speed * Time.deltaTime;
        //}


        // 목적지까지 거리가 0.1f 보다 멀다면
        if (Vector3.Distance(transform.position, movePoint) > 0.1f)
        {

            // 이동
            Move();
        }

    }

    void Move()
    {
        // thisUpdatePoint 는 이번 업데이트(프레임) 에서 이동할 포인트를 담는 변수다.
        // 이동할 방향(이동할 곳-현재 위치) 곱하기 속도를 해서 이동할 위치값을 계산한다.
        Vector3 thisUpdatePoint = (movePoint - transform.position).normalized * speed;
        // characterController 는 캐릭터 이동에 사용하는 컴포넌트다.
        // simpleMove 는 자동으로 중력을 계산해서 이동시켜주는 메소드다.
        // 값으로 이동할 포인트를 전달해주면 된다.
        characterController.SimpleMove(thisUpdatePoint);
    }
}