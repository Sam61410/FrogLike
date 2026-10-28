using UnityEngine;

public class SkullPickupEffect : ItemEffect
{
    [SerializeField] private float multiplierRate = 1.10f;
    [SerializeField] ExplosionController explosion;
    [SerializeField] EnemyHealth enemy;
    [SerializeField] GameManager game;
    [SerializeField] Item item;
    [SerializeField] InventoryManager inventory;

    public void Start()
    {
        explosion = FindObjectOfType<ExplosionController>();
        game = FindObjectOfType<GameManager>();
        enemy = FindObjectOfType<EnemyHealth>();
        item = FindObjectOfType<Item>();
        inventory = FindObjectOfType<InventoryManager>();
    }

    public override void ApplyEffect()
    {
        if(item.quantity > 0)
        {
            game.enemiesExplode = true;
            game.enemyExplodePower *= (item.quantity * multiplierRate);
            game.enemyExplodeRadius *= (item.quantity * multiplierRate);
        }
    }
}
