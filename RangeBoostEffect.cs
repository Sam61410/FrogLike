using UnityEngine;
using StarterAssets;

public class RangeBoostEffect : ItemEffect
{
    [SerializeField] private int damageIncrease = 1;
    [SerializeField] Shoot shoot;
    [SerializeField] GameObject rangeHelper;

    public void Start()
    {
        shoot = FindObjectOfType<Shoot>();
    }

    public override void ApplyEffect()
    {
        shoot.shootDistance += 3;
        rangeHelper.transform.Translate(0, 0, 3);
    }
}
