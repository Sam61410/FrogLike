using UnityEngine;

public class OpenChest : MonoBehaviour
{
    [SerializeField] CoinManager coinManager;
    [SerializeField] Chest chest;
    public int chestCost = 25;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinManager = FindFirstObjectByType<CoinManager>();
        chest = GetComponent<Chest>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenChestMethod()
    {
        if (coinManager.coinAmount < chestCost)
        {
            return;
        }
        else
        {
            chest.BuyChest();
            coinManager.SpendCoins(chestCost);
        }
    }
}
