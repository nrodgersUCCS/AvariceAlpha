using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This is a struct to hold a weapon's index in the stack and its name when it is up
/// </summary>
public struct WeaponUpForTrade
{
    public int indexInStack;
    public ItemName nameOfWeapon;

    public WeaponUpForTrade(int index, ItemName name)
    {
        this.indexInStack = index;
        this.nameOfWeapon = name;
    }
}

/// <summary>
/// This script deals with checking if the stack got updated and if so it tells the canvas to update so that the sprite in the HUD
/// changes accordingly.
/// </summary>
public class HUDUpdater : MonoBehaviour
{
    // Constants
    float MIN_STACK_DISTANCE;                       // The minimum distance between icons in the stack
    float MAX_STACK_DISTANCE;                       // The maximum distance between icons in the stack
    float STACK_DISTANCE_AMOUNT;                    // The value to change the distance between stack icons by

    // Variables
    PlayerThrow playerThrow;                        // The player Throw script so we can check if there is a decoration in the players hands
    List<Weapon> throwStack;                        // The throwing stack to check what is in the stack
    List<Weapon> throwList;                         // The list version of the throw stack
    Stack<Armor> armorStack;                        // The armor stack to check what is on the stack
    List<Armor> armorList;                          // The list version of the armor stack
    bool isPlayerHoldingDecoration;                 // This keeps track of the player holding a decoration
    [SerializeField]
    Image weaponBackground = null;                  // The background icon for weapons
    [SerializeField]
    Image armorBackground = null;                   // The background icon for armor
    [SerializeField]
    Canvas theHUD = null;                           // The canvas attached to the player
    [SerializeField]
    Button button = null;                           // The button prefab
    float currentDisplayArmorHP;                    // The HP of the current armor card on the stack
    Image currentDisplayArmorHPCard;                // The HP card of the current armor on the stack
    List<Sprite> healthCards;                       // The list of HP cards for the armor hp
    bool merchantHUDOpen;                           // The bool to check if the merchantHUD is up
    bool isNearMerchant;                            // The bool to check if the player is near the merchant
    List<Image> weaponsOnMerchantHUD;               // A list of the weapons currently on screen from the player inventory
    List<Image> merchantHUDOutlines;                // A list of outlines to drag and drop the weapons into
    Button tradeButton;                             // The trade button for the MerchantHUD
    Button exitButton;                              // The exit button for the MerchantHUD
    Button upButton;                                // The up button for the MerchantHUD
    Button downButton;                              // The down button for the MerchantHUD
    int lowerValueLimitMerchantHUD;                 // This is to keep track of the lower value of the five weapons on screen (this is 0 if weapons 0-4 are displayed)
    int higherValueLimitMerchantHUD;                // This is to keep track of the upper value of the five weapons on screen (this is 4 if weapons 0-4 are displayed)
    Color enabledButtonColor;                       // The color of the button when it is enabled
    Color disabledButtonColor;                      // The color of the button when it is disabled
    float stackDistance;                            // Distance between icons

    // A int to keep track of the amount of items in a slot
    public int MerchantHUDOutlinesFilled { get; set; } 

    // A List of weaponForTrade that are
    public List<WeaponUpForTrade> WeaponsUpForTrade { get; set; }

