using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Creates an inventory object and a GameObject object
    private Inventory inventory;
    private PlayerMoney playerMoney;
    Vector3 moneyDrop;

    // On start set inventory to player's inventory
    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
            playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>();
        }

        moneyDrop = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
    }

    public void DropMoney(int minGold, int maxGold, GameObject gameObject)
    {
        moneyDrop = new Vector3(gameObject.transform.position.x -1, gameObject.transform.position.y, 0);

        GameObject money = Instantiate(Resources.Load("Prefabs/Gameplay/Interactable/Money/Money"), moneyDrop, Quaternion.identity) as GameObject;
        money.GetComponent<droppedMoney>().SetMin(minGold);
        money.GetComponent<droppedMoney>().SetMax(maxGold);
        AudioManager.Play(AudioClipName.vsCoinDrop, money.gameObject.GetComponent<AudioSource>());

        if (GameObject.FindGameObjectWithTag("Player") != null && GameObject.FindGameObjectWithTag("Player").GetComponent<ItemDrop>() != null)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<ItemDrop>().itemDrop == 0)
            {
                GameObject dagger = Instantiate(Resources.Load("Prefabs/Gameplay/Weapons/Dagger"), new Vector3(gameObject.transform.position.x + 4, gameObject.transform.position.y, 0), Quaternion.identity) as GameObject;
                dagger.GetComponent<Dagger>().attacking = false;

                GameObject.FindGameObjectWithTag("Player").GetComponent<ItemDrop>().itemDrop = 1;
            }
            else if (GameObject.FindGameObjectWithTag("Player").GetComponent<ItemDrop>().itemDrop == 1)
            {
                //GameObject crossbow = Instantiate(Resources.Load("Prefabs/Gameplay/Weapons/Crossbow"), new Vector3(transform.position.x + 2, gameObject.transform.position.y, 0), Quaternion.identity) as GameObject;

                GameObject.FindGameObjectWithTag("Player").GetComponent<ItemDrop>().itemDrop = 2;
            }
            else if (GameObject.FindGameObjectWithTag("Player").GetComponent<ItemDrop>().itemDrop == 2)
            {
                GameObject ragnarock = Instantiate(Resources.Load("Prefabs/Gameplay/Weapons/Ragnarok"), new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y, 0), Quaternion.identity) as GameObject;

                GameObject.FindGameObjectWithTag("Player").GetComponent<ItemDrop>().itemDrop = 3;
            }
        }
    }

    /// <summary>
    /// Checks for trigger between two collisions
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // if collision has tag Player then... 
        if (other.CompareTag("Player"))
        {
            if (gameObject.tag == "Money")
            {
                playerMoney.AddMoney(gameObject.GetComponent<droppedMoney>().GetMoney());
                Destroy(gameObject);
                AudioManager.Play(AudioClipName.vsCoinPickup,other.gameObject.GetComponent<AudioSource>());
            }
            else if (gameObject.tag != "Enemy")
            {
                for (int i = 0; i < inventory.slots.Length; i++)
                {
                    // if inventory is not full then set it to full and display item picked up
                    if (inventory.isFull[i] == false)
                    {
                        inventory.isFull[i] = true;
                        Instantiate(gameObject, inventory.slots[i].transform, false);
                        Destroy(gameObject);
                        break;
                    }
                }
            }
        }
        /*
        else
        {
            // for all the slots in inventory check...
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                // if inventory is not full then set it to full and display item picked up
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    Instantiate(item, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }*/
    }
}
