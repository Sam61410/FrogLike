using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] public LayerMask playerLayers;
    [SerializeField] GameObject chestText;
    [SerializeField] GameObject lid;
    [SerializeField] GameObject openEffect;
    [SerializeField] GameObject[] items;
    [SerializeField] Transform spawnPosition;

    [SerializeField] OpenChest openChest;

    public int boxWidth;
    public int boxHeight;
    public int boxDepth;

    public int chestCost;

    public bool playerFound = false;
    private bool canOpen = true;

    void start()
    {
        openChest = GetComponent<OpenChest>();
        Debug.Log(openChest.chestCost);
        chestText.SetActive(false);
        canOpen = true;
    }

    void Update()
    {
        PlayerCheck();
        if (playerFound)
        {
            chestText.SetActive(true);
        }
        else if(!playerFound)
        {
            chestText.SetActive(false);
        }
        if (playerFound && Input.GetKeyDown(KeyCode.F) && canOpen)
        {
            openChest.OpenChestMethod();
        }
    }

    private void PlayerCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 playerRadius = new Vector3(boxDepth, boxHeight, boxWidth);
        playerFound = Physics.CheckBox(spherePosition, playerRadius, Quaternion.identity, playerLayers);
    } 

    public void BuyChest()
    {
        lid.transform.Rotate(90, 0, 0);
        Instantiate(openEffect, transform.position, Quaternion.Euler(0, 0, 90));
        Instantiate(items[Random.Range(0, items.Length)], spawnPosition.position, Quaternion.identity);
        canOpen = false;
    }
}
