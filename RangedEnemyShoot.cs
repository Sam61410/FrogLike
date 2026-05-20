using System.Collections;
using UnityEngine;

public class RangedEnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    //[SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetPoint;
    [SerializeField] Transform turretProjectileSpawnPoint;
    [SerializeField] int damage = 2;

    [SerializeField] float fireRate = 2f;

    public bool canShoot = true;

    PlayerHealth player;
    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
    }

    void Update()
    {
       // turretHead.LookAt(playerTargetPoint);
    }

    public IEnumerator FireRoutine()
    {
        while (player && canShoot)
        {
            canShoot = false;
            var projectile = Instantiate(projectilePrefab, turretProjectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            projectile.rb.linearVelocity = (player.transform.position - turretProjectileSpawnPoint.position).normalized * projectile.speed;
            yield return new WaitForSeconds(fireRate);
            canShoot = true;
        }
    }
}