    /// <summary>
    /// This initializes the Stack and the HUDWeaponDisplay variables
    /// </summary>
    void Start()
    {
        // Set up constants
        ConstantValues container = (ItemValues)Constants.Values(ContainerType.ITEM);
        MIN_STACK_DISTANCE = ((ItemValues)Constants.Values(ContainerType.ITEM)).MinStackDistance;
        MAX_STACK_DISTANCE = ((ItemValues)Constants.Values(ContainerType.ITEM)).MaxStackDistance;
        STACK_DISTANCE_AMOUNT = ((ItemValues)Constants.Values(ContainerType.ITEM)).StackDistanceAmount;

        // Set up other variables
        throwStack = GetComponent<PlayerInventory>().ThrowStack;
        throwList = new List<Weapon>();
        armorStack = GetComponent<PlayerInventory>().ArmorStack;
        armorList = new List<Armor>();
        playerThrow = GetComponent<PlayerThrow>();
        isPlayerHoldingDecoration = false;
        weaponsOnMerchantHUD = new List<Image>();
        merchantHUDOutlines = new List<Image>();
        WeaponsUpForTrade = new List<WeaponUpForTrade>();
        currentDisplayArmorHP = 0;
        merchantHUDOpen = false;
        isNearMerchant = false;
        lowerValueLimitMerchantHUD = 0;
        higherValueLimitMerchantHUD = 5;

        //Set up the healthCards list with all the cards in. 
        //NOTE: They are in order from 1 HP to full HP (4)
        healthCards = new List<Sprite>();
        healthCards.Add(Resources.Load<Sprite>("Sprites/UI/spr_heart1"));         
        healthCards.Add(Resources.Load<Sprite>("Sprites/UI/spr_heart2"));         
        healthCards.Add(Resources.Load<Sprite>("Sprites/UI/spr_heart3"));         
        healthCards.Add(Resources.Load<Sprite>("Sprites/UI/spr_heart4"));
    }

    /// <summary>
    /// This method checks if the stack is out of date, and if it is updates it and then
    /// updates the cards on the hud accordingly.
    /// </summary>
    void LateUpdate()
    {
        //If the merchant HUD is not open, update the stacks on the HUD
        if (!merchantHUDOpen)
        {
            MakeArmorAndWeaponHUD();
        }

        //Check for user input if the merchant is within range
        if (isNearMerchant)
        {
            if(Input.GetKeyDown(KeyCode.E) && !merchantHUDOpen)
            {
                merchantHUDOpen = true;
                SetUpMerchantHUD();
            }

            //If there are five items up to trade, enable the option to trade
            if (MerchantHUDOutlinesFilled > 4)
            {
                //Enable the button
                tradeButton.enabled = true;
                Color enabledColor = tradeButton.image.color;
                enabledColor.a = 1.0f;
                tradeButton.image.color = enabledColor;
            }
            //Else, disable the option to trade if the merchant hud is up
            else if (merchantHUDOpen)
            {
                tradeButton.enabled = false;
                Color disabledColor = tradeButton.image.color;
                disabledColor.a = .5f;
                tradeButton.image.color = disabledColor;
            }
        }
    }

