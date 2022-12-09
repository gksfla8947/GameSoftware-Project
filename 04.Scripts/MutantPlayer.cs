using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MutantPlayer : Player
{

    public Animator mutantAnimator;

    Vector3 movePoint;
    Ray ray;
    GameObject hitBox;


    public override void Awake()
    {
        base.Awake();
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

        if (Input.GetMouseButtonDown(1) && !mutantAnimator.GetBool("isAttack"))
        {
            Quaternion a = Quaternion.identity;
            a.SetLookRotation(movePoint - transform.position);
            transform.rotation = a;
            mutantAnimator.SetTrigger("Attack");

        }
        if (Input.GetKey(KeyCode.E) && !mutantAnimator.GetBool("isJumpSkill"))
        {
            Quaternion a = Quaternion.identity;
            a.SetLookRotation(movePoint - transform.position);
            transform.rotation = a;
            mutantAnimator.SetTrigger("JumpSkill");
            //Instantiate(FireField).transform.position = movePoint;
        }
        //if (!mutantAnimator.GetBool("isJumpSkill"))
        //if(Input.GetMouseButton(1))
        {

            if (moveVec.magnitude > 0)
            {
                mutantAnimator.SetBool("isRun", true);
                transform.position += moveVec * speed * Time.deltaTime;
                //Debug.Log(moveVec);
                if (!mutantAnimator.GetBool("isJumpSkill") && !mutantAnimator.GetBool("isAttack"))
                {
                    Quaternion a = Quaternion.identity;
                    a.SetLookRotation(moveVec);
                    transform.rotation = a;
                }

            }
            else
            {
                mutantAnimator.SetBool("isRun", false);
            }


        }
    }
}
