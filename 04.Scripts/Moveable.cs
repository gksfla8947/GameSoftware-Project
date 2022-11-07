using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.AI;

public class Moveable : MonoBehaviour
{
    public Transform target;
    public Camera camera;
    public GameObject mousePoint;
    NavMeshAgent agent;
    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycasthit;
            if(Physics.Raycast(ray,out raycasthit))
            {
                mousePoint.transform.position = raycasthit.point;
                agent.SetDestination(mousePoint.transform.position);
            }
        }
    }
}
