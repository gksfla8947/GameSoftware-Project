using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class FixIcon : MonoBehaviour
{
    public float offsetY = 50f;
    public GameObject mapIcon;

    private GameObject marker;
    private void Awake()
    {
        marker = Instantiate(mapIcon, transform.position, transform.rotation);
        marker.transform.SetParent(GameObject.Find("Object Pool").transform.GetChild(2));
    }
    // Start is called before the first frame update
    void Start()
    {
        marker.transform.Rotate(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        marker.transform.position = new Vector3(transform.position.x, offsetY, transform.position.z);
    }

    private void OnEnable()
    {
        marker.SetActive(true);
    }
    private void OnDisable()
    {
        marker.SetActive(false);
    }
}
