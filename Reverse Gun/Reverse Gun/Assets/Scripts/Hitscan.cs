using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitscan : MonoBehaviour
{
    public Camera hitScanCam;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Hit();
        }
    }

    void Hit()
    {
        RaycastHit hit;
        if(Physics.Raycast(hitScanCam.transform.position, hitScanCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
        }

    }
}
