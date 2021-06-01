using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the armor super class that will hold properties that all armor types will have.
/// </summary>
public class Armor : Item
{
    /// <summary>
    /// An enumeration of armor types
    /// </summary>
    protected enum ArmorType
    {
        LEATHER,
        CHAINMAIL,
        PLATE
    }

    [SerializeField]
    protected ArmorType type;                                   // The type of armor

    // Audio vars
    protected List<AudioClipName> damageAudioClips;             // Audio clip for armor taking damage
    protected List<AudioClipName> breakingAudioClips;           // Audio clip for armor breaking
    protected AudioSource playerAudioSource;                    // The player's audio source
    protected HUDUpdater playerHUD;                             // The player's audio source

    /// <summary>
    /// The rarity of the armor
    /// </summary>
    public Rarity Rarity { get; protected set; }

    /// <summary>
    /// How many hits the armor can take
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    /// Start method
    /// </summary>
    protected override void Awake()
    {
        //Calling base start method
        base.Awake();

        // Set starting values
        switch (type)
        {
            case ArmorType.LEATHER:
                Health = ((LightArmorValues)Constants.Values(ContainerType.LIGHT_ARMOR)).Health;
                Rarity = Rarity.Common;
                ShadowSprHUD = Resources.Load<Sprite>("Sprites/UI/Light Armor/spr_lightarmorShadow");
                SprHUD = Resources.Load<Sprite>("Sprites/UI/Light Armor/spr_lightarmor");
                PickupSound = AudioClipName.LIGHT_ARMOR_EQUIP;
                damageAudioClips = new List<AudioClipName>()
                {
                  AudioClipName.LIGHT_ARMOR_HIT,
                  AudioClipName.LIGHT_ARMOR_HIT2,
                  AudioClipName.LIGHT_ARMOR_HIT3,
                  AudioClipName.LIGHT_ARMOR_HIT4
                };
                breakingAudioClips = new List<AudioClipName>()
                {
                    AudioClipName.LIGHT_ARMOR_BREAK,
                    AudioClipName.LIGHT_ARMOR_BREAK2,
                    AudioClipName.LIGHT_ARMOR_BREAK3,
                    AudioClipName.LIGHT_ARMOR_BREAK4,
                };
                break;
            case ArmorType.CHAINMAIL:
                Health = ((ChainMailValues)Constants.Values(ContainerType.CHAINMAIL)).Health;
                Rarity = Rarity.Uncommon;
                ShadowSprHUD = Resources.Load<Sprite>("Sprites/UI/Chainmail/spr_chainmailShadow");
                SprHUD = Resources.Load<Sprite>("Sprites/UI/Chainmail/spr_chainmail");
                PickupSound = AudioClipName.LIGHT_ARMOR_EQUIP;
                damageAudioClips = new List<AudioClipName>() { AudioClipName.LIGHT_ARMOR_HIT };
                breakingAudioClips = new List<AudioClipName>() { AudioClipName.CHAINMAIL_ARMOR_BREAK };
                break;
            case ArmorType.PLATE:
                Health = ((PlateArmorValues)Constants.Values(ContainerType.PLATE_ARMOR)).Health;
                Rarity = Rarity.Rare;
                ShadowSprHUD = Resources.Load<Sprite>("Sprites/UI/Plate Armor/spr_plateArmorShadow");
                SprHUD = Resources.Load<Sprite>("Sprites/UI/Plate Armor/spr_plateArmor");
                PickupSound = AudioClipName.ARMOR_EQUIP;
                damageAudioClips = new List<AudioClipName>() { AudioClipName.PLATE_ARMOR_HIT };
                breakingAudioClips = new List<AudioClipName>() { AudioClipName.PLATE_ARMOR_BREAK };
                break;
        }
    }

    protected override void Start()
    {
        base.Start();

        // Get player audio source
        // TODO: player reference is not being set when armor is loaded into the player's inventory
        // because the objects are disabled from the beginning, so Start is not being called
        playerAudioSource = player.GetComponent<AudioSource>();
        playerHUD = player.GetComponent<HUDUpdater>();
    }

    /// <summary>
    /// Play a sound when broken and destroys the armor. Takes a clip and audioSource parameter.
    /// Since the sound will be different for each armor type the method takes in parameters
    /// </summary>
    public virtual void ArmorBreak()
    {
        playerAudioSource.Stop();
        AudioManager.Play(breakingAudioClips[Random.Range(0, breakingAudioClips.Count)], playerAudioSource);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// This method takes away the hp from the armor upon damage being done to it, and playes the sound of it being hit.
    /// It is virtual so the child class can override it if needed.
    /// </summary>
    /// <param name="damage"></param>
    public virtual void ArmorTakeDamage(int damage)
    {
        Health -= damage;
        AudioManager.Play(damageAudioClips[Random.Range(0, damageAudioClips.Count)], playerAudioSource);
        playerHUD.UpdateArmorHealth();
    }
}
