using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorButton : MonoBehaviour
{
    Button button;
    Inventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.AddComponent<Button>();
        playerInventory = GameObject.Find("Player(Clone)").GetComponent<Inventory>();
        button.onClick.AddListener(delegate
        {
            Unequip();
        });
    }

    /// <summary>
    /// Unequips armor
    /// </summary>
    public void Unequip()
    {
        if(gameObject.transform.childCount != 0)
        {
            // Checks for empty space in inventory & places armor in first empty space found
            for(int i = 0; i <= 20; i++)
            {
                if(playerInventory.isFull[i] == false)
                {
                    GameObject armor = Instantiate(gameObject.transform.GetChild(0).gameObject, playerInventory.slots[i].transform, false);
                    armor.name = gameObject.transform.GetChild(0).gameObject.name;
                    Destroy(gameObject.transform.GetChild(0).gameObject);
                    playerInventory.isFull[i] = true;
                    break;
                }
            }
        }
    }
}
