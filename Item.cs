using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] public string itemDescription;
    [SerializeField] public Sprite itemIcon;
    [SerializeField] public int quantity;
    [SerializeField] public ItemEffect itemEffect; 

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            inventoryManager.AddItem(itemName, quantity, itemIcon, itemEffect, itemDescription);
            Destroy(this.gameObject);
        }
    }
}
