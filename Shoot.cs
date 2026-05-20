using UnityEngine;
using System;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform ShootPoint;
    public Transform FirePoint;
    public GameObject Fire;
    public GameObject HitPoint;

    public bool canShoot;

    public int damage = 5;
    public int shootDistance = 100;

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && canShoot)
        {
            Shooting();
        }
        if(Input.GetKeyUp(KeyCode.E) && canShoot)
         {
            //Ultimate();
        }
    }

    public void Ultimate()
    {
        RaycastHit hit;
        if (bulletPrefab != null && ShootPoint != null)
        {
            Instantiate(bulletPrefab, ShootPoint.position, ShootPoint.rotation);
            bulletPrefab.GetComponent<Rigidbody>().angularVelocity = ShootPoint.forward * 100000f;
            bulletPrefab.GetComponent<Rigidbody>().linearVelocity = ShootPoint.forward * 100000f;
        }
      
    }

    public void Shooting()
    {
        RaycastHit hit;

        if(Physics.Raycast(ShootPoint.position, ShootPoint.forward, out hit, shootDistance))
        {
            Debug.DrawRay(ShootPoint.position, ShootPoint.forward * hit.distance, Color.red);

            hit.collider.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            hit.collider.GetComponent<DoorHealth>()?.TakeDamage(damage);
            hit.collider.GetComponent<TutorialDoorHealth>()?.TakeDamage(damage);
            hit.collider.GetComponent<TutorialEnemyHealth>()?.TakeDamage(damage);
            hit.collider.GetComponent<TutorialEndButton>()?.TakeDamage(damage);
            hit.collider.GetComponent<ChangeColor>()?.ColorChange();

            if(!hit.collider.CompareTag("Aim Helper"))
            {
                Instantiate(HitPoint, hit.point, Quaternion.identity);
                Instantiate(Fire, FirePoint.position, Quaternion.identity);
            }
        }


    }
}
