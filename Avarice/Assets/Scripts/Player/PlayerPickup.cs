using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// This class handles how the player picks up objects in the world
/// </summary>
public class PlayerPickup : MonoBehaviour
{
    AudioSource playerAudioSource;          // The player's audio source
    PlayerThrow playerThrow;                // This is the player's throwing script

    /// <summary>
    /// Whether the player can pick up a decoration
    /// </summary>
    public bool CanPickupDecoration { private get; set; }

    /// <summary>
    /// The decoartion the mouse is hovering over
    /// </summary>
    public Decoration ItemToPickup { private get; set; }

    private PlayerInventory PlayerInventory
    {
        get { return GetComponent<PlayerInventory>(); }
    }

    /// <summary>
    /// This method runs before the game starts.
    /// It gets the player's inventory
    /// </summary>
    void Awake()
    {
        playerAudioSource = GetComponent<AudioSource>();
        playerThrow = GetComponent<PlayerThrow>();
        CanPickupDecoration = false;
        ItemToPickup = null;
    }

    /// <summary>
    /// Start is called before update
    /// </summary>
    private void Start()
    {
    }

    void Update()
    {
        // If player presses LMB when a decoration is highlighted
        if (Input.GetMouseButtonDown(0))
        {
            // If the item is the ironmaiden enemy, activate its attack behavior instead of picking it up
            if (ItemToPickup is IronMaiden)
            {
                ItemToPickup.gameObject.GetComponent<IronMaiden>().Attack();
                return;
            }
            
            // Player cannot be holding a decoration already
            if (CanPickupDecoration && playerThrow.CurrentDecoration == null && !playerThrow.IsThrowing)
            {
                // Pick up item
                playerThrow.CurrentDecoration = ItemToPickup;
                ItemToPickup.GetComponent<Rigidbody2D>().isKinematic = true;
                ItemToPickup.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

                // Set holding item to true if it isn't a small object
                if (!ItemToPickup.IsSmall)
                    PlayerInventory.transform.GetComponent<Animator>().SetBool("HoldingItem", true);

                // Pick up item
                PickUpItem(ItemToPickup);

                // Add to player inventory weight
                PlayerInventory.CurrentWeight += ItemToPickup.Weight;

                // Displays environmental weapon tutorial page if it hasn't yet been seen
                if(!SaveManager.Instance.EnvironmentalWeaponTutorialSeen)
                {
                    PauseManager.Instance.CreateTutorialWindow(ItemToPickup.GetComponent<ThrowableItem>());
                    SaveManager.Instance.EnvironmentalWeaponTutorialSeen = true;
                }
            }
        }
    }

    /// <summary>
    /// Adds an item to the player's inventory when the player collides with it 
    /// </summary>
    /// <param name="col">Other collider</param>
    void OnTriggerEnter2D(Collider2D col) 
    {
        // If colliding with an item
        Item itemScript = col.GetComponent<Item>();

        if (itemScript != null && !playerThrow.IsThrowing)
        {
            PickUpItem(itemScript);
        }
    }


    /// <summary>
    /// If the player picks up an item
    /// </summary>
    /// <param name="item"></param>
    public void PickUpItem(Item item, bool playSound = true)
    {
        // Add the item to the inventory
        if (!(item is Decoration))
        {
            PlayerInventory.AddItem(item);
        }
        item.transform.parent = transform;

        if (!(item is Decoration))
        {
            // Disable object
            item.GetComponent<Collider2D>().enabled = false;
            Collider2D[] itemColliders = item.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D collider in itemColliders)
            {
                collider.enabled = false;
            }
        }
        else
        {
            // Change layers of the item
            item.gameObject.layer = LayerMask.NameToLayer("PlayerShield");
            item.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
            item.gameObject.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Weapons";

            // Change item to kinematic rigidbody
            item.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }

        item.gameObject.SetActive(false);

        // Play pickup sound
        if(playSound)
            AudioManager.Play(item.PickupSound, playerAudioSource);

        // Update throw range indicator
        GetComponentInChildren<ThrowRangeIndicator>().UpdateIndicator();
    }
}
