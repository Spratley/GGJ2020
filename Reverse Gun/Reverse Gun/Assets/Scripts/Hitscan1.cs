using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Hitscan1 : MonoBehaviour
{
    public Camera hitScanCam;
    public ParticleSystem bullet;
    public GameObject gun; 
    public int ammo = 6;
    public Text ammoDisplay;
    public float gunForce;
    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = ammo.ToString();

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

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(hitScanCam.transform.forward * gunForce, ForceMode.Impulse);

        }
            bullet.Emit(hitScanCam.transform.position, hitScanCam.transform.forward*100.0f, 1.0f, 1.0f, Color.red);
            ammo--;

    }

    void Reverse()
    {
        RaycastHit hit;

        if (Physics.Raycast(hitScanCam.transform.position, hitScanCam.transform.forward, out hit))
        {
            List<IShootable> ai = hit.transform.root.gameObject.GetComponentsInChildren<MonoBehaviour>().OfType<IShootable>().ToList();

            if (ai.Count() > 0)
            {
                if (ai.First().GetHealth() > 0)
                    return;

                // The gun has hit a dead object that can heal damage
                ai.First().HealDamage(10);

                bullet.Emit(hit.point, -hitScanCam.transform.forward * 100.0f, 1.0f, 1.0f, Color.red);
                ammo++;
            }
        }
    }
}
