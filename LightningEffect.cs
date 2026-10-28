using UnityEngine;

public class LightningEffect : ItemEffect
{
    [SerializeField] Shoot shoot;

    private int lightningCount = 0;

    public void Start()
    {
        shoot = FindObjectOfType<Shoot>();
    }

    public override void ApplyEffect()
    {
        lightningCount++;
        if (lightningCount >= 1)
        {
            shoot.lightningEnabled = true;
        }
        else
        {
            shoot.lightningChance += 5f;
        }
    }
}
