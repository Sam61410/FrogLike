using UnityEngine;

public class CooldownEffect : ItemEffect
{
    [SerializeField] private float lowerRate = 0.97f;
    [SerializeField] Dash dash;

    public void Start()
    {
        dash = FindObjectOfType<Dash>();    
    }

    public override void ApplyEffect()
    {
        dash.dashTimeOut *= lowerRate;
    }

}
