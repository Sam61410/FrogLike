using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GrenadeThrower : MonoBehaviour
{
    [SerializeField] Camera Camera;
    [SerializeField] Rigidbody Grenade;
    [SerializeField] LineRenderer LineRenderer;
    [SerializeField] Transform ReleasePosition;
    [SerializeField] [Range(1, 100)] float ThrowStrength = 10f;
    [SerializeField] [Range(1, 10)] float ExplosionDelay = 5f;
    [SerializeField] GameObject ExplosionParticleSystem;
    [SerializeField] [Range(10, 100)] int LinePoints = 25;
    [SerializeField] [Range(0.01f, 0.25f)] float TimeBetweenPoints = 0.1f;

    private Transform InitialParent;
    private Vector3 InitialLocalPosition;
    private Quaternion InitialRotation;

    public bool IsGrenadeThrowAvailable = true;
    public LayerMask GrenadeCollisionMask;

    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private int explosionDamage = 50;
    [SerializeField] private float explosionForce = 50f;

    [SerializeField] TMP_Text throwText;
    public float throwTimer;

    public void HandleLeapTimer()
    {
        if (IsGrenadeThrowAvailable)
        {
            throwText.text = "Ultimate Ready (Right Click to aim)";
        }
        else
        {
            throwTimer -= Time.deltaTime;
            throwText.text = "Ultimate: " + Mathf.Clamp(throwTimer, 0f, 2f).ToString("F2") + "s";
        }
    }

    public void Awake()
    {
        throwTimer = ExplosionDelay;
        InitialParent = Grenade.transform.parent;
        InitialRotation = Grenade.transform.localRotation;
        InitialLocalPosition = Grenade.transform.localPosition;
        Grenade.freezeRotation = true;

        int grenadeLayer = Grenade.gameObject.layer;
        for (int i = 0; i < 32; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(grenadeLayer, i))
            {
                GrenadeCollisionMask |= 1 << i; // magic
            }
        }
    }

    public void Update()
    {
        HandleLeapTimer();
        //Application.isFocused && 
        if (Mouse.current.rightButton.isPressed)
        {
            DrawProjection();

            if (Mouse.current.leftButton.wasReleasedThisFrame && IsGrenadeThrowAvailable)
            {
                IsGrenadeThrowAvailable = false;
                ReleaseGrenade();
            }
        }
        else
        {
            LineRenderer.enabled = false;
        }
    }

    public void DrawProjection()
    {
        LineRenderer.enabled = true;
        LineRenderer.positionCount = Mathf.CeilToInt(LinePoints / TimeBetweenPoints) + 1;
        Vector3 startPosition = ReleasePosition.position;
        Vector3 startVelocity = ThrowStrength * Camera.transform.forward / Grenade.mass;
        int i = 0;
        LineRenderer.SetPosition(i, startPosition);
        for (float time = 0; time < LinePoints; time += TimeBetweenPoints)
        {
            i++;
            Vector3 point = startPosition + time * startVelocity;
            point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);

            LineRenderer.SetPosition(i, point);

            Vector3 lastPosition = LineRenderer.GetPosition(i - 1);

            if (Physics.Raycast(lastPosition,
                (point - lastPosition).normalized,
                out RaycastHit hit,
                (point - lastPosition).magnitude,
                GrenadeCollisionMask))
            {
                LineRenderer.SetPosition(i, hit.point);
                LineRenderer.positionCount = i + 1;
                return;
            }
        }
    }

    public void Explode(Vector3 explosionPosition, float explosionMultiplier)
    { 
        explosionPosition = Grenade.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, (explosionRadius * explosionMultiplier));

        foreach (Collider hit in colliders)
        {
            hit.GetComponent<EnemyHealth>()?.TakeDamage(explosionDamage);
        }
    }

    public void ReleaseGrenade()
    {
        Grenade.linearVelocity = Vector3.zero;
        Grenade.angularVelocity = Vector3.zero;
        Grenade.isKinematic = false;
        Grenade.freezeRotation = false;
        Grenade.transform.SetParent(null, true);
        Grenade.AddForce(Camera.transform.forward * ThrowStrength, ForceMode.Impulse);
        throwTimer = ExplosionDelay;
        StartCoroutine(ExplodeGrenade());
    }

    public IEnumerator ExplodeGrenade()
    {
        yield return new WaitForSeconds(ExplosionDelay);

        Explode(Grenade.transform.position, 1f);
        Instantiate(ExplosionParticleSystem, Grenade.transform.position, Quaternion.identity);

        //Grenade.GetComponentInParent<Cinemachine.CinemachineImpulseSource>().GenerateImpulse(new Vector3(Random.Range(-1, 1), Random.Range(0.5f, 1), Random.Range(-1, 1)));

        Grenade.freezeRotation = true;
        Grenade.isKinematic = true;
        Grenade.transform.SetParent(InitialParent, false);
        Grenade.rotation = InitialRotation;
        Grenade.transform.localPosition = InitialLocalPosition;
        IsGrenadeThrowAvailable = true;
    }
}