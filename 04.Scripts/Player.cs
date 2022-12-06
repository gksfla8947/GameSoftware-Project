using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : LivingEntity
{
    public float speed = 5.0f; 
    public float rotateSpeed = 10.0f;
    public Camera mainCamera; 
    public GameObject mousePointer;

    public int atk = 20;

    public float attackRate = 0.5f;
    public float coefIncAttackRate = 0.8f;
    public float fasterAttackTime = 3f;

    public GameObject BulletFactory;
    public Transform FirePos;
    public GameObject FireEffect;

    private Animator playerAnimator;
    private bool isIdle;
    private bool isAttack;

    Vector3 movePoint;
    float attackCurTime = 0f;
    float fasterAttackCurTime = 0f;
    int flag = 0;
    private void Awake()
    {
        mainCamera = Camera.main;
        playerAnimator = GetComponent<Animator>();
        movePoint = transform.position;
        isIdle = true;
        isAttack = false;
    }
    void Start()
    {     
    }

    void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        Vector3 dir = moveVec;
        Move(moveVec);
        Attack(KeyCode.Mouse1);
        if (isAttack) 
        { 
            dir = mousePointer.transform.position - transform.position; 
        }
        dir.y = 0f;
        if(dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), Time.deltaTime * rotateSpeed);
        }

        UpdatePlayerAnimation();
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

    void Move(Vector3 dir)
    {
        if(dir.magnitude != 0)
        {
            isIdle = false;
            transform.position += dir * speed * Time.deltaTime;
        }
        else
        {
            isIdle = true;
        }
    }

    void Attack(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            isAttack = true;
        }
        if (Input.GetKey(key))
        {
            PointMousePos();
            if(fasterAttackCurTime > fasterAttackTime)
            {
                if (attackCurTime > attackRate * coefIncAttackRate)
                {
                    if(flag == 0)
                    {
                        attackRate *= coefIncAttackRate;
                        flag = 1;
                    }
                    BulletFire();
                    attackCurTime = 0f;
                }
            }
            if (attackCurTime > attackRate)
            {
                if(flag == 1)
                    {
                        attackRate = 0.5f;
                        flag = 0;
                    }
                BulletFire();
                attackCurTime = 0f;
            }
            attackCurTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(key))
        {
            isAttack = false;
        }
    }
    void UpdatePlayerAnimation()
    {
        playerAnimator.SetBool("isIdle", isIdle);
        playerAnimator.SetBool("isAttack", isAttack);
        if(isAttack && isIdle)
        {
            fasterAttackCurTime += Time.deltaTime;
            playerAnimator.SetFloat("fasterShoot", fasterAttackCurTime);
        }
        else
        {
            fasterAttackCurTime = 0f;
        }
        if (dead)
        {
            playerAnimator.SetTrigger("Die");
        }
    }
    void BulletFire()
    {
        Instantiate(FireEffect, FirePos.transform.position, FirePos.transform.rotation);
        Instantiate(BulletFactory, FirePos.transform.position, FirePos.transform.rotation);
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();
        Time.timeScale = 0;
    }
}