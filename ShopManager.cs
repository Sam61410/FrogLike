using UnityEngine;
using StarterAssets;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Shoot shoot;
    [SerializeField] ThirdPersonController controller;
    [SerializeField] StarterAssetsInputs starterAssets;
    [SerializeField] Dash dash;
    [SerializeField] Leap leap;
    [SerializeField] PlayerHealth health;
    [SerializeField] WaveManager waveManager;
    [SerializeField] GameManager gameManager;
    [SerializeField] CoinManager coin;

    [SerializeField] int healCost = 50;
    [SerializeField] int damageCost = 50;
    [SerializeField] int speedCost = 50;
    [SerializeField] int leapCost = 50;
    [SerializeField] int dashCost = 50;
    [SerializeField] int rangeCost = 50;

    [SerializeField] GameObject rangeHelper;
    [SerializeField] GameObject shopBackground;

    public void OpenShop()
    {
        controller.LockCameraPosition = true;
        Cursor.lockState = CursorLockMode.None;
        shopBackground.SetActive(true);
        waveManager.canSpawn = false;
        shoot.canShoot = false;
        leap.canLeap = false;
        dash.canDash = false;
        controller.canMove = false;
        gameManager.ActivateShop();
    }

    public void CloseShop()
    {
        Cursor.lockState = CursorLockMode.Locked;
        shopBackground.SetActive(false);
        waveManager.canSpawn = true;
        waveManager.StartCoroutine(waveManager.ShopRoutine());
        shoot.canShoot = true;
        leap.canLeap = true;
        dash.canDash = true;
        controller.canMove = true;
        controller.LockCameraPosition = false;
        gameManager.DeactivateShop();
    }

    public void Heal()
    {
        if (health.currentHealth >= health.maxHealth)
        {
            Debug.Log("Health is already full!");
            return;
        }
        if (coin.coinAmount < healCost)
        {
            Debug.Log("Not enough coins to heal!");
            return;
        }
        else
        {
            health.Heal(health.maxHealth * .05f);
            coin.SpendCoins(healCost);
        }
    }
    public void IncreaseDamage()
    {
        if (coin.coinAmount < damageCost)
        {
            Debug.Log("Not enough coins to increase damage!");
            return;
        }
        else
        {
            shoot.damage += 1;
            coin.SpendCoins(damageCost);
        }
    }
    public void IncreaseSpeed()
    {
        if (coin.coinAmount < speedCost)
        {
            Debug.Log("Not enough coins to increase speed!");
            return;
        }
        else
        {
            controller.MoveSpeed += 1;
            coin.SpendCoins(speedCost);
        }
    }
    public void IncreaseLeap()
    {
        if (coin.coinAmount < leapCost)
        {
            Debug.Log("Not enough coins to increase leap!");
            return;
        }
        else
        {
            controller.LeapPower += 1;
            coin.SpendCoins(leapCost);
        }
    }
    public void IncreaseDash()
    {
        if (coin.coinAmount < dashCost)
        {
            Debug.Log("Not enough coins to increase dash!");
            return;
        }
        else
        {
            controller.DashSpeed += 1;
            coin.SpendCoins(dashCost);
        }
    }
    public void IncreaseRange()
    {
        if (coin.coinAmount < rangeCost)
        {
            Debug.Log("Not enough coins to increase range!");
            return;
        }
        else
        {
            shoot.shootDistance += 3;
            coin.SpendCoins(rangeCost);
            rangeHelper.transform.Translate(0, 0, 3);
        }
    }
}
