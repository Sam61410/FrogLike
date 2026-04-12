using UnityEngine;
using System;
using System.Collections;
using StarterAssets;
using TMPro;

public class Dash : MonoBehaviour
{
    [SerializeField] ThirdPersonController playerController;
    [SerializeField] TMP_Text dashText;

    public float dashTimer = 1f;

    public bool canDash = false;
    // Update is called once per frame
    void Update()
    {
        HandleDashTimer();
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0f)
        {
            StartCoroutine(DashCoroutine());
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopCoroutine(DashCoroutine());
        }
    }
    public void HandleDashTimer()
    {
        if (dashTimer > 0f && canDash)
        {
            dashTimer -= Time.deltaTime; 
            dashText.text = "Dash (Left Shift) : " + Mathf.Clamp(dashTimer, 0f, 1f).ToString("F2") + "s";
        }
        else
        {
            dashText.text = "Dash (Left Shift) ready!";
        }
    }

    public void ResetDash()
    {
        dashTimer = 1f;
    }

    IEnumerator DashCoroutine()
    {
        playerController.Dash();
        ResetDash();
        yield return null;
    }
}
