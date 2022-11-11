using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapMouseMove : MonoBehaviour
{
    public GameObject mousePointer;
    public Camera camera;
    public int speed = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1)){
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray,out raycastHit)){
                mousePointer.transform.position = raycastHit.point;
            }
        }
    }
}
