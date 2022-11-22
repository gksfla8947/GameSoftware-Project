using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public GameObject Bullet;
    public Transform FirePos;

    private void Update()
    {
        if(Input.GetKeyDown("q"))
        {
            Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
            //Destroy(gameObject, 2f);
        }
    }

}