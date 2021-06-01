using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The player's inventory
/// </summary>
public class PlayerInventory : Inventory
{
    private GameObject playerInventory;     // The player inventory prefab object

    public GameObject Helmet;               // Item in the helmet slot
    private GameObject helmetSlot;
    public bool helmetIsFull = false;

    public GameObject Armor;                // Item in the armor slot
    private GameObject armorSlot;
    public bool armorIsFull = false;

    public GameObject LeftHand;             // Item in the left hand slot
    private GameObject leftHandSlot;
    public bool leftHandIsFull = false;

    public GameObject RightHand;            // Item in the right hand slot
    private GameObject rightHandSlot;
    public bool rightHandIsFull = false;

    private GameObject dummyUIPrefab;         // Dummy object prefab

    /// <summary>
    /// Called before start
    /// </summary>
    private void Awake()
    {
        dummyUIPrefab = Resources.Load<GameObject>("Prefabs/DummyUIObject");

        if (transform.Find("PlayerInventory") != null)
        {
            // Set player inventory
            playerInventory = transform.Find("PlayerInventory").gameObject;

            // Set weapon and armor
            helmetSlot = playerInventory.transform.Find("HelmetSlot").gameObject;
            Helmet = helmetSlot.GetComponent<ItemSlot>().GetItem();
            armorSlot = playerInventory.transform.Find("ArmorSlot").gameObject;
            Armor = armorSlot.GetComponent<ItemSlot>().GetItem();
            leftHandSlot = playerInventory.transform.Find("LeftSlot").gameObject;
            LeftHand = leftHandSlot.GetComponent<ItemSlot>().GetItem();
            rightHandSlot = playerInventory.transform.Find("RightSlot").gameObject;
            RightHand = rightHandSlot.GetComponent<ItemSlot>().GetItem();
        }
    }

    /// <summary>
    /// Adds a weapon to the left hand slot
    /// </summary>
    /// <param name="item">Item to add</param>
    public void AddLeftHand(GameObject item)
    {
        // Check if item is a weapon
        if (item.GetComponent<Weapon>() != null)
        {
            // Add item to inventory
            leftHandSlot.GetComponent<ItemSlot>().SetItem(item);
            LeftHand = item;
            GetComponent<PlayerAttack>().LeftWeapon = item.GetComponent<Weapon>();
            leftHandIsFull = true;

            // Update inventory UI
            GameObject itemUI = Instantiate<GameObject>(dummyUIPrefab, leftHandSlot.transform, false);
            itemUI.name = "LeftWeapon";
            Vector2 parentSize = new Vector2(leftHandSlot.GetComponent<RectTransform>().sizeDelta.x, leftHandSlot.GetComponent<RectTransform>().sizeDelta.y);
            itemUI.GetComponent<RectTransform>().sizeDelta = parentSize;
            Image itemImage = itemUI.AddComponent<Image>();
            itemImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
        }
    }

    /// <summary>
    /// Removes the item from the left hand slot
    /// </summary>
    public void RemoveLeftHand()
    {
        // Remove from inventory
        leftHandSlot.GetComponent<ItemSlot>().SetItem(null);
        LeftHand = null;
        GetComponent<PlayerAttack>().LeftWeapon = null;
        leftHandIsFull = false;

        // Update inventory UI
        Destroy(leftHandSlot.transform.Find("LeftWeapon").gameObject);
    }

    // TODO: Add add and remove methods for armor, helmet, and right hand
    /// <summary>
    /// Adds a weapon to the armor slot
    /// </summary>
    /// <param name="item">Item to add</param>
    public void AddArmor(GameObject item)
    {
        // Add item to inventory
        armorSlot.GetComponent<ItemSlot>().SetItem(item);
        Armor = item;
        armorIsFull= true;

        // Update inventory UI
        GameObject itemUI = Instantiate<GameObject>(dummyUIPrefab, armorSlot.transform, false);
        itemUI.name = "Armor";
        Vector2 parentSize = new Vector2(armorSlot.GetComponent<RectTransform>().sizeDelta.x, armorSlot.GetComponent<RectTransform>().sizeDelta.y);
        itemUI.GetComponent<RectTransform>().sizeDelta = parentSize;
        Image itemImage = itemUI.AddComponent<Image>();
        itemImage.sprite = item.GetComponent<Image>().sprite;
    }

    /// <summary>
    /// Removes the item from the left hand slot
    /// </summary>
    public void RemoveArmor()
    {
        // Remove from inventory
        armorSlot.GetComponent<ItemSlot>().SetItem(null);
        Armor = null;
        armorIsFull = false;

        // Update inventory UI
        Destroy(armorSlot.transform.Find("Armor").gameObject);
    }
}
