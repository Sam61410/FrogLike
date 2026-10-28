using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour
{
    [Header("Item Data")]
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public bool thisItemSelected;
    public GameObject selectedShader;
    public string itemDescription;

    [Header("Item Slot")]
    [SerializeField] TMP_Text quantityText;
    [SerializeField] Image itemImage;

    private InventoryManager inventoryManager;
    [Header("Item Description")]
    public Image itemDescriptionImage;
    [SerializeField] TMPro.TextMeshProUGUI itemDescriptionNameText;
    [SerializeField] TMPro.TextMeshProUGUI itemDescriptionText;

    public void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
    }

}
