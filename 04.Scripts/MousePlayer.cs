using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MousePlayer : Player
{

    public Animator mouseAnimator;
    public GameObject FireField;

    Vector3 movePoint;
    Ray ray;
    GameObject sword;

    public override void Awake()
    {
        base.Awake();
        sword = GameObject.Find("SM_Wep_Sword_01");
        sword.SetActive(false);
        mainCamera = Camera.main;
        mouseAnimator = this.GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();

        ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 300.0f, 1 << 8))
        {
            movePoint = raycastHit.point;
            mousePointer.transform.position = raycastHit.point;
        }

        if (Input.GetMouseButtonDown(1) && !mouseAnimator.GetBool("isStrike"))
        {
            Quaternion a = Quaternion.identity;
            a.SetLookRotation(movePoint - transform.position);
            transform.rotation = a;
            sword.SetActive(true);
            mouseAnimator.SetBool("isStrike", true);

        }
        if (Input.GetKey(KeyCode.E) && !mouseAnimator.GetBool("isMagic"))
        {
            Quaternion a = Quaternion.identity;
            a.SetLookRotation(movePoint - transform.position);
            transform.rotation = a;
            mouseAnimator.SetBool("isMagic", true);
            Instantiate(FireField).transform.position = movePoint;
        }
        if (!mouseAnimator.GetBool("isMagic"))
        //if(Input.GetMouseButton(1))
        {

            if (moveVec.magnitude > 0)
            {
                mouseAnimator.SetBool("isRun", true);
                transform.position += moveVec * speed * Time.deltaTime;
                //Debug.Log(moveVec);
                if (!mouseAnimator.GetBool("isMagic") && !mouseAnimator.GetBool("isStrike"))
                {
                    Quaternion a = Quaternion.identity;
                    a.SetLookRotation(moveVec);
                    transform.rotation = a;
                }

            }
            else
            {
                mouseAnimator.SetBool("isRun", false);
            }


        }



    }
    public void EndAttack()
    {
        sword.SetActive(false);
    }
}

    
    /*
     * public AudioClip DmgClip;

    public float health = 100f; //?????? ??????
    public float curHealth; //?????? ??????
    public bool dead { get; protected set; } //????????????

    public GameObject hitEffect;
    public GameObject dyingEffect;

    protected Slider HP_slider;//??? ????????? ?????????(????????????)

    public virtual void Awake()
    {
        //???????????? ?????? ????????? ??????
        dead = false;
        //?????? ?????????
        curHealth = health;
        HP_slider = GetComponentInChildren<Slider>();//???
    }

    public virtual void Update()
    {
        HP_slider.value = curHealth / health;//???
    }

    // ????????? ?????? ??????
    //OnDamage(?????????, ???????????? ??????, ???????????? ????????? ??????)
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        curHealth -= damage;

        if (curHealth <= 0)
        {
            dead = true;
            Die();
        }
        Instantiate(hitEffect, hitPoint, transform.rotation);
    }

    //?????? ??????
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
        {
            return;
        }
        Debug.Log("heal");
        curHealth += newHealth;

        if (curHealth > health)
        {
            curHealth = health;
        }
    }

    //????????????
    public virtual void Die()
    {
        Instantiate(dyingEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    public virtual void OnDestroy()
    {
    }
     * 
     * 
     * 
     * 
     * 
     * public float speed = 5.0f;
    public Camera mainCamera;
    public GameObject mousePointer;
    public int atk = 20;
    public float attackRate = 0.5f;
    public float coefIncAttackRate = 0.8f;
    public float fasterAttackTime = 3f;

    public override void Awake()
    {
        base.Awake();
        mainCamera = GameObject.Find("MainCameraPos").GetComponentInChildren<Camera>();
        mousePointer = GameObject.Find("Pointer");
        transform.SetParent(GameObject.Find("Player").transform);
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

    public virtual void UseActiveItem()
    {
        if (ItemManager.instance.SlotItems[0] != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ItemManager.instance.ActiveSlotItem(0);
                ItemManager.instance.ClearActiveItem(0);
            }
        }
        if (ItemManager.instance.SlotItems[1] != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ItemManager.instance.ActiveSlotItem(1);
                ItemManager.instance.ClearActiveItem(1);
            }
        } 
    }
     */
    // Start is called before the first frame update
