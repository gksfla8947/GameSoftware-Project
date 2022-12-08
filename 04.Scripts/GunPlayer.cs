using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlayer : Player
{
    public float rotateSpeed = 10.0f;

    public GameObject BulletFactory;
    public Transform FirePos;
    public GameObject FireEffect;
    public Animator playerAnimator;

    private bool isIdle;
    private bool isAttack;

    Vector3 movePoint;
    float attackCurTime = 0f;
    float fasterAttackCurTime = 0f;
    float fasterAttackRate = 0.2f;

    public override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
        playerAnimator = GetComponent<Animator>();
        movePoint = transform.position;
        isIdle = true;
        isAttack = false;
    }

    public override void Update()
    {
        base.Update();
        base.UseActiveItem();
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
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), Time.deltaTime * rotateSpeed);
        }

        UpdatePlayerAnimation();
    }
    void PointMousePos()
    {
        Vector3 d = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 300.0f, 3))
        {
            movePoint = raycastHit.point;
            mousePointer.transform.position = raycastHit.point;
        }
    }

    void Move(Vector3 dir)
    {
        if (dir.magnitude != 0)
        {
            isIdle = false;
            transform.position += dir * speed * Time.deltaTime;
        }
        else
        {
            isIdle = true;
        }
    }

    public void Attack(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            isAttack = true;
        }
        if (Input.GetKey(key))
        {
            PointMousePos();
            if (fasterAttackCurTime > fasterAttackTime)
            {
                if (attackCurTime > fasterAttackRate)
                {
                    BulletFire();
                    attackCurTime = 0f;
                }
            }
            if (attackCurTime > attackRate)
            {
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
        if (isAttack && isIdle)
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
        GameObject bullet = Instantiate(BulletFactory, FirePos.transform.position, FirePos.transform.rotation);
        bullet.transform.SetParent(GameObject.Find("Object Pool").transform.GetChild(1));
        /*
        GameObject bullet = ObjectPool.GetObject();
        var direction = movePoint - FirePos.position;
        direction.y = 0f;
        bullet.transform.position = direction.normalized;
        bullet.GetComponent<PlayerBullet>().Shoot(direction.normalized);
        */
    }
}
