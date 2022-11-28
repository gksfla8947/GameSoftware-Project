using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : LivingEntity
{
    public float speed = 5.0f; 
    public float rotateSpeed = 10.0f;
    public CharacterController characterController; 
    public Camera mainCamera; 
    public GameObject mousePointer;

    public float attackRate = 0.5f;

    public GameObject Bullet;
    public Transform FirePos;

    private Animator playerAnimator;
    private bool isIdle;
    private bool isAttack;

    Vector3 movePoint;
    float curTime = 0f;

    void Start()
    {
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        movePoint = transform.position;
        isIdle = true;
        isAttack = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            PointMousePos();
        }
        if (Input.GetKeyDown("q"))
        {
            isAttack = true;
        }
        if(Input.GetKey("q"))
        {
            if(curTime > attackRate)
            {
                BulletFire();
                curTime = 0f;
            }
            curTime += Time.deltaTime;
        }
        if (Input.GetKeyUp("q"))
        {
            isAttack = false;
        }
        Move();
        playerAnimator.SetBool("isIdle", isIdle);
        playerAnimator.SetBool("isAttack", isAttack);
    }

    void PointMousePos()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            movePoint = raycastHit.point;
            mousePointer.transform.position = raycastHit.point;
        }
        
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, movePoint) > 0.1f)
        {
            isIdle = false;
            Vector3 thisUpdatePoint = (movePoint - transform.position).normalized * speed;
            characterController.SimpleMove(thisUpdatePoint);
            Vector3 dir = mousePointer.transform.position - transform.position;
            dir.y = 0f;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), Time.deltaTime * rotateSpeed);
        }
        else
        {
            isIdle = true;
        }
    }

    void BulletFire()
    {
        Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();
    }
}