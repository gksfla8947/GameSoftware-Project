using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 5.0f; 
    public float rotateSpeed = 10.0f;
    public CharacterController characterController; 
    public Camera mainCamera; 
    public GameObject mousePointer;
    Vector3 movePoint; 

    void Start()
    {
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
        movePoint = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                movePoint = raycastHit.point;
                mousePointer.transform.position = raycastHit.point;
            }
        }
        if (Vector3.Distance(transform.position, movePoint) > 0.1f)
        {
            Move();
            Rotate();
        }

    }

    void Move()
    {
        Vector3 thisUpdatePoint = (movePoint - transform.position).normalized * speed;
        characterController.SimpleMove(thisUpdatePoint);
    }

    void Rotate()
    {
        Vector3 dir = mousePointer.transform.position - transform.position;
        dir.y = 0f;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), Time.deltaTime * rotateSpeed);
    }
}