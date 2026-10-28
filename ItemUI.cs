using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;

    public void SetItem(string itemName, int quantity, Sprite icon, string itemDescription)
    {
        itemIcon.sprite = icon;
        itemNameText.text = itemName;
        quantityText.text = "x" + quantity;
        itemDescriptionText.text = itemDescription;
    }
}
