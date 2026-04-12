using UnityEngine;
using System;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public Transform ShootPoint;
    public Transform FirePoint;
    public GameObject Fire;
    public GameObject HitPoint;

    public bool canShoot;

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && canShoot)
        {
            Shooting();
        }
    }

    public void Shooting()
    {
        RaycastHit hit;

        if(Physics.Raycast(ShootPoint.position, ShootPoint.forward, out hit, 100f))
        {
            Debug.DrawRay(ShootPoint.position, ShootPoint.forward * hit.distance, Color.red);

            hit.collider.GetComponent<EnemyHealth>()?.TakeDamage(5);

            Instantiate(HitPoint, hit.point, Quaternion.identity);
            Instantiate(Fire, FirePoint.position, Quaternion.identity);
        }
    }
}
