using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to move objects out of stash
/// </summary>
public class StashButton : MonoBehaviour
{
    Inventory stashInventory;
    Inventory playerInventory;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        stashInventory = GameObject.Find("StashUI(Clone)").GetComponent<Inventory>();
        playerInventory = GameObject.Find("Player(Clone)").GetComponent<Inventory>();

        button = GetComponent<Button>();
        button.onClick.AddListener(Move);
    }

    /// <summary>
    /// Moves objects from the stash to the player inventory
    /// </summary>
    void Move()
    {
        // Finds which slot was clicked
        for(int i = 0; i <= 20; i++)
        {
            if(gameObject.transform.parent.gameObject == stashInventory.slots[i])
            {
                if (stashInventory.isFull[i])
                {
                    // Finds an empty slot
                    for (int j = 0; j <= 20; j++)
                    {
                        if(!playerInventory.isFull[j])
                        {
                            // Moves object to empty slot
                            playerInventory.isFull[j] = true;
                            GameObject item = Instantiate(stashInventory.transform.GetChild(0).gameObject.transform.
                                GetChild(i).gameObject.transform.GetChild(1).gameObject, 
                                playerInventory.slots[j].transform, false);
                            item.name = stashInventory.transform.GetChild(0).gameObject.transform.
                                GetChild(i).gameObject.transform.GetChild(1).gameObject.name;
                            Destroy(stashInventory.slots[i].transform.GetChild(1).gameObject);
                            stashInventory.isFull[i] = false;
                            break;
                        }
                    }
                }
            }
        }
    }
}