    /// <summary>
    /// This method makes the HUD for the Armor and the Weapon stacks.
    /// </summary>
    /// <param name="justClosedMerchantHUD"></param>
    private void MakeArmorAndWeaponHUD(bool justClosedMerchantHUD = false)
    {
        //If the player is holding a room item, check the bool.
        //Otherwise, uncheck the bool
        isPlayerHoldingDecoration = (playerThrow.CurrentDecoration != null);


        if (justClosedMerchantHUD == true
       || (isPlayerHoldingDecoration == false) && (throwList.Count != throwStack.Count)
       || (isPlayerHoldingDecoration == true) && (throwList.Count - 1 != throwStack.Count)
       || (throwList.Count > 0 && (throwStack.Count > 0) && (throwList[0] != throwStack[0])))
        {
            // Update the weapon HUD
            throwList = GetComponent<PlayerInventory>().ThrowStack.ToList();

            // Now that the stack is updated, update the hud
            // If there are items in the throw stack
            if (throwList.Count > 0)
            {
                // Image to set the next background card
                Image nextWeaponBackground;

                // Sprite to set the next image
                Sprite nextItemImage;

                //Destory all the current weapon icons on the screen before creating all new ones
                for (int k = theHUD.transform.GetChild(0).childCount; k > 0; --k)
                {
                    Destroy(theHUD.transform.GetChild(0).GetChild(k - 1).gameObject);
                }

                //For loop for each weapon in the stack put them on the hud
                for (int i = throwList.Count - 1; i >= 0; --i)
                {
                    // Calculate how far apart icons in the stack should be, the more items the player has, the closer the icons will be
                    stackDistance = distanceCalculation(i);

                    //Make a new background with its parent being the HUD
                    nextWeaponBackground = Instantiate(weaponBackground, theHUD.transform.GetChild(0).transform);
                    
                    //Set the new backgrounds parent and position
                    nextWeaponBackground.transform.localPosition = new Vector3(0, (i * stackDistance) - 400, 0);
                    
                    // Checks to see if the item is on top of the stack
                    if (i == 0 && !isPlayerHoldingDecoration)
                    {
                        // Is on top, set to normal sprite
                        nextItemImage = throwList[i].SprHUD;
                    }
                    else
                    {
                        // Is not on top, set to shadowed-out sprite
                        nextItemImage = throwList[i].ShadowSprHUD;
                    }

                    //Set the child of the weapon backgrounds image to the next weapons sprite
                    nextWeaponBackground.sprite = nextItemImage;
                    if (!(throwList[i].Type == DamageType.NORMAL) || throwList[i].IsTempered || throwList[i].IsLegendary)
                    {
                        nextWeaponBackground.color = Color.Lerp(throwList[i].GlowColor, Color.white, 0.6f);
                        nextWeaponBackground.material = throwList[i].highlightMaterial;
                    }
                }
            }

            // If the stack only has one weapon on it, and the HUD has a card displaying, destroy the card
            else if (throwList.Count == 0 && theHUD.transform.GetChild(0).childCount > 0)
            {
                Destroy(theHUD.transform.GetChild(0).GetChild(0).gameObject);
            }
        }


        //If the current armor stack is out of date update it
        if (justClosedMerchantHUD == true 
         || armorStack.Count() != armorList.Count() 
         || (armorStack.Count > 0) && armorStack.Peek() != armorList[0])
        {
            //Update the armor list with the up to date armor stack
            armorList = GetComponent<PlayerInventory>().ArmorStack.ToList();

            //If the updated stack has any values, update the hud
            if (armorList.Count > 0)
            {
                //Image to set the next background card
                Image nextArmorBackground;
                Image nextArmorHealthBackground;

                //Destory all the current armor icons on the screen before creating all new ones
                for (int k = theHUD.transform.GetChild(1).childCount; k > 0; --k)
                {
                    Destroy(theHUD.transform.GetChild(1).GetChild(k - 1).gameObject);
                }

                //Set the current armor display hp and card to null
                currentDisplayArmorHP = -1;
                currentDisplayArmorHPCard = null;

                    //For loop for each armor in the stack put them on the hud
                    for (int i = armorList.Count - 1; i > -1; --i)
                    {
                        //Make a new background with its parent being the HUD
                        nextArmorBackground = Instantiate(armorBackground, theHUD.transform.GetChild(1).transform);

                        //Make the health background
                        nextArmorHealthBackground = Instantiate(armorBackground, theHUD.transform.GetChild(1).transform);

                        // Calculate how far apart icons in the stack should be, the more items the player has, the closer the icons will be
                        stackDistance = distanceCalculation(i);

                        //Set the new backgrounds parent and position
                        nextArmorBackground.transform.localPosition = new Vector3(0, ((i) * stackDistance) - 400, 0);
                        nextArmorHealthBackground.transform.localPosition = new Vector3(0, ((i) * stackDistance) - 400, 0);

                    //Get the nextWeapons sprite renderer
                    Sprite nextItemImage;

                    // Checks to see if the item is on top of the stack
                    if (i == 0)
                    {
                        // Is on top, set to normal sprite
                        nextItemImage = armorList[i].SprHUD;
                    }
                    else
                    {
                        // Is not on top, set to shadowed-out sprite
                        nextItemImage = armorList[i].ShadowSprHUD;
                    }

                    //Set the child of the weapon backgrounds image to the next weapons sprite
                    nextArmorBackground.transform.GetComponent<Image>().sprite = nextItemImage;
                    nextArmorHealthBackground.transform.GetComponent<Image>().sprite = healthCards[(int)armorList[i].Health - 1];

                        //Grab the current armor health card and it's health
                        if (i == 0)
                        {
                            currentDisplayArmorHP = armorStack.Peek().Health;
                            currentDisplayArmorHPCard = nextArmorHealthBackground;
                        }
                    }
                }
                //If the stack updates to 0, remove the last armor card and the health card
                else if (armorList.Count == 0 && theHUD.transform.GetChild(1).childCount > 0)
                {
                    Destroy(theHUD.transform.GetChild(1).GetChild(0).gameObject);
                    Destroy(theHUD.transform.GetChild(1).GetChild(1).gameObject);
                }
            }
        }
    
