using UnityEngine;

public class LightningDecay : MonoBehaviour
{
    public float timeToDecay = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToDecay <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timeToDecay -= Time.deltaTime;
        }
    }
}
