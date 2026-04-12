using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEditor.PackageManager;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] ThirdPersonController player;
    [SerializeField] NavMeshAgent agent;
//    [SerializeField] Animator animator;
    //[SerializeField] ObjectGrab objectGrab;
    [SerializeField] public LayerMask PlayerLayers;
    [SerializeField] EnemyHealth enemyHealth;

    public Transform[] waypoints;
    [SerializeField] Slider healthBar;

    //   [SerializeField] float playerRadius = 0.28f;
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

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<ThirdPersonController>();
    }

    public void Update()
    {
            PlayerCheck();
            if (playerFound)
            {
                agent.SetDestination(player.transform.position);
                agent.acceleration = 7;
                agent.speed = 5;
                // animator.speed = 2;
            }
            else
            {
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
        if (waypoints == null || waypoints.Length == 0)
        {
            enabled = false;
            return;
        }
        SetNextDestination();
    }
    private void SetNextDestination()
    {
        if (waypoints.Length == 0) return;
        agent.acceleration = 5;
        agent.speed = 3.5f;
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

}