    /// <summary>
    /// Helper method for calculating the distance between item icons
    /// </summary>
    /// <param name="index">Index of the icon</param>
    /// <returns>The distance between icons</returns>
    private float distanceCalculation(int index)
    {
        float stackDistance;                                            // Distance between icons
        int distanceModifier;                                           // Modifier for calculating stackDistance 

        // Calculate how far apart icons in the stack should be, the greater the index, the shorter the distance should be
        distanceModifier = index - 2;
        if (distanceModifier < 0)
        {
            distanceModifier = 0;
        }
        stackDistance = MAX_STACK_DISTANCE - (distanceModifier * STACK_DISTANCE_AMOUNT);
        if (stackDistance < MIN_STACK_DISTANCE)
        {
            stackDistance = MIN_STACK_DISTANCE;
        }

        return stackDistance;
    }

    /// <summary>
    /// This method updates the HUD to show the accurate health of the current armor
    /// </summary>
    public void UpdateArmorHealth()
    {
        //If the current armor display HP isnt the current armor HP, and all the parts are in place, update the HUD
        if (armorStack.Count() > 0 && armorStack.Peek().Health != currentDisplayArmorHP &&
            armorStack.Peek().Health > 0 && currentDisplayArmorHPCard != null)
        {
            currentDisplayArmorHP = armorStack.Peek().Health;
            currentDisplayArmorHPCard.sprite = healthCards[armorStack.Peek().Health - 1];
        }
    }


