using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public float sensivility;



    // Update is called once per frame
    void Update()
    {
        if(target != null)
            transform.position = Vector3.Lerp(transform.position, target.transform.position - offset, sensivility * Time.deltaTime);        
    }
}
