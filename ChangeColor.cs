using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] Material wall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ColorChange()
    {
        GetComponent<Renderer>().material.color = Color.red;
        StartCoroutine(ResetColor());
    }

    private IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<Renderer>().material.color = wall.color;
    }
}
