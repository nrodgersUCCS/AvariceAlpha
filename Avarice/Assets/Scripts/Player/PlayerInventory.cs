using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This class is the player's inventory. Manages how to 
/// store anything the player picks up
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    //Variables for the script
    List<Item> inventoryItems;      // A list of non-throwable items in the player's inventory
    PlayerThrow playerThrow;        // Reference to the player throw script

    private float currentArmorMultiplier;     // The player's current armor multiplier
    private float currentWeaponMultiplier;    // The player's current weapon multiplier
    private int MAX_ARMOR_MULTIPLIER;       // The max armor multiplier
    private int MAX_WEAPON_MULTIPLIER;      // The max weapon multiplier
    private float GREED_PER_ARMOR;            // How much greed each armor peice gives
    private float GREED_PER_WEAPON;           // How much greed each weapon gives

    /// <summary>
    /// The stack of throwableItems
    /// </summary>
    public List<Weapon> ThrowStack { get; set; }

    /// <summary>
    /// The stack of throwableItems
    /// </summary>
    public Stack<Armor> ArmorStack { get; set; }

    /// <summary>
    /// The weight of the player's inventory
    /// </summary>
    public float CurrentWeight { get; set; }

    /// <summary>
    /// The max weight the player can carry
    /// </summary>
    public float MaxWeight { get; protected set; }

    /// <summary>
    /// How much more damage weapons should currently do
    /// </summary>
    public float GreedMultiplier { get; private set; }

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        // Initialize inventory
        inventoryItems = new List<Item>();
        ThrowStack = new List<Weapon>();
        ArmorStack = new Stack<Armor>();

        // Set constants
        MaxWeight = ((PlayerValues)Constants.Values(ContainerType.PLAYER)).MaxCarryWeight;
        MAX_ARMOR_MULTIPLIER = ((PlayerValues)Constants.Values(ContainerType.PLAYER)).MaxArmorMultiplier;
        MAX_WEAPON_MULTIPLIER = ((PlayerValues)Constants.Values(ContainerType.PLAYER)).MaxWeaponMultiplier;
        GREED_PER_ARMOR = ((PlayerValues)Constants.Values(ContainerType.PLAYER)).GreedPerArmor;
        GREED_PER_WEAPON = ((PlayerValues)Constants.Values(ContainerType.PLAYER)).GreedPerWeapon;

        // Get the player throw script
        playerThrow = GetComponent<PlayerThrow>();
        //Load the game save data
        SaveManager.Player = gameObject;
        SaveManager.Instance.LoadData();
    }

    /// <summary>
    /// Called every frame
    /// </summary>
    private void Update()
    {
        // If we have any armor, check if the top one has less than 1 hp.
        // If so break it
        if (ArmorStack.Count > 0)
        {
            if (ArmorStack.Peek().Health < 1)
            {
                ArmorStack.Pop().ArmorBreak();
                UpdateGreed();
            }
        }

        // Scroll through the stack with the scroll wheel
        if (!playerThrow.IsThrowing && ThrowStack.Count > 1)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                ThrowStack.Insert(0, ThrowStack[ThrowStack.Count - 1]);
                ThrowStack.RemoveAt(ThrowStack.Count - 1);
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                ThrowStack.Add(ThrowStack[0]);
                ThrowStack.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// Updates the greed system
    /// </summary>
    public void UpdateGreed()
    {
        // Sets the current multipliers based on how much the player is carrying
        currentArmorMultiplier = ArmorStack.Count * GREED_PER_ARMOR;
        currentWeaponMultiplier = ThrowStack.Count * GREED_PER_WEAPON;

        // Increases greed damage multiplier if player has more than 1 item
        if (ArmorStack.Count + ThrowStack.Count > 1)
        {
            // Sets the greed multiplier
            float weaponMultiplier = Mathf.Clamp(currentWeaponMultiplier, 0, MAX_WEAPON_MULTIPLIER);
            float armorMultiplier = Mathf.Clamp(currentArmorMultiplier, 0, MAX_ARMOR_MULTIPLIER);
            GreedMultiplier = 1 + weaponMultiplier + armorMultiplier;
        }
        else
            GreedMultiplier = 1;

        // Set text of UI component
        GameObject.Find("Greed Text").GetComponent<TextMeshProUGUI>().text = GreedMultiplier.ToString();
    }

    /// <summary>
    /// Adds items to the correct spot depending on what type of item it is
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item)
    {
        // If item is a weapon
        if (item is Weapon)
        {
            // Add throwable item to throw stack
            Weapon weapon = (Weapon)item;
            ThrowStack.Insert(0, weapon);
            weapon.PickedUp();
            CurrentWeight += weapon.Weight;

            // Opens the weapon's tutorial page if it hasn't been seen before
            if(weapon.Name == ItemName.DAGGER && !SaveManager.Instance.DaggerTutorialSeen)
            {
                PauseManager.Instance.CreateTutorialWindow(weapon);
                SaveManager.Instance.DaggerTutorialSeen = true;
            }
            if (weapon.Name == ItemName.SPEAR && !SaveManager.Instance.SpearTutorialSeen)
            {
                PauseManager.Instance.CreateTutorialWindow(weapon);
                SaveManager.Instance.SpearTutorialSeen = true;
            }
            if (weapon.Name == ItemName.WARHAMMER && !SaveManager.Instance.WarhammerTutorialSeen)
            {
                PauseManager.Instance.CreateTutorialWindow(weapon);
                SaveManager.Instance.WarhammerTutorialSeen = true;
            }
        }

        //This method adds to the armor stack
        else if (item is Armor)
        {
            ArmorStack.Push((Armor)item);

            // Makes armor tutorial page open if it hasn't been seen before
            if(!SaveManager.Instance.ArmorTutorialSeen)
            {
                PauseManager.Instance.CreateTutorialWindow((Armor)item);
                SaveManager.Instance.ArmorTutorialSeen = true;
            }
        }
        else  // Regular inventory item
        {
            // Add item to inventory list
            inventoryItems.Add(item);
        }

        UpdateGreed();
    }   
}
