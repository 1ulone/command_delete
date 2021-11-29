using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float followspd = 10;

    private GameObject followObject;

    void Start()
    {
        followObject = GameObject.FindGameObjectWithTag("ovPlayer");
    }

    void Update()
    {
        Vector3 lfollow = new Vector3(Mathf.Lerp(transform.position.x, followObject.transform.position.x, Time.deltaTime * followspd), 
                                      Mathf.Lerp(transform.position.y, followObject.transform.position.y, Time.deltaTime * followspd),
                                      transform.position.z);

        transform.position = lfollow;
    }
}
