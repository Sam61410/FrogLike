using UnityEngine;

public class LeapCoolDownEffect : ItemEffect
{
    [SerializeField] private float lowerRate = 0.95f;
    [SerializeField] Leap leap;

    public void Start()
    {
        leap = FindObjectOfType<Leap>();
    }

    public override void ApplyEffect()
    {
        leap.leapTimeOut *= lowerRate;
    }
}
