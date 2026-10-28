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
    public bool lightningEnabled;

    public int damage = 5;
    public int shootDistance = 100;

    [SerializeField] GameObject lightning;
    public float lightningChance = 20f;

    public Vector3 minEuler = new Vector3(0f, 0f, 0f);
    public Vector3 maxEuler = new Vector3(0f, 10f, 10f);

    [Range(0f, 100f)]
    public float chancePercent = 25f;

    bool IsChanceSuccessful(float percent)
    {
        float roll = UnityEngine.Random.Range(0f, 100f);
        return roll < percent;
    }

    public void Start()
    {
        lightningEnabled = false;
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && canShoot)
        {
            Shooting();
            LightningEffect();
        }
    }

    public void LightningEffect()
    {
        RaycastHit hit;
        float x = 180f;
        float y = 0f;
        float z = 0f;

        if (Physics.Raycast(ShootPoint.position, ShootPoint.forward, out hit, shootDistance) && lightningEnabled)
        {
            Vector3 lightningPosition = new Vector3 (hit.transform.position.x -3, hit.transform.position.y -40f, hit.transform.position.z +1);
            if(hit.collider.CompareTag("Enemy") && IsChanceSuccessful(lightningChance))
            {
                Instantiate(lightning, lightningPosition, Quaternion.Euler(x, y, z));
                hit.collider.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            }
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
            hit.collider.GetComponent<BossHealth>()?.TakeDamage(damage);
            hit.collider.GetComponent<ChangeColor>()?.ColorChange();

            if(!hit.collider.CompareTag("Aim Helper"))
            {
                Instantiate(HitPoint, hit.point, Quaternion.identity);
                Instantiate(Fire, FirePoint.position, Quaternion.identity);
            }
        }


    }
}
