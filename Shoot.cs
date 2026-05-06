using UnityEngine;
using System;
using System.Collections;using StarterAssets;


public class Shoot : MonoBehaviour
{
    public Transform ShootPoint;
    public Transform FirePoint;
    public GameObject Fire;
    public GameObject HitPoint;

    public bool canShoot;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] int damage = 2;

    [SerializeField] float fireRate = 2f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Shooting();
        }
        if (Input.GetButtonDown("Fire2") && canShoot)
        {
            StartCoroutine(FireRoutine());
        }
    }

    public void Shooting()
    {
        RaycastHit hit;

        if (Physics.Raycast(ShootPoint.position, ShootPoint.forward, out hit, 100f))
        {
            Debug.DrawRay(ShootPoint.position, ShootPoint.forward * hit.distance, Color.red);

            hit.collider.GetComponent<EnemyHealth>()?.TakeDamage(5);

            Instantiate(HitPoint, hit.point, Quaternion.identity);
            Instantiate(Fire, FirePoint.position, Quaternion.identity);
        }
    }

    IEnumerator FireRoutine()
    {
        yield return new WaitForSeconds(fireRate);
        Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
        newProjectile.Init(damage);
    }
}