using UnityEngine;
using System;
using System.Collections;
using StarterAssets;
using TMPro;

public class Leap : MonoBehaviour
{
    [SerializeField] ThirdPersonController playerController;

    [SerializeField] TMP_Text leapText;

    public float leapTimer = 2f;

    public bool canLeap = false;
    // Update is called once per frame
    void Update()
    {
        HandleLeapTimer();
        if (Input.GetKeyDown(KeyCode.Q) && leapTimer < 0)
        {
            StartCoroutine(LeapCoroutine());
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            StopCoroutine(LeapCoroutine());
        }
    }
    public void HandleLeapTimer()
    {
        if (leapTimer > 0f && canLeap)
        {
            leapTimer -= Time.deltaTime;
            leapText.text = "Leap (Q) : " + Mathf.Clamp(leapTimer, 0f, 2f).ToString("F2") + "s";
        }
        else
        {
            leapText.text = "Leap (Q) ready!";
        }
    }

    public void ResetLeap()
    {
        leapTimer = 2f;
    }


    IEnumerator LeapCoroutine()
    {
        playerController.Leap();
        ResetLeap();
        yield return null;
    }
}
