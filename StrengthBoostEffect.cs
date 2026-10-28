using UnityEngine;
using StarterAssets;

public class StrengthBoostEffect : ItemEffect
{
    [SerializeField] private int damageIncrease = 1;
    [SerializeField] Shoot shoot;

    public void Start()
    {
        shoot = FindObjectOfType<Shoot>();
    }

    public override void ApplyEffect()
    {
        shoot.damage += damageIncrease;
    }
}