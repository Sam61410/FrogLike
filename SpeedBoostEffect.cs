using UnityEngine;
using StarterAssets;

public class SpeedBoostEffect : ItemEffect
{
    public float speedMultiplier = 1.15f;
    public float speedAmount = 0;
    [SerializeField] ThirdPersonController thirdPersonController;
  
    public void Start()
    {
        thirdPersonController = FindObjectOfType<ThirdPersonController>();
        speedAmount = 0;
    }

    public override void ApplyEffect()
    {
        speedAmount++;
        thirdPersonController.MoveSpeed += Mathf.Pow(0.9f, speedAmount);
    }  
}