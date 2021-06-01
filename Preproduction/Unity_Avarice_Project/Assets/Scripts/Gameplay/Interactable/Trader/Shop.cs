using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    //Prefabs
    public Transform shopInfoTemplate;
    public GameObject traderUI;
    private GameObject itemDetail;
    private GameObject item;
    private Inventory goods;
    private Inventory player;
    private CurrencyExchange trasaction;
    private List<GameObject> inventory;
    private List<Transform> itemDetailsList;
    private bool BuyOrSell = false;
    private List<GameObject> cart;
    private GameObject traderInventory;
    private int total;
    private int slot;
    private AudioSource audioSource;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        inventory = new List<GameObject>();
        cart = new List<GameObject>();

        traderInventory = traderUI.transform.Find("TraderInventory").gameObject;

        shopInfoTemplate.gameObject.SetActive(false);
        goods = gameObject.GetComponent<Inventory>();
        item =  Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Trader/DefaultItem");
        trasaction = gameObject.AddComponent<CurrencyExchange>();

        audioSource = gameObject.AddComponent<AudioSource>();

        int i = 0;
        foreach (GameObject item in goods.slots)
        {

            goods.slots[i] = traderUI.transform.Find("TraderInventory").Find("Slot" + (i + 1)).gameObject;

            i++;
        }

        FillInventory();
    }
    /// <summary>
    /// poulates inventory
    /// </summary>
    private void FillInventory()
    {
        GameObject itemDetails = Instantiate(item);

      /*
        GameObject itemDetails1 = Instantiate(item, gameObject.transform);
        GameObject itemDetails2 = Instantiate(item, gameObject.transform);
      */

        int i = 0;
        //adds item to the trader inventory
        inventory.Add(Instantiate(itemDetails));
        //Sets item name and price this was done for testing
        inventory[i].GetComponent<DefaultItem>().SetName("Sword");
        inventory[i].GetComponent<DefaultItem>().SetPrice(50);
        inventory[i].name = "Item 1";
        //Sets details
        CreateItemDetails(inventory[i], inventory[i].GetComponent<DefaultItem>().GetName(), inventory[i].GetComponent<DefaultItem>().GetPrice(), new int[3], 0);
      
        
        /*  
        i++;

        inventory.Add(Instantiate(itemDetails1));
        inventory[i].GetComponent<DefaultItem>().SetName("Item 2");
        inventory[i].GetComponent<DefaultItem>().SetPrice(45);
        inventory[i].name = "Item 2";
        CreateItemDetails(inventory[i], inventory[i].name, inventory[i].GetComponent<DefaultItem>().GetPrice(), new int[3], 1);
        i++;

        inventory.Add(itemDetails2);
        inventory[i].GetComponent<DefaultItem>().SetName("Item 3");
        inventory[i].GetComponent<DefaultItem>().SetPrice(5);
        inventory[i].name = "Item 3";
        CreateItemDetails(inventory[i], inventory[i].name, inventory[i].GetComponent<DefaultItem>().GetPrice(), new int[3], 2);
        */
    }

    /// <summary>
    /// poulates item details
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="itemName"></param>
    /// <param name="itemPrice"></param>
    /// <param name="stats"></param>
    private void CreateItemDetails(GameObject gameObject, string itemName, int itemPrice, int[] stats, int slot)
    {
        int traderSlot = slot;
        Transform shopItemTransform = Instantiate(shopInfoTemplate, traderInventory.transform);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        //sets postion
        shopItemRectTransform.anchoredPosition = shopInfoTemplate.GetComponent<RectTransform>().anchoredPosition;
        //sets name, price, and stats
        shopItemTransform.Find("ItemName").GetComponent<Text>().text = "Name: " + "Sword";
        shopItemTransform.Find("ItemPrice").GetComponent<Text>().text = "Price: " + itemPrice;
        shopItemTransform.Find("ItemStat").GetComponent<Text>().text = "Stats: " +  "Dam: 3";
        shopItemTransform.name = gameObject.name + " Details";

        int i = slot + 7;
        // if inventory is not full then set it to full and display item.
        if (goods.isFull[i] == false)
        {
            goods.isFull[i] = true;
            GameObject itemDetails = Instantiate(gameObject, goods.slots[i].transform, false);
            goods.slots[i].GetComponent<ItemSlot>().SetItem(gameObject);
        }
    }
    /// <summary>
    /// displays details
    /// </summary>
    /// <param name="item"></param>
    public void DisplayItemDetails(int slot)
    {

        if (goods.isFull[slot])
        {
            GameObject details = traderInventory.transform.Find("Item " + (slot - 6) + " Details").gameObject;

            details.gameObject.SetActive(true);
        }

    }

    /// <summary>
    /// Removes displays details
    /// </summary>
    /// <param name="item"></param>
    public void RemoveItemDetails(int slot)
    {
        if (goods.isFull[slot])
        {
            GameObject details = GameObject.Find("TraderInventory").transform.Find("Item " + (slot - 6) + " Details").gameObject;

            details.gameObject.SetActive(false);
        }
        return;
    }
    /// <summary>
    /// moves item from trader inventory to the cart inventory slot is passed in
    /// </summary>
    /// <param name="slot"></param>
    public void MoveItemToCart(int slot)
    {
        //Checks what made the shop is in
        if (BuyOrSell == false)
        {
            AudioManager.Play(AudioClipName.vsArmorPickup, audioSource);

            GameObject itemSlot;
            itemSlot = goods.slots[slot].GetComponent<ItemSlot>().GetItem();
            int k = 0;
            //Check if cart is full
            while (k < 7)
            {
                if (k < 7 && !goods.isFull[k] && itemSlot != null)
                {
                    goods.isFull[k] = true;
                    GameObject cartItem = Instantiate(itemSlot, goods.slots[k].transform, false);
                    //updates total and total text
                    cartItem.GetComponent<DefaultItem>().SetName(inventory[slot - 7].GetComponent<DefaultItem>().GetName());
                    cartItem.GetComponent<DefaultItem>().SetStats(inventory[slot - 7].GetComponent<DefaultItem>().GetStats());
                    cartItem.GetComponent<DefaultItem>().SetPrice(inventory[slot - 7].GetComponent<DefaultItem>().GetPrice());
                    total += inventory[slot - 7].GetComponent<DefaultItem>().GetPrice();
                    traderInventory.transform.Find("Total").GetComponent<Text>().text = total.ToString();
                    //Adds item to the cart
                    cart.Add(cartItem);
                    break;
                }
                k++;
            }
        }
    }
    /// <summary>
    /// Moves player item to the cart
    /// </summary>
    /// <param name="slot"></param>
    public void MovePlayerItemToCart(int slot)
    {
        int k = 0;
        if (player.isFull[slot] && BuyOrSell == true)
        {

            while (k < 7)
            {

                if (!goods.isFull[k])
                {

                    GameObject gameObject = Instantiate(player.slots[slot].transform.GetChild(1).gameObject, goods.slots[k].transform, false);
                    goods.isFull[k] = true;
                    //Adds item to cart
                    cart.Add(gameObject);
                    //updates total and total text
                    total += player.slots[slot].gameObject.transform.GetChild(1).gameObject.GetComponent<DefaultItem>().GetPrice();
                    traderInventory.transform.Find("Total").GetComponent<Text>().text = total.ToString();
                    //Destorys item
                    Destroy(player.slots[slot].transform.GetChild(1).gameObject);
                    player.isFull[slot] = false;
                    break;
                }
                k++;
            }
        }

    }
    /// <summary>
    /// Moves items from trader to player if player has enough money
    /// </summary>
    public void Buy()
    {
        int i = 0;
        int j = 0;
        Inventory player = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        //checks if player has enough money
        if (trasaction.Transaction(total, 1) > 0)
        {
            AudioManager.Play(AudioClipName.vsTradeTransaction, audioSource);
            total = 0;
            traderInventory.transform.Find("Total").GetComponent<Text>().text = total.ToString();
            foreach (GameObject gameObject in cart)
            {
                while (player.isFull[i] == true)
                {
                    i++;
                }
                player.isFull[i] = true;
                GameObject soldItem = Instantiate(Resources.Load<GameObject>("Prefabs/Gameplay/Weapons/Sword"), player.transform, false);
               // soldItem.GetComponent<DefaultItem>().SetPrice(cart[j].GetComponent<DefaultItem>().GetPrice() / 2);
                j++;
            }
            int k = 0;
            foreach (GameObject gameObject in cart)
            {
                Destroy(cart[k]);
                goods.isFull[k] = false;
                k++;
            }
            //Clears cart
            cart.Clear();
        }
        else
        {
            GameObject.Find("ShopText").GetComponent<Text>().text = "Not enough money!";
            AudioManager.Play(AudioClipName.vsTradeFail, audioSource);
        }

    }
    /// <summary>
    /// gives player money for items in cart
    /// </summary>
    public void Sell()
    {
        trasaction.Transaction(total, -1);
        AudioManager.Play(AudioClipName.vsTradeTransaction, audioSource);

        int k = 0;
        //emptys cart
        while (k < 7)
        {
            if (goods.isFull[k])
            {

                Destroy(cart[k]);
                goods.isFull[k] = false;
            }
            else k++;
        }
        //clears cart
        cart.Clear();

        total = 0;
        traderInventory.transform.Find("Total").GetComponent<Text>().text = total.ToString();
    }
    /// <summary>
    /// Clears item from the cart
    /// </summary>
    public void ClearItem()
    {

        //if Shop is in Sell mode
        if (BuyOrSell == true)
        {
            ReturnItems();
            return;
        }
        GameObject.Find("ShopText").GetComponent<Text>().text = "Buy";
        int k = 0;
        //emptys cart
        while (k < 7)
        {
            if (goods.isFull[k])
            {
                Destroy(cart[k]);
                goods.isFull[k] = false;
            }
            else k++;
        }
        cart.Clear();
        //Updates total
        total = 0;
        traderInventory.transform.Find("Total").GetComponent<Text>().text = total.ToString();
    }
    /// <summary>
    /// Returns player items put into the cart !doesn't work as intended!
    /// </summary>
    public void ReturnItems()
    {
        Inventory player = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        //Updates total
        total = 0;
        traderInventory.transform.Find("Total").GetComponent<Text>().text = total.ToString();

        int i = 0;
        //returns each item in the cart back to the player 
        //This is where the bug is. 
        foreach (GameObject gameObject in cart)
        {
            if (player.isFull[i] == false)
            {
                player.isFull[i] = true;
                GameObject returnedItem = Instantiate(goods.slots[i].transform.GetChild(0).gameObject, player.slots[i].transform);
                returnedItem.GetComponent<DefaultItem>().SetPrice(cart[i].GetComponent<DefaultItem>().GetPrice() / 2);
                Destroy(cart[i]);
                goods.isFull[i] = false;
            }
            i++;
        }
        cart.Clear();
    }
    /// <summary>
    /// Sets Text for button to either Buy or Sell 
    /// </summary>
    /// <param name="text"> bool used to track shop state</param>
    public void SetText(bool text)
    {
        BuyOrSell = text;
        if (BuyOrSell == false)
        {
            traderInventory.transform.Find("ShopText").GetComponent<Text>().text = "Buy";
            traderInventory.transform.Find("ShopText").GetComponent<Text>().gameObject.SetActive(false);
            traderInventory.transform.Find("ShopText").GetComponent<Text>().gameObject.SetActive(true);
        }
        else traderInventory.transform.Find("ShopText").GetComponent<Text>().text = "Sell";
    }
    /// <summary>
    /// Checks state of the shop then calls method
    /// </summary>
    public void OnCLick()
    {
        if (BuyOrSell == false)
        {
            Buy();
        }
        else
        {
            Sell();
        }
    }

}
