using StarterAssets;
using System;
using System.Collections;
using UnityEngine;
using TMPro;



public class Shoot : MonoBehaviour
{
    public Transform ShootPoint;
    public Transform FirePoint;
    public GameObject Fire;
    public GameObject HitPoint;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] int damage = 2;
    [SerializeField] float fireRate = 2f;
    [SerializeField] TMP_Text ultText;

    public bool canShoot;
    public float ultTimer = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
        {
            Debug.Log("hdadafq");
            Shooting();
        }
        if (Input.GetKeyDown(KeyCode.Q))
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

    public void HandleUltTimer()
    {
        if (ultTimer > 0f && canShoot)
        {
            ultTimer -= Time.deltaTime;
            ultText.text = "Leap (Q) : " + Mathf.Clamp(ultTimer, 0f, 2f).ToString("F2") + "s";
        }
        else
        {
            ultText.text = "Leap (Q) ready!";
        }
    }

    IEnumerator FireRoutine()
    {
        yield return new WaitForSeconds(fireRate);
        Projectile newProjectile = Instantiate(projectilePrefab, FirePoint.position, Quaternion.identity).GetComponent<Projectile>();
        newProjectile.Init(damage);
    }
}