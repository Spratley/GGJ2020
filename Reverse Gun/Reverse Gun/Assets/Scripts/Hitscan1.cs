using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Hitscan1 : MonoBehaviour
{
    public Camera hitScanCam;
    public ParticleSystem bullet;
    public ParticleSystem reversebullet;
    public GameObject gun; 
    public int ammo = 6;
    public Text ammoDisplay;
    // Update is called once per frame
    void Update()
    {
        //ammoDisplay.text = ammo.ToString();
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
            List<IShootable> ai = hit.transform.root.gameObject.GetComponentsInChildren<MonoBehaviour>().OfType<IShootable>().ToList();
            
            if(ai.Count() > 0)
            {
                // The gun has hit an object that can take damage
                ai.First().TakeDamage(10);
            }

        }
            bullet.Emit(gun.transform.position, hitScanCam.transform.forward*100.0f, 1.0f, 1.0f, Color.red);
            ammo--;

    }

    void Reverse()
    {
        RaycastHit hit;

        if (Physics.Raycast(hitScanCam.transform.position, hitScanCam.transform.forward, out hit))
        {

            reversebullet.Emit(hit.point, (gun.transform.position - hit.point).normalized * 100.0f, 1.0f, 1.0f, Color.red);
            ammo++;
        }
    }
}
