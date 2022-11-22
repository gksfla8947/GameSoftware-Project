using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour
{
    void Update()
    {
        this.transform.Translate(Vector3.back*0.1f);
        Destroy(gameObject, 2f);
    }
}
