using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour
{
    public float bulletSpeed = 20f;
    private GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        this.transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            
            if (gm.getIsWaveStart())
            {
                gm.setKillCount(gm.getKillCount() + 1);
            }         
        }
    }
}
