using UnityEngine;
using Unity.Cinemachine;
using StarterAssets;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private int explosionDamage = 50;
    [SerializeField] private float explosionForce = 50f;

    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private AudioClip explosionSound;

    [SerializeField] SkullPickupEffect skull;
    [SerializeField] GameManager game;

    [SerializeField] CinemachineImpulseSource impulseSource;

    public void Start()
    {
        skull = FindObjectOfType<SkullPickupEffect>();
        game = FindObjectOfType<GameManager>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void update()
    {
        if (game.enemiesExplode)
        {
            explosionRadius = game.enemyExplodeRadius;
            explosionForce = game.enemyExplodePower;
        }
    }

    public void Explode(Vector3 explosionPosition, float explosionMultiplier)
    {
        {
            impulseSource.GenerateImpulse();
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, explosionPosition, Quaternion.identity);
            }

            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, explosionPosition);
            }

            Collider[] colliders = Physics.OverlapSphere(explosionPosition, (explosionRadius * explosionMultiplier));

            foreach (Collider hit in colliders)
            {
                hit.GetComponent<EnemyHealth>()?.TakeDamage(explosionDamage);

                if (hit.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.AddExplosionForce(explosionForce, explosionPosition, (explosionRadius * explosionMultiplier));
                }
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}