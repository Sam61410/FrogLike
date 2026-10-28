using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    public bool menuActivated = false;

    [SerializeField] Shoot shoot;
    [SerializeField] ThirdPersonController controller;
    [SerializeField] StarterAssetsInputs starterAssets;
    [SerializeField] PlayerHealth health;
    [SerializeField] WaveManager waveManager;
    [SerializeField] GameManager gameManager;
    [SerializeField] Dash dash;
    [SerializeField] Leap leap;
    [SerializeField] CoinManager coin;

    [SerializeField] private Transform inventoryContent;
    [SerializeField] private GameObject inventoryItemUIPrefab;

    public List<InventoryItem> inventory = new List<InventoryItem>();

    public ItemSlot[] itemSlot;

    public bool thisItemSelected;
    public GameObject selectedShader;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && menuActivated)
        {
            Time.timeScale = 1f;
            inventoryMenu.SetActive(false);
            menuActivated = false;

            controller.LockCameraPosition = false;
            Cursor.lockState = CursorLockMode.Locked;
            shoot.canShoot = true;
            leap.canLeap = true;
            dash.canDash = true;
            controller.canMove = true;
        }

        else if (Input.GetKeyDown(KeyCode.E) && !menuActivated)
        {
            Time.timeScale = 0f;
            inventoryMenu.SetActive(true);
            menuActivated = true;

            controller.LockCameraPosition = true;
            Cursor.lockState = CursorLockMode.None;
            shoot.canShoot = false;
            leap.canLeap = false;
            dash.canDash = false;
            controller.canMove = false;
        }
    }

    public void AddItem(string itemName, int quantity, Sprite itemIcon, ItemEffect itemEffect, string itemDescription)
    {
        InventoryItem existingItem = inventory.Find(item => item.itemName == itemName);

        if (existingItem != null)
        {
            existingItem.quantity += quantity;
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemName, quantity, itemIcon, itemDescription);
            inventory.Add(newItem);
        }

        // Apply the effect when item is added
        if (itemEffect != null)
        {
            itemEffect.ApplyEffect();
        }
        RefreshInventoryDisplay();
    }

    private void RefreshInventoryDisplay()
    {
        // Clear old items
        foreach (Transform child in inventoryContent)
        {
            Destroy(child.gameObject);
        }

        // Add new items
        foreach (InventoryItem item in inventory)
        {
            GameObject itemSlot = Instantiate(inventoryItemUIPrefab, inventoryContent);
            ItemSlot uiScript = itemSlot.GetComponent<ItemSlot>();
            uiScript.AddItem(item.itemName, item.quantity, item.itemIcon, item.itemDescription);
        }
    }
    public void DeselectAllSlots()
    {
        for (int i = 0; i< itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

    [System.Serializable]
    public class InventoryItem
    {
        public string itemName;
        public string itemDescription;
        public int quantity;
        public Sprite itemIcon;

        public InventoryItem(string name, int qty, Sprite icon, string description)
        {
            itemName = name;
            itemDescription = description;
            quantity = qty;
            itemIcon = icon;
        }
    }
}