using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerButtonEvents : MonoBehaviour
{
    // Start is called before the first frame update
    int slot = 0;
    Button button;

    Canvas stash;
    Canvas traderInventory;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();

        // Checks if stash exists
        if (GameObject.Find("StashUI(Clone)") != null)
        {
            stash = GameObject.Find("StashUI(Clone)").GetComponent<Canvas>();
        }

        // Checks if trader exists
        if(GameObject.Find("TraderUI(Clone)") != null)
        {
            traderInventory = GameObject.Find("TraderUI(Clone)").transform.GetChild(0).gameObject.GetComponent<Canvas>();
        }

        if (transform.parent.name.Length < 6)
        {
            slot = int.Parse(transform.parent.name.Substring(transform.parent.name.Length - 1, 1));
        }
        else slot = int.Parse(transform.parent.name.Substring(transform.parent.name.Length - 2, 2));

        /*
        if (GameObject.Find("Trader(Clone)") != null)
        {
            button.onClick.AddListener(delegate { GameObject.Find("Trader(Clone)").GetComponent<Shop>().MovePlayerItemToCart(slot - 1); });
        }
        */

    }

    private void Update()
    {
        // Checks if stash & trader exist
        if (GameObject.Find("StashUI(Clone)") != null && GameObject.Find("Trader(Clone)") != null)
        {
            // Changes button function if stash is open
            if (stash.enabled)
            {
                button.onClick.AddListener(delegate
                {
                    GameObject.Find("InventoryManager(Clone)").GetComponent<InventoryManager>().Move(this.gameObject);
                });
            }
            else
            {
                // Changes button function if trader window is open & stash is closed
                if (traderInventory.enabled)
                {
                    button.onClick.AddListener(delegate
                    {
                        GameObject.Find("Trader(Clone)").GetComponent<Shop>().MovePlayerItemToCart(slot - 1);
                    });
                }

                else
                {
                    // Changes button function if both stahs & trader windows are closed
                    button.onClick.AddListener(delegate
                    {
                        GameObject.Find("InventoryManager(Clone)").GetComponent<InventoryManager>().Equip(this.gameObject);
                    });
                }
            }
        // Changes button function if both stash & trader do not exist
        } else if (GameObject.Find("StashUI(Clone)") == null && GameObject.Find("Trader(Clone)") == null)
        {
            button.onClick.AddListener(delegate
            {
                GameObject.Find("InventoryManager(Clone)").GetComponent<InventoryManager>().Equip(this.gameObject);
            });
        }
    }
}
