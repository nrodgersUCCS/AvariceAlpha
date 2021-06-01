using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class representing decorations
/// </summary>
public class Decoration : ThrowableItem
{
    // Constants
    float AREA_OF_EFFECT;                               // Area of effect of the decoration when thrown
    float DAMAGE;                                       // Damage dealt by the decoration
    
    //Vars
    protected PlayerPickup playerPickup;                // To grab the player's playerPickup script
    protected List<AudioClipName> itemBreak;            // The decoration's breaking sound
    GameObject brokenItemPrefab;                        // Broken item prefab
    List<AudioClipName> liftingSoundNames;              // A list of player lifting sounds
    private LayerMask enemyLayer;                       // The layer enemies are on
    private LayerMask decorationLayer;                  // The layer decorations are on

    /// <summary>
    /// // How much damage the item can take before breaking
    /// </summary>
    public float MaxHealth { get; protected set; }                     

    /// <summary>
    /// Current health of the item
    /// </summary>
    public float CurrentHealth { get; protected set; }

    /// <summary>
    /// Whether the item is small (determines which holding and throwing animation to use)
    /// </summary>
    public bool IsSmall { get; protected set; }

    protected override void Awake()
    {
        //Call parents awake method
        base.Awake();

        itemBreak = new List<AudioClipName>();
        itemBreak.Add(AudioClipName.WOOD_BREAKING);
        itemBreak.Add(AudioClipName.WOOD_BREAKING2);
        wallHitSound = itemBreak[0];
        liftingSoundNames = new List<AudioClipName>
        {
            AudioClipName.PLAYER_LIFTING,
            AudioClipName.PLAYER_LIFTING2
        };
        brokenItemPrefab = Resources.Load<GameObject>("Prefabs/Environment/Decorations/BrokenBarrel");

        int rand = Random.Range(0, 2);
        PickupSound = liftingSoundNames[rand];

        // Set constants
        DecorationValues container = ((DecorationValues)Constants.Values(containerType));
        AREA_OF_EFFECT = container.DecorationAreaOfEffect;
        DAMAGE = container.Damage;

        // TODO: Change throw animation based on decoration size

        // Change the health to the correct value.
        MaxHealth = container.MaxHealth;

        // Set the item's health
        CurrentHealth = MaxHealth;

        // Set the layer masks
        enemyLayer = LayerMask.GetMask("Enemy");
        decorationLayer = LayerMask.GetMask("EnvironmentalObjects");
    }

    /// <summary>
    /// Start method
    /// </summary>
    protected override void Start()
    {
        //Calls the parents start method
        base.Start();

        //Set variables
        playerPickup = player.GetComponent<PlayerPickup>();
    }

    /// <summary>
    /// Damages the item on landing
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator Land(float delay)
    {
        // Wait for travel time
        yield return new WaitForSeconds(delay);

        // Destroy Item
        DestroyItem(transform.position);
    }


    /// <summary>
    /// Damages the decoration after being hit
    /// </summary>
    public virtual void DamageItem(Vector2 location)
    {
        // Damage Item
        CurrentHealth -= 1;

        // If health is 0, destroy item
        if (CurrentHealth <= 0)
        {
            DestroyItem(location);
        }
    }

    /// <summary>
    /// Destroys the decoration after being thrown or its heath is reduced to 0
    /// </summary>
    /// <param name="location"></param>
    public virtual void DestroyItem(Vector2 location)
    {
        // Prevents the player from picking up a destroyed item and causing a Null Reference Exception
        playerPickup.CanPickupDecoration = false;

        // Make the destoyed version of the decoration so the animation of it plays being destroyed, also scale it to be equal to AREA_OF_EFFECT.
        GameObject brokenItem = Instantiate(brokenItemPrefab, location, this.transform.rotation);
        brokenItem.transform.localScale = new Vector3(AREA_OF_EFFECT, AREA_OF_EFFECT, 1);

        // Play breaking sound
        AudioManager.Play(itemBreak[Random.Range(0, 2)], brokenItem.GetComponent<AudioSource>());

        // If decoration was thrown, inflict area of effect damage
        if (InFlight)
        {
            AreaOfEffect();
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Inflicts damage within an area of effect to both decorations and enemies
    /// </summary>
    protected void AreaOfEffect()
    {
        Collider2D[] colliders;                        // Array of colliders

        // Get all enemies within AREA_OF_EFFECT
        colliders = Physics2D.OverlapCircleAll(transform.position, AREA_OF_EFFECT, enemyLayer);

        // Damage all enemies within AREA_OF_EFFECT
        foreach (Collider2D enemy in colliders)
        {
            Enemy target = enemy.gameObject.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(DAMAGE, DamageType.NORMAL, 0, 0);
            }
        }

        // Get all decorations within AREA_OF_EFFECT
        colliders = Physics2D.OverlapCircleAll(transform.position, AREA_OF_EFFECT, decorationLayer);

        // Damage all enemies within AREA_OF_EFFECT
        foreach (Collider2D decoration in colliders)
        {
            Decoration target = decoration.gameObject.GetComponent<Decoration>();
            if (target != null)
            {
                target.DamageItem(decoration.transform.position);
            }   
        }
    }

    /// <summary>
    /// Damages the item on collision when it is thrown, or when it is hit by an attack from both the player and enemies
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // If item is in flight
        if (InFlight)
        {
            // Determines if it collides with an enemy or another decoration
            Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
            Decoration decorationScript = collision.gameObject.GetComponent<Decoration>();

            // If colliding with an enemy
            if (enemyScript != null)
            {
                // Damage enemy
                collision.gameObject.GetComponent<Enemy>().TakeDamage();
            }
            // If colliding with a room item
            else if (decorationScript != null)
            {
                collision.gameObject.GetComponent<Decoration>().DamageItem(collision.GetContact(0).point);
            }

            // Damage item
            DestroyItem(collision.GetContact(0).point);
        }

        // If colliding with an enemy or player attack
        else if (collision.gameObject.tag == "EnemyWeapon" || collision.gameObject.tag == "PlayerWeapon")
        {
            // Damage item
            DamageItem(collision.GetContact(0).point);
        }
    }

    /// <summary>
    /// When the mouse goes over the decoration, let the player have the ability to pick it up
    /// </summary>
    protected virtual void OnMouseOver()
    {
        // If close to decoration & game isn't paused
        if (Vector2.Distance(player.position, GetComponent<Collider2D>().ClosestPoint(player.position)) <= MIN_PICKUP_DISTANCE && 
            !PauseManager.Instance.IsPaused)
        {
            playerPickup.CanPickupDecoration = true;
            playerPickup.ItemToPickup = this;
            sprRenderer.material = highlightMaterial;
        }
        else
        {
            playerPickup.CanPickupDecoration = false;
            playerPickup.ItemToPickup = null;
            sprRenderer.material = startMaterial;
        }
    }

    /// <summary>
    /// When the mouse leaves the Decoration, remove the player's ability to pick it up
    /// </summary>
    protected virtual void OnMouseExit()
    {
        playerPickup.CanPickupDecoration = false;
        playerPickup.ItemToPickup = null;
        sprRenderer.material = startMaterial;
    }
}
