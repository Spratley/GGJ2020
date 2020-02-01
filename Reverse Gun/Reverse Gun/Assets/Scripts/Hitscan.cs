using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitscan : MonoBehaviour
{
    public Camera hitScanCam;
    public ParticleSystem bullet;
    public int ammo = 6;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && ammo > 0)
        {
            Hit();
        }

        if(Input.GetButtonDown("Fire2")&& ammo < 6)
        {
            Reverse();
        }
    }

    void Hit()
    {
        RaycastHit hit;
        if(Physics.Raycast(hitScanCam.transform.position, hitScanCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            bullet.Emit(hitScanCam.transform.position, hitScanCam.transform.forward*100.0f, 1.0f, 1.0f, Color.red);
            ammo--;
        }

    }

    void Reverse()
    {
        RaycastHit hit;

        if (Physics.Raycast(hitScanCam.transform.position, hitScanCam.transform.forward, out hit))
        {

            bullet.Emit(hit.point, -hitScanCam.transform.forward * 100.0f, 1.0f, 1.0f, Color.red);
            ammo++;
        }
    }
}
