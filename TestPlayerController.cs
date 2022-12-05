using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{

    public int moveSpeed;
    public Animator testAnimator;
    public Camera mainCamera;
    public GameObject pointer;

    Vector3 movePoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            movePoint = raycastHit.point;
            pointer.transform.position = raycastHit.point;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            testAnimator.SetBool("isStrike", true);
        }
        if(Input.GetKey(KeyCode.E))
        {
            testAnimator.SetBool("isMagic", true);
        }
        //if (!testAnimator.GetBool("isStrike") && !testAnimator.GetBool("isMagic"))
        {
            float hAxis = Input.GetAxisRaw("Horizontal");
            float vAxis = Input.GetAxisRaw("Vertical");

            Vector3 moveVec = new Vector3(hAxis, 0, vAxis).normalized;
            if (moveVec.magnitude > 0)
            {
                testAnimator.SetBool("isRun", true);
                transform.position += moveVec * moveSpeed * Time.deltaTime;
                Debug.Log(moveVec);
                Quaternion a = Quaternion.identity;
                a.SetLookRotation(moveVec);
                transform.rotation = a;
            }
            else
            {
                testAnimator.SetBool("isRun", false);
            }


        }
        


    }

    
}
