using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 5.0f;   // ĳ���� ������ ���ǵ�
    public CharacterController characterController; // ĳ���� ��Ʈ�ѷ�
    public Camera mainCamera; // ���� ī�޶�
    //public GameObject mousePoint;


    Vector3 movePoint; // �̵� ��ġ ����
    Vector3 temp;

    void Start()
    {
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        // ��Ŭ�� �̺�Ʈ�� ���Դٸ�
        if (Input.GetMouseButtonUp(0))
        {
            // ī�޶󿡼� �������� ���.
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            // Scence ���� ī�޶󿡼� ������ ������ ������ Ȯ���ϱ�
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f);

            // �������� ������ �¾Ҵٸ�
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                // ���� ��ġ�� �������� ����
                movePoint = raycastHit.point;
                //mousePoint.transform.position = movePoint;
                Debug.Log("movePoint : " + movePoint.ToString());
                Debug.Log("���� ��ü : " + raycastHit.transform.name);

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


        // ���������� �Ÿ��� 0.1f ���� �ִٸ�
        if (Vector3.Distance(transform.position, movePoint) > 0.1f)
        {

            // �̵�
            Move();
        }

    }

    void Move()
    {
        // thisUpdatePoint �� �̹� ������Ʈ(������) ���� �̵��� ����Ʈ�� ��� ������.
        // �̵��� ����(�̵��� ��-���� ��ġ) ���ϱ� �ӵ��� �ؼ� �̵��� ��ġ���� ����Ѵ�.
        Vector3 thisUpdatePoint = (movePoint - transform.position).normalized * speed;
        // characterController �� ĳ���� �̵��� ����ϴ� ������Ʈ��.
        // simpleMove �� �ڵ����� �߷��� ����ؼ� �̵������ִ� �޼ҵ��.
        // ������ �̵��� ����Ʈ�� �������ָ� �ȴ�.
        characterController.SimpleMove(thisUpdatePoint);
    }
}