    /// <summary>
    /// If the player is with in range of the merchant, set the bool to true
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Merchant")
        {
            //Set isNearMerchant to true so LateUpdate can check for user input
            isNearMerchant = true;
        }
    }

    /// <summary>
    /// If the player leaves the merchant, let the stacks render back on the HUD
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Merchant")
        {
            //Since the merchant is far away, stop checking for input
            isNearMerchant = false;
        }
    }

    /// <summary>
    /// This sets up the merchant HUD. 
    /// </summary>
    void SetUpMerchantHUD()
    {
        PauseManager.Instance.Pause(false);

        //Set up the max and min values for when the hud is initialized
        higherValueLimitMerchantHUD = throwStack.Count() > 5 ? 5 : throwStack.Count();
        lowerValueLimitMerchantHUD = 0;


        //Set up the trade button
        tradeButton = Instantiate(button, theHUD.transform.GetChild(4).transform);
        tradeButton.transform.localPosition = Vector3.zero;
        tradeButton.enabled = false;
        disabledButtonColor = tradeButton.image.color;
        disabledButtonColor.a = .5f;
        tradeButton.image.color = disabledButtonColor;
        tradeButton.gameObject.GetComponent<MenuButton>().ButtonText.text = "Trade";
        tradeButton.onClick.AddListener(TradeWithMerchant);

        //Set up the exit button
        exitButton = Instantiate(button, theHUD.transform);
        exitButton.transform.localPosition = new Vector3(600, 380, 0);
        exitButton.enabled = true;
        exitButton.gameObject.GetComponent<MenuButton>().ButtonText.text = "Exit";
        exitButton.onClick.AddListener(CloseMerchantHUD);

        //Set up the up buttons
        upButton = Instantiate(button, theHUD.transform);
        upButton.transform.localPosition = new Vector3(600, 300, 0);
        upButton.enabled = false;
        upButton.image.color = disabledButtonColor;
        upButton.gameObject.GetComponent<MenuButton>().ButtonText.text = "Go Up";
        upButton.onClick.AddListener(UpButtonClicked);

        //Set up the down buttons
        downButton = Instantiate(button, theHUD.transform);
        downButton.transform.localPosition = new Vector3(600, -300, 0);
        downButton.enabled = throwStack.Count() > 5 ? true : false;
        enabledButtonColor = downButton.image.color;
        downButton.image.color = throwStack.Count() > 5 ? downButton.image.color : disabledButtonColor; 
        downButton.gameObject.GetComponent<MenuButton>().ButtonText.text = "Go Down";
        downButton.onClick.AddListener(DownButtonClicked);

        //Set the amount of filled icons to 0
        MerchantHUDOutlinesFilled = 0;

        //Get rid of the weapon stack on screen
        for (int k = theHUD.transform.GetChild(0).childCount; k > 0; --k)
        {
            Destroy(theHUD.transform.GetChild(0).GetChild(k - 1).gameObject);
        }

        //Get rid of the armor stack on screen
        for (int k = theHUD.transform.GetChild(1).childCount; k > 0; --k)
        {
            Destroy(theHUD.transform.GetChild(1).GetChild(k - 1).gameObject);
        }

        //Put up to the first five weapons on the HUD for the merchant hud. 
        DisplayPlayerWeaponsToTrade();

        Image nextOutline;
        //Put the drop outlines in place and change their layer
        for (int k = 0; k < 5; ++k)
        {
            nextOutline = Instantiate(weaponBackground, theHUD.transform.GetChild(2).transform);
            nextOutline.transform.localPosition = new Vector3((200 * (k + 1)) + 100, 0, 0);
            nextOutline.sprite = Resources.Load<Sprite>("Sprites/UI/spr_baseIconShadow");
            nextOutline.gameObject.AddComponent<MerchantHUDOutline>();
            nextOutline.GetComponent<Canvas>().sortingOrder = -1;
            merchantHUDOutlines.Add(nextOutline);
        } 
    }
 
    /// <summary>
    /// This method handles removing a weaponUpForTrade from the list of weapons up for trade
    /// </summary>
    public void RemoveItem(int indexInThrowStack)
    {
        for (int i = 0; i < WeaponsUpForTrade.Count; ++i)
        {
            if (WeaponsUpForTrade[i].indexInStack == indexInThrowStack)
            {
                WeaponsUpForTrade.RemoveAt(i);
                break;
            }
        }
    }

    /// <summary>
    /// This is the method that is called when the trade button is clicked
    /// </summary>
    public void TradeWithMerchant()
    {
        //Remove each weapon to trade from the throwList starting from the 
        //weapon in the upForTrade list highest index to the lowest
        List<int> weaponsToTradeIndecies = new List<int>();
        foreach(WeaponUpForTrade aWeapon in WeaponsUpForTrade)
        {
            weaponsToTradeIndecies.Add(aWeapon.indexInStack);
        }

        weaponsToTradeIndecies.Sort();
        weaponsToTradeIndecies.Reverse();

        // Make a new weapon based on what the player traded in.
        List<Rarity> weaponsTradingInRarities = new List<Rarity>();
        foreach (int aWeaponIndex in weaponsToTradeIndecies)
        {

            //If null, then the weapon is on the hand anchor
            if(transform.root.Find(throwList[aWeaponIndex].name) == null)
            {
                weaponsTradingInRarities.Add(throwList[aWeaponIndex].Rarity);
                Destroy(transform.root.GetChild(0).Find(throwList[aWeaponIndex].name).gameObject);
            }
            else
            {
                weaponsTradingInRarities.Add(throwList[aWeaponIndex].Rarity);
                Destroy(transform.root.Find(throwList[aWeaponIndex].name).gameObject);
            }
            
            //Update the local throw list
            throwList.RemoveAt(aWeaponIndex);
        }

        //Drop the new weapon to the player
        Rarity theRarity = LootManager.GetRarity(weaponsTradingInRarities);
        LootManager.DropLoot(transform.root.position, theRarity);

        //Make the new stack the stack in the player's inventory
        GetComponent<PlayerInventory>().ThrowStack = new List<Weapon>(throwList);

        //Update the local list and stack
        throwStack = GetComponent<PlayerInventory>().ThrowStack;
        throwList = new List<Weapon>(throwStack);

        //Reset the Merchant HUD post trade
        MerchantHUDOutlinesFilled = 0;

        //Get rid of the weapon Icons that we just traded
        foreach (Image aWeaponIcon in weaponsOnMerchantHUD)
        {
            Destroy(aWeaponIcon);
        }

        //Close the merchantHUD
        CloseMerchantHUD();
    }

    /// <summary>
    /// This closes the merchant hud and returns the weapon stacks
    /// </summary>
    private void CloseMerchantHUD()
    {
        //Remove the values in the weaponsUpForTrade so they are back on the weapons to pick from
        WeaponsUpForTrade.Clear();

        //Reset the values
        MerchantHUDOutlinesFilled = 0;

        //Destroy the weapon icons on the MerchantHUD and clear the list
        foreach(Image weaponImage in weaponsOnMerchantHUD)
        {
            Destroy(weaponImage);
            Destroy(weaponImage.gameObject);
        }

        weaponsOnMerchantHUD.Clear();

        //Destroy the outline icons on the MerchantHUD and clear the list
        foreach (Image outlineImage in merchantHUDOutlines)
        {
            Destroy(outlineImage);
            Destroy(outlineImage.gameObject);
        }

        merchantHUDOutlines.Clear();

        //Destory the buttons
        Destroy(tradeButton.gameObject);
        Destroy(exitButton.gameObject);
        Destroy(upButton.gameObject);
        Destroy(downButton.gameObject);

        //Set the merchantHUDOpen bool to false
        merchantHUDOpen = false;

        //Unpause the game
        PauseManager.Instance.Unpause();

        //Recreate the weapon and armor stacks on screen
        MakeArmorAndWeaponHUD(true);
    }

    /// <summary>
    /// This method displays up to five weapons on the merchant HUD
    /// </summary>
    private void DisplayPlayerWeaponsToTrade()
    {
        //To check if the weapon icon we are destroying/creating is one up for trade.
        bool weaponIsUpForTrade = false;

        //Destroy the weapon icons on the MerchantHUD
        for (int i = weaponsOnMerchantHUD.Count - 1; i > -1; --i)
        {
            //Check if the player added one of the weapons on the hud to the trade slots.
            DraggableHUDIcon weaponHUDIcon = weaponsOnMerchantHUD[i].GetComponent<DraggableHUDIcon>();
            
            foreach (WeaponUpForTrade weaponInTradeSlot in WeaponsUpForTrade)
            {
                if (weaponHUDIcon.IndexInStack == weaponInTradeSlot.indexInStack)
                {
                    weaponIsUpForTrade = true;
                }
                else
                    weaponIsUpForTrade = false;

                if (weaponIsUpForTrade)
                    break;
            }

            //If the icon was not added to the weapons to trade slots, destroy it to make the new ones.
            if (!weaponIsUpForTrade)
            {
                Destroy(weaponsOnMerchantHUD[i]);
                Destroy(weaponsOnMerchantHUD[i].gameObject);
                weaponsOnMerchantHUD.Remove(weaponsOnMerchantHUD[i]);
            }
        }

        // To reset the bool
        weaponIsUpForTrade = false;

        //Make the new weapon icons
        Image nextWeapon;

        //Put up to the five weapons on the HUD for the merchant hud. 
        //Reusing the weapon stack child
        int positionOnScreen = 0;
        for (int i = lowerValueLimitMerchantHUD; i < higherValueLimitMerchantHUD; ++i)
        {
            foreach (WeaponUpForTrade weaponInTradeSlot in WeaponsUpForTrade)
            {
                //If the weapon is already on screen, dont re-render it
                if (i == weaponInTradeSlot.indexInStack)
                    weaponIsUpForTrade = true;
                 else
                    weaponIsUpForTrade = false;
                

                if (weaponIsUpForTrade)
                    break;
            }

            if (!weaponIsUpForTrade)
            {
                nextWeapon = Instantiate(weaponBackground, theHUD.transform.GetChild(0).transform);
                nextWeapon.transform.localPosition = new Vector3(0, (positionOnScreen * 100) - 200, 0);
                nextWeapon.sprite = throwList[i].SprHUD;
                nextWeapon.color = Color.Lerp(throwList[i].GlowColor, Color.white, 0.6f);
                nextWeapon.material = throwList[i].highlightMaterial;
                nextWeapon.gameObject.AddComponent<DraggableHUDIcon>().IndexInStack = i;
                nextWeapon.GetComponent<DraggableHUDIcon>().ItemName = throwList[i].Name;
                nextWeapon.GetComponent<DraggableHUDIcon>().StartPosition = nextWeapon.transform.localPosition;
                nextWeapon.GetComponent<DraggableHUDIcon>().StartParent = nextWeapon.transform.parent;
                nextWeapon.GetComponent<Canvas>().sortingOrder = 1;
                weaponsOnMerchantHUD.Add(nextWeapon);
            }
                        
            ++positionOnScreen;
        }

        //Check the status of the buttons and change them accordingly
        downButton.enabled = higherValueLimitMerchantHUD < throwStack.Count() ? true : false;
        downButton.image.color = downButton.enabled ? enabledButtonColor : disabledButtonColor;

        upButton.enabled = lowerValueLimitMerchantHUD > 0 ? true : false;
        upButton.image.color = upButton.enabled ? enabledButtonColor : disabledButtonColor;
    }

    /// <summary>
    /// Reacts accordingly when the Up button is clicked
    /// </summary>
    private void UpButtonClicked()
    {
        //If we are not at the upper limit, move the limits to render different weapons onto the screen
        if (lowerValueLimitMerchantHUD > 0)
        {
            //Decrement the upper and lower limits and check if we are out of bounds for the upper.
            lowerValueLimitMerchantHUD -= 1;
            lowerValueLimitMerchantHUD = lowerValueLimitMerchantHUD > 0 ? lowerValueLimitMerchantHUD : 0;

            //Move the lower by the same amount as the upper and check if the offset is still the same. 
            higherValueLimitMerchantHUD -= 1;
            higherValueLimitMerchantHUD = lowerValueLimitMerchantHUD - higherValueLimitMerchantHUD == 4 ? higherValueLimitMerchantHUD : lowerValueLimitMerchantHUD + 5;

            //Redisplay the weapons to trade
            DisplayPlayerWeaponsToTrade();
        }
    }

    /// <summary>
    /// Reacts accordingly when the Down button is clicked
    /// </summary>
    private void DownButtonClicked()
    {
        //If we are not at the upper limit, move the limits to render different weapons onto the screen
        if (higherValueLimitMerchantHUD < throwStack.Count)
        {
            //Move the lower by the same amount as the upper and check if the offset is still the same. 
            higherValueLimitMerchantHUD += 1;
            higherValueLimitMerchantHUD = higherValueLimitMerchantHUD < throwStack.Count ? higherValueLimitMerchantHUD : throwStack.Count;

            //Decrement the upper and lower limits and check if we are out of bounds for the upper.
            lowerValueLimitMerchantHUD += 1;
            lowerValueLimitMerchantHUD = lowerValueLimitMerchantHUD - higherValueLimitMerchantHUD == 4 ? lowerValueLimitMerchantHUD : higherValueLimitMerchantHUD - 5;

            //Redisplay the weapons to trade
            DisplayPlayerWeaponsToTrade();
        }
    }
}

