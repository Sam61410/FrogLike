using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class RangedEnemyAI : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform turretProjectileSpawnPoint;
    [SerializeField] int damage = 2;

    [SerializeField] float fireRate = 2f;

    public bool canShoot = true;
    public bool canTarget = true;

    [SerializeField] ThirdPersonController player;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] RangedEnemyShoot rangedEnemyShoot;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] public LayerMask PlayerLayers;
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] GameObject projectile;
    WaveManager waveManager;

    public Transform[] waypoints;
    [SerializeField] Slider healthBar;

    public float waypointTolerance = 0.5f;

    public int currentPointIndex = 0;
    private int currentWaypointIndex = 0;

    public int boxWidth;
    public int boxHeight;
    public int boxDepth;

    public bool playerFound = false;
    public bool waited = false;
    public bool loopPatrol = true;
    public bool shouldWait = false;

    public float cooldownTime = 0.5f; // Seconds between shots
    private float nextFireTime = 0f;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<ThirdPersonController>();
        waveManager = FindFirstObjectByType<WaveManager>();
        canShoot = true;
        canTarget = false;
    }

    public void Update()
    {
        nextFireTime -= Time.deltaTime; 
        PlayerCheck();
        if (playerFound)
        {
            agent.SetDestination(player.transform.position);
            transform.LookAt(agent.destination);
            StartCoroutine(FireRoutine());
        }
        else if (!playerFound)
        {
            StopAllCoroutines();
            if (!agent.pathPending && agent.remainingDistance <= waypointTolerance && !playerFound)
            {
                AdvanceWaypoint();
                SetNextDestination();
            }
            else return;
        }
    }
    public void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (playerFound) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 playerRadius = new Vector3(boxDepth, boxHeight, boxWidth);
        Gizmos.DrawCube(
        new Vector3(transform.position.x, transform.position.y, transform.position.z), playerRadius);
    }

    private void PlayerCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 playerRadius = new Vector3(boxDepth, boxHeight, boxWidth);
        playerFound = Physics.CheckBox(spherePosition, playerRadius, Quaternion.identity, PlayerLayers);
    }

    public void Start()
    {
        player = FindFirstObjectByType<ThirdPersonController>();
        currentWaypointIndex = UnityEngine.Random.Range(0, waypoints.Length);
        if (waypoints == null || waypoints.Length == 0)
        {
            enabled = false;
            return;
        }
        SetNextDestination();
    }
    public void SetNextDestination()
    {
        if (waypoints.Length == 0) return;
        agent.acceleration = 5;
        agent.speed = (5f * Mathf.Sqrt(waveManager.currentWaveIndex + 1));
        //  animator.speed = 1;

        Transform targetWaypoint = waypoints[currentWaypointIndex];

        if (waited = true && targetWaypoint != null)
        {
            waited = false;
            shouldWait = true;
            agent.SetDestination(targetWaypoint.position);
            // animator.Play("Crawl");
        }
    }
    private void AdvanceWaypoint()
    {
        currentWaypointIndex++;
        if (currentWaypointIndex >= waypoints.Length)
        {
            if (loopPatrol)
            {
                currentWaypointIndex = 0;
            }
            else
            {
                enabled = false;
            }
        }
    }
    public IEnumerator FireRoutine()
    {
        while (player && 0 >= nextFireTime)
        {
            var projectile = Instantiate(projectilePrefab, turretProjectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            projectile.rb.linearVelocity = (player.transform.position - turretProjectileSpawnPoint.position).normalized * projectile.speed;
            nextFireTime = (cooldownTime * 1.5f * Mathf.Sqrt(waveManager.currentWaveIndex + 1));
            yield return new WaitForSeconds(cooldownTime);
        }
    }
}