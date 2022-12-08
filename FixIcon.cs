using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixIcon : MonoBehaviour
{
    public string target;
    public float offsetY = 50f;

    private Transform targetTransform;
    // Start is called before the first frame update
    void Start()
    {
        targetTransform = GameObject.Find(target).transform;
    }

    // Update is called once per frame
    void Update()
    {
        var temp = targetTransform.position;
        temp.y = offsetY;
        this.transform.position = temp;
    }
}
