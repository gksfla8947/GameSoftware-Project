using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{

    public int moveSpeed;
    public Animator testAnimator;
    public Camera mainCamera;
    public GameObject pointer;
    public GameObject FireField;

    Vector3 movePoint;
    Ray ray;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        pointer = GameObject.Find("Pointer");
        GameObject.Find("SM_Wep_Sword_01").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 300.0f, 1<<8))
        {
            movePoint = raycastHit.point;
            pointer.transform.position = raycastHit.point;
        }

        if (Input.GetMouseButtonDown(1) && !testAnimator.GetBool("isStrike"))
        {
            Quaternion a = Quaternion.identity;
            a.SetLookRotation(movePoint-transform.position);
            transform.rotation = a;
            testAnimator.SetBool("isStrike", true);

        }
        if(Input.GetKey(KeyCode.E) && !testAnimator.GetBool("isMagic"))
        {
            Quaternion a = Quaternion.identity;
            a.SetLookRotation(movePoint - transform.position);
            transform.rotation = a;
            testAnimator.SetBool("isMagic", true);
            Instantiate(FireField).transform.position = movePoint;
        }
        if (!testAnimator.GetBool("isMagic"))
        //if(Input.GetMouseButton(1))
        {
            
            if (moveVec.magnitude > 0)
            {
                testAnimator.SetBool("isRun", true);
                transform.position += moveVec * moveSpeed * Time.deltaTime;
                Debug.Log(moveVec);
                if (!testAnimator.GetBool("isMagic"))
                {
                    Quaternion a = Quaternion.identity;
                    a.SetLookRotation(moveVec);
                    transform.rotation = a;
                }
                    
            }
            else
            {
                testAnimator.SetBool("isRun", false);
            }


        }
        


    }

    
}
