using UnityEngine;

public class StarEffect : ItemEffect
{
    public int starMultiplier = 1;
    [SerializeField] CoinManager coin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coin = FindObjectOfType<CoinManager>();
    }

    public override void ApplyEffect()
    {
        coin.CoinMultiply(starMultiplier);
    }
}
