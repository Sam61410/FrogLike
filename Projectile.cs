using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    [SerializeField] Transform playerTargetPoint;
    [SerializeField] Transform player;
    [SerializeField] GameObject projectileHitVFX;
    [SerializeField] PlayerHealth playerHealth;

    public Rigidbody rb;

    int damage = 10;
    public float dieTimer = 1.5f;

    void Awake()
    {
        player = FindFirstObjectByType<PlayerHealth>().transform;
        rb = GetComponent<Rigidbody>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    void Update()
    {
        dieTimer -= Time.deltaTime;
        if (dieTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Init(int damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Instantiate(projectileHitVFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Instantiate(projectileHitVFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}