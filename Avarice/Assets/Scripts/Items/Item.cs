using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An enumeration of lootable items
/// </summary>
public enum ItemName
{
    // Weapons
    DAGGER,
    WARHAMMER,
    SPEAR,

    // Decorations
    BIGCRATE,
    SMALLCRATE,
    BARREL,
    CANDELHOLDER,
    TABLE,
    IRONMAIDEN,
    PLATE,
    FORK,
    KNIFE,
    CHAIR,

    // Armor
    LIGHT_ARMOR,
    CHAINMAIL,
    PLATE_ARMOR,

    //Legendary Weapons
    HOARFROST,
}

/// <summary>
/// This is the super class for all inventory items in the game.
/// </summary>
public class Item : MonoBehaviour
{
    // Constants
    protected float MIN_PICKUP_DISTANCE;        // inimum distance for items to glow and be able to be picked up
    protected ContainerType containerType;      // The type container for constant values

    //Variables for this class
    protected AudioSource audioSource;          // Item's audio source
    protected Transform player;                 // The player object
    protected SpriteRenderer sprRenderer;       // The object's sprite renderer
    protected Material startMaterial;           // The object's starting material
    protected bool canBePickedUp;               // Whether the item can be picked up

    // Public and serialized members
    public ItemName Name;                       // Name of the item
    [TextArea]
    public string Description;                  // Item description for inventory

    /// <summary>
    /// The item's HUD sprite
    /// </summary>
    public Sprite SprHUD { get; protected set; }

    /// <summary>
    /// The item's HUD sprite when it isn't on top of the stack
    /// </summary>
    public Sprite ShadowSprHUD { get; protected set; }

    /// <summary>
    /// The object's highlighted material
    /// </summary>
    public Material highlightMaterial { get; protected set; }

    /// <summary>
    /// Item's pickup sound
    /// </summary>
    public AudioClipName PickupSound { get; protected set; }

    /// <summary>
    /// Called before Start
    /// </summary>
    protected virtual void Awake()
    {
        // Get the audiosource for the item
        audioSource = GetComponent<AudioSource>();

        // Set start material and highlight material
        sprRenderer = GetComponentInChildren<SpriteRenderer>();
        if (sprRenderer != null)
            startMaterial = sprRenderer.material;
        highlightMaterial = new Material(Resources.Load<Material>("Materials/ItemGlow"));
        canBePickedUp = true;

        // Get container type
        containerType = ContainerType.ITEM;
        switch (Name)
        {
            case ItemName.DAGGER:
                containerType = ContainerType.DAGGER;
                break;
            case ItemName.HOARFROST:
                containerType = ContainerType.HOARFROST;
                break;
            case ItemName.WARHAMMER:
                containerType = ContainerType.WARHAMMER;
                break;
            case ItemName.SPEAR:
                containerType = ContainerType.SPEAR;
                break;
            case ItemName.BIGCRATE:
                containerType = ContainerType.BIGCRATE;
                break;
            case ItemName.SMALLCRATE:
                containerType = ContainerType.SMALLCRATE;
                break;
            case ItemName.BARREL:
                containerType = ContainerType.BARREL;
                break;
            case ItemName.CANDELHOLDER:
                containerType = ContainerType.CANDELHOLDER;
                break;
            case ItemName.TABLE:
                containerType = ContainerType.TABLE;
                break;
            case ItemName.IRONMAIDEN:
                containerType = ContainerType.IRONMAIDEN;
                break;
            case ItemName.PLATE:
                containerType = ContainerType.PLATE;
                break;
            case ItemName.FORK:
                containerType = ContainerType.FORK;
                break;
            case ItemName.KNIFE:
                containerType = ContainerType.KNIFE;
                break;
            case ItemName.CHAIR:
                containerType = ContainerType.CHAIR;
                break;
        }

        // Set constants
        ItemValues container = (ItemValues)Constants.Values(containerType);
        MIN_PICKUP_DISTANCE = ((ItemValues)Constants.Values(ContainerType.ITEM)).MinPickupDistance;
    }

    /// <summary>
    /// Start method
    /// </summary>
    protected virtual void Start()
    {
        //Getting the player transform after we know the player exists
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <summary>
    /// Called every frame
    /// </summary>
    protected virtual void Update()
    {
        // For all auto-pickup items
        if (!(this is Decoration))
        {
            // Highlight item when the player is near it
            if (canBePickedUp &&
                transform.root != player &&
                Vector2.Distance(player.position, transform.position) < MIN_PICKUP_DISTANCE * 2
                )
            {
                sprRenderer.material = highlightMaterial;
            }
            else
            {
                sprRenderer.material = startMaterial;
            }
        }
    }
}
