using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);
        transform.rotation = rotation;
    }
}
