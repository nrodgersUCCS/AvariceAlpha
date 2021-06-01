using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A stash
/// </summary>
public class Stash : MonoBehaviour
{
    bool playerInCollision;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInCollision)
        {
            GameObject.Find("InventoryManager(Clone)").GetComponent<InventoryManager>().OpenStash();
        }
    }

    /// <summary>
    /// Checks if colliding with player
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerInCollision = true;
        }
    }

    /// <summary>
    /// Checks if player left collision
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerInCollision = false;
            GameObject.Find("InventoryManager(Clone)").GetComponent<InventoryManager>().CloseStash();
        }
    }
}
