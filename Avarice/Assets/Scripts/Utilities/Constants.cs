using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// An enumeration of constant container types 
/// </summary> 
public enum ContainerType
{
    // Player 
    PLAYER,

    // Enemies 
    ENEMY,
    ALGOR_MORTIS,
    BANDIT,
    BRUTE,
    CORPSE_EATER,
    CHERUB,
    DEMON_KING,
    IRONMAIDEN_TRAP,
    SHADOWBLADE,
    SKELODEVIL,
    HERMIT,

    // Items 
    ITEM,

    // Weapons
    WEAPON,
    THROWABLE_ITEM,
    WEAPON_VARIANT,
    DAGGER,
    HOARFROST,
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
    ARMOR,
    LIGHT_ARMOR,
    CHAINMAIL,
    PLATE_ARMOR,

    // Map Generation 
    MAP,

    // Loot
    LOOT
}

/// <summary>
/// A class that uses a singlton to make
/// game constants adjustable in the inspector
/// </summary>
public class Constants : MonoBehaviour
{
    private static GameObject instance;                     // Singleton object

    /////////////////////////////////////////
    // INSERT VALUE NAMES HERE AND
    // ADJUST THEM IN THE CONSTANTS PREFAB
    /////////////////////////////////////////

    #region Containers

    // Player Values
    public PlayerValues Player;

    // Enemy Values
    public EnemyValues Enemy;
    public AlgorMortisValues AlgorMortis;
    public CherubValues Cherub;
    public BanditValues Bandit;
    public SkelodevilValues Skelodevil;
    public HermitValues Hermit;
    public CorpseEaterValues CorpseEater;
    public ShadowbladeValues Shadowblade;
    public BruteValues Brute;
    public IronMaidenTrapValues IronMaidenTrap;
    public DemonKingValues DemonKing;

    // Global Item Values
    public ItemValues Item;

    // Weapon Values
    public ThrowableItemValues ThrowableItem;
    public WeaponVariantValues WeaponVariant;
    public WeaponValues Weapon;
    public DaggerValues Dagger;
    public HoarfrostValues Hoarfrost;
    public WarhammerValues Warhammer;
    public SpearValues Spear;
    public BigCrateValues BigCrate;
    public SmallCrateValues SmallCrate;
    public BarrelValues Barrel;
    public CandleHolderValues CandleHolder;
    public TableValues Table;
    public IronMaidenValues IronMaiden;
    public PlateValues Plate;
    public ForkValues Fork;
    public KnifeValues Knife;
    public ChairValues Chair;

    // Armor Values
    public ArmorValues Armor;
    public LightArmorValues LightArmor;
    public ChainMailValues ChainMail;
    public PlateArmorValues PlateArmor;

    // Map Generation Values
    public MapValues Map;

    // Loot Values
    public LootValues Loot;

    #endregion

    /// <summary>
    /// All constant values
    /// </summary>
    private static Constants Instance
    {
        get
        {
            if (instance == null)
            {
                // Create singleton
                instance = Instantiate(Resources.Load<GameObject>("Prefabs/Constants"));
                DontDestroyOnLoad(instance);
            }
            return instance.GetComponent<Constants>();
        }
    }

    /// <summary>
    /// Public access to constant values using types
    /// </summary>
    /// <typeparam name="T">Type of constants container</typeparam>
    /// <returns>The container object of the corresponding type</returns>
    public static ConstantValues Values(ContainerType type)
    {
        switch (type)
        {
            // Player 
            case ContainerType.PLAYER:
                return Instance.Player;

            // Enemies 
            case ContainerType.ENEMY:
                return Instance.Enemy;
            case ContainerType.ALGOR_MORTIS:
                return Instance.AlgorMortis;
            case ContainerType.BANDIT:
                return Instance.Bandit;
            case ContainerType.BRUTE:
                return Instance.Brute;
            case ContainerType.CORPSE_EATER:
                return Instance.CorpseEater;
            case ContainerType.CHERUB:
                return Instance.Cherub;
            case ContainerType.DEMON_KING:
                return Instance.DemonKing;
            case ContainerType.IRONMAIDEN_TRAP:
                return Instance.IronMaidenTrap;
            case ContainerType.SHADOWBLADE:
                return Instance.Shadowblade;
            case ContainerType.SKELODEVIL:
                return Instance.Skelodevil;
            case ContainerType.HERMIT:
                return Instance.Hermit;

            // Items 
            case ContainerType.ITEM:
                return Instance.Item;

            // Weapons 
            case ContainerType.THROWABLE_ITEM:
                return Instance.ThrowableItem;
            case ContainerType.WEAPON_VARIANT:
                return Instance.WeaponVariant;
            case ContainerType.WEAPON:
                return Instance.Weapon;
            case ContainerType.DAGGER:
                return Instance.Dagger;
            case ContainerType.HOARFROST:
                return Instance.Hoarfrost;
            case ContainerType.WARHAMMER:
                return Instance.Warhammer;
            case ContainerType.SPEAR:
                return Instance.Spear;

            // Decorations 
            case ContainerType.BIGCRATE:
                return Instance.BigCrate;
            case ContainerType.SMALLCRATE:
                return Instance.SmallCrate;
            case ContainerType.BARREL:
                return Instance.Barrel;
            case ContainerType.CANDELHOLDER:
                return Instance.CandleHolder;
            case ContainerType.TABLE:
                return Instance.Table;
            case ContainerType.IRONMAIDEN:
                return Instance.IronMaiden;
            case ContainerType.PLATE:
                return Instance.Plate;
            case ContainerType.FORK:
                return Instance.Fork;
            case ContainerType.KNIFE:
                return Instance.Knife;
            case ContainerType.CHAIR:
                return Instance.Chair;

            // Armor 
            case ContainerType.ARMOR:
                return Instance.Armor;
            case ContainerType.LIGHT_ARMOR:
                return Instance.LightArmor;
            case ContainerType.CHAINMAIL:
                return Instance.ChainMail;
            case ContainerType.PLATE_ARMOR:
                return Instance.PlateArmor;

            // Map Generation 
            case ContainerType.MAP:
                return Instance.Map;

            // Loot
            case ContainerType.LOOT:
                return Instance.Loot;
        }

        return null;
    }
}

#region Values

/// <summary>
/// Parent class for all constant value types
/// </summary>
public abstract class ConstantValues { }

#region Player

[System.Serializable]
public class PlayerValues : ConstantValues
{
    [Tooltip("Size of the player's camera")]
    public float CameraSize;

    [Header("Movement Values")]
    [Tooltip("Player's maximum move speed")]
    public float MoveSpeed;
    [Tooltip("The multiplier for move speed while spinning")]
    public float SpinMultiplier;
    [Tooltip("Player acceleration rate")]
    public float AccelerationRate;
    [Tooltip("Player deceleration rate")]
    public float DecelerationRate;
    [Tooltip("Minimum speed at which the player can be moving before being forced to stop")]
    public float MinSpeedThreshold;
    [Tooltip("Maximum speed multiplyer for the player animator during the spin animation")]
    public float MaxSpinSpeed;
    [Tooltip("How much the player can carry before they stop moving")]
    public float MaxCarryWeight;

    [Header("Timers")]
    [Tooltip("Delay after death sounds and animation have been played to wait before going to the Hub")]
    public float DeathDelay;
    [Tooltip("How often the wall hit sound can be played")]
    public float WallHitDelay;

    [Header("Ferocity Values")]
    [Tooltip("How many enemies it takes to offset weight of 1 item")]
    public float OffsetValue;
    [Tooltip("Max amount of ferocity the player can have")]
    public float MaxFerocity;

    [Header("Greed Values")]
    [Tooltip("Max value for the armor's greed damage multiplier")]
    public int MaxArmorMultiplier;
    [Tooltip("Max value for weapon's greed damage multiplier")]
    public int MaxWeaponMultiplier;
    [Tooltip("How much greed each piece of armor should give")]
    public float GreedPerArmor;
    [Tooltip("How much greed each weapon should give")]
    public float GreedPerWeapon;

    [Space(20)]
    [Tooltip("The alpha value of the player's throw range indicator (0-1)")]
    public float RangeIndicatorOpaicty;
}

#endregion Player

#region Enemies

[System.Serializable]
public class EnemyValues : ConstantValues
{
    // Global Enemy Values
    [Tooltip("The opacity of the red flash for when enemies take damage damage")]
    [SerializeField]
    [HideInInspector]
    [Range(0f, 1f)]
    private float redFlashOpacity = 1f;
    public float RedFlashOpacity { get { return redFlashOpacity; } }

    [Tooltip("How long the enemy should be red after being hit")]
    [SerializeField]
    [HideInInspector]
    private float redFlashTime = 1f;
    public float RedFlashTime { get { return redFlashTime; } }

    // Default Values
    [Header("Health")]
    [Tooltip("The maximum health of the enemy")]
    public float MaxHealth;
    [Tooltip("The amount of damage mitigated per flame tick")]
    public float FlameResistance;
    [Tooltip("The total number of slow counters needed to completely freeze the enemy")]
    public int FreezeThreshold;

    [Header("Movement")]
    [Tooltip("Maximum speed of the enemy when passive")]
    public float PassiveMoveSpeed;
    [Tooltip("Maximum speed of the algormortis when aggressive")]
    public float AggressiveMoveSpeed;
    [Tooltip("The distance to the player at which the enemy becomes aggresive")]
    public float AggroRange;
    [Tooltip("The distance to the player at which the enemy will flee")]
    public float FleeRange;

    [Header("Attacks")]
    [Tooltip("The time in seconds the enemy waits between attacks")]
    public float AttackDelay;
    [Tooltip("The range of the enemy's attack")]
    public float AttackRange;
        

    [Header("Large Enemy Values")]
    [Tooltip("Relative scale of the enemy's large variant")]
    public float ScaleMultiplier;
    [Tooltip("Relative health values of the enemy's large variant")]
    public float HealthMultiplier;
}

[System.Serializable]
public class AlgorMortisValues : EnemyValues
{
    [Header("Individual Enemy Values")]
    [Tooltip("When the player's distance to the cherub is greater than or equal to this value, it becomes passive")]
    public float PassiveRange;
    [Tooltip("The time in seconds that the algormortis spends charging toward the player during each of its attacks")]
    public float AttackTime;
    [Tooltip("The time in seconds that the algormortis waits between moving when passive")]
    public float MoveWait;
    [Tooltip("The time in seconds that the algormortis spends moving when passive")]
    public float MoveTime;
    [Tooltip("The delay in seconds the algor mortis waits before starting his ram movement")]
    public float RamDelay;

    [Header("Ice Explosion Values")]
    [Tooltip("The radius of the explosion")]
    public float ExplosionRadius;
    [Tooltip("The time in seconds that the explosion stays in effect before disappearin")]
    public float ExplosionTime;
    [Tooltip("The time in seconds that a player is frozen when hit by the explosion")]
    public float FreezeTime;
}

[System.Serializable]
public class CherubValues : EnemyValues
{
    [Header("Individual Enemy Values")]
    [Tooltip("When the player's distance to the cherub is greater than or equal to this value, it becomes passive")]
    public float PassiveRange;
    [Tooltip("The time in seconds that the cherub spends charging the first part of an attack")]
    public float Charge1Time;
    [Tooltip("The time in seconds that the cherub spends charging the second part of an attack")]
    public float Charge2Time;
    [Tooltip("The time in seconds that the cherub spends charging the final part of an attack")]
    public float Charge3Time;
    [Tooltip("The time in seconds that the cherub waits between moving when passive")]
    public float MoveDelay;
    [Tooltip("The time in seconds that the cherub spends moving when passive")]
    public float MoveTime;

    [Header("Fireball Values")]
    [Tooltip("The flight speed of the fireball")]
    public float FireballSpeed;
    [Tooltip("The time in seconds before the fireball begins moving")]
    public float FireballDelay;
    [Tooltip("The time in seconds before the fireball is destroyed if it hasn't hit anything, must be higher than Move Delay")]
    public float FireballDestroyTime;

    [Header("Cherub Manager Values")]
    [Tooltip("Distance from the player that the cherubs should be when encircling the player")]
    public float PlayerDistance;
    [Tooltip("How fast the cherubs rotate around the player")]
    public float RotationSpeed;
}

[System.Serializable]
public class BanditValues : EnemyValues
{
    [Header("Individual Enemy Values")]
    [Tooltip("Range at which the bandit will move toward items")]
    public float ItemDistance;
}

[System.Serializable]
public class SkelodevilValues : EnemyValues
{
    [Header("Individual Enemy Values")]
    [Tooltip("Speed at which the skelodevil backs away")]
    public float FleeSpeed;
    [Tooltip("Range at which the skelodevil will chase the player while aggressive")]
    public float ChaseRange;
    [Tooltip("Speed at which the skelodevil will chase the player")]
    public float ChaseSpeed;
    [Tooltip("Time (in seconds) that the skelodevil is vulnerable after being downed")]
    public float ImmuneDelay;
}

[System.Serializable]
public class HermitValues : EnemyValues
{
    [Header("Individual Enemy Values")]
    [Tooltip("The time in seconds that the hermit waits between crawling")]
    public float CrawlDelay;
    [Tooltip("The time in seconds that the hermit spends crawling")]
    public float CrawlTime;
    [Tooltip("The time in seconds that the hermit waits before playing crawl sounds")]
    public float CrawlAudioDelay;
    [Tooltip("The time in seconds that the hermit waits before playing idle sounds")]
    public float IdleAudioDelay;
}

[System.Serializable]
public class CorpseEaterValues : EnemyValues
{
    [Header("Individual Enemy Values")]
    [Tooltip("Range at which the corpse eater will chase the player")]
    public float ChaseRange;
    [Tooltip("How long the corpse eater should stand before chasign the player")]
    public float RiseTime;
}

[System.Serializable]
public class ShadowbladeValues : EnemyValues { }

[System.Serializable]
public class BruteValues : EnemyValues
{
    [Header("Individual Enemy Values")]
    [Tooltip("How long the brute should walk before changing directions")]
    public float WalkTime;
    [Tooltip("How much health the brute's shield has")]
    public int ShieldHealth;
}

[System.Serializable]
public class IronMaidenTrapValues : ConstantValues
{
    [Header("Individual Enemy Values")]
    [Tooltip("How long in seconds before the iron maiden closes")]
    public float CloseTime;
    [Tooltip("How long in seconds before the iron maiden opens")]
    public float OpenTime;
    [Tooltip("How powerful the force is when trying to suck the player in")]
    public float CloseForce;
    [Tooltip("How powerful the force is when spitting the player out")]
    public float OpenForce;
    [Tooltip("Attack range of the iron maiden trap")]
    public float AttackRange;
}

[System.Serializable]
public class DemonKingValues : EnemyValues
{
    [Header("Individual Enemy Values")]
    [Tooltip("Minimum movement speed")]
    public float MinSpeed;
    [Tooltip("Maximum movement speed")]
    public float MaxSpeed;
    [Tooltip("Percent chance to change directions after an attack")]
    public int DirectionChangeChance;
    [Tooltip("Chance of using multishot attack, does not need to add up to 100")]
    public int MultishotProbability;
    [Tooltip("Chance of using rapidshot attack, does not need to add up to 100")]
    public int RapidshotProbability;
    [Tooltip("Chance of using geokinesis attack, does not need to add up to 100")]
    public int GeokinesisProbability;

    [Header("Multishot Attack")]
    [Tooltip("Number of fireballs shot from the multishot, must be an odd number")]
    public int MultishotFireballs;
    [Tooltip("Angle in degrees between fireballs that are shot from the multishot")]
    public float MultishotAngle;
    [Tooltip("The animation speed of the multishot attack")]
    public float MultishotAnimationSpeed;
    [Tooltip("Number of times to make the multishot attack")]
    public int MultishotCount;
    [Tooltip("Cooldown before demon king can make another attack after using multishot")]
    public float MultishotCooldown;
    [Tooltip("Overridden duration of the burning effect")]
    public float BurnDuration;
    [Tooltip("Overridden duration of the freezing effect")]
    public float FreezeDuration;

    [Header("Rapidshot Attack")]
    [Tooltip("Number of fireballs to be shot in one stream of the rapidshot")]
    public int RapidshotFireballs;
    [Tooltip("How long in seconds it takes for the rapidshot to be charged")]
    public float RapidshotChargeTime;
    [Tooltip("How many streams of fireballs to shoot during the rapidshot")]
    public int RapidshotCount;
    [Tooltip("Delay in seconds between rapidshot streams")]        
    public float RapidshotDelay;
    [Tooltip("Delay in seconds between individual fireballs in each stream")]
    public float RapidshotFireballDelay;
    [Tooltip("Cooldown before demon king can make another attack after using rapidshot")]
    public float RapidshotCooldown;
    [Tooltip("Cooldown before the demon king can move again after using rapidshot")]
    public float RapidshotVulnerableTime;
    [Tooltip("Angle to begin the rapidshot attack")]
    public float RapidshotStartAngle;
    [Tooltip("Angle to end the rapidshot attack")]
    public float RapidshotEndAngle;
    [Tooltip("The speed of the fireballs fired in the rapidshot attack")]
    public float RapidShotFireballSpeed;
    [Tooltip("Number of fireballs to not create within gaps")]
    public int RapidshotGap;                                      
    [Tooltip("Minimum number of gaps to create")]
    public int RapidshotMinGaps;                                            
    [Tooltip("Maximum number of gaps to create")]
    public int RapidshotMaxGaps;
    [Tooltip("Random range to make rapidshot fireballs be shot at a different angle by")]
    public float RapidshotAimOffset;

    [Header("Geokinesis Attack")]
    [Tooltip("Time that geokinesis rocks exist before splitting apart")]
    public float GeokinesisTime;
    [Tooltip("Delay between launching giant rocks")]
    public float GeokinesisDelay;
    [Tooltip("Delay between creating giant rocks")]
    public float GeokinesisCreationDelay;
    [Tooltip("Cooldown before demon king can use another attack after using geokinesis")]
    public float GeokinesisCooldown;
    [Tooltip("Speed of the giant rocks")]
    public float GeokinesisSpeed;
    [Tooltip("Speed of the smaller rocks")]
    public float GeokinesisSmallSpeed;
    [Tooltip("Number of rocks to break into when a giant rock reaches the max range")]
    public int GeokinesisSplitCount;
}

#endregion Enemies

#region Items

[System.Serializable]
public class ItemValues : ConstantValues
{
    [Tooltip("Minimum distance for the player to pick up an item")]
    [SerializeField]
    [HideInInspector]
    private float minPickupDistance = 0f;
    public float MinPickupDistance { get { return minPickupDistance; } }

    [Tooltip("Rarity of the item")]
    public Rarity Rarity;

    [Tooltip("The minimum distance between icons in the stack")]
    [SerializeField]
    [HideInInspector]
    private float minStackDistance = 0f;
    public float MinStackDistance { get { return minStackDistance; } }
    [Tooltip("The maximum distance between icons in the stack")]
    [SerializeField]
    [HideInInspector]
    private float maxStackDistance = 0f;
    public float MaxStackDistance { get { return maxStackDistance; } }
    [Tooltip("The value to change the distance between stack icons by")]
    [SerializeField]
    [HideInInspector]
    private float stackDistanceAmount = 0f;
    public float StackDistanceAmount { get { return stackDistanceAmount; } }
}

#endregion Items

#region Weapons

[System.Serializable]
public class ThrowableItemValues : ItemValues
{
    [Header("Weapon Values")]
    [Tooltip("Default speed at which item is thrown")]
    public float ThrowSpeed;
    [Tooltip("Default travel time of the item")]
    public float TravelTime;
    [Tooltip("How heavy the item is")]
    public float Weight;
    [Tooltip("Default damage dealt by the weapon")]
    public float Damage;
}

[System.Serializable]
public class WeaponValues : ThrowableItemValues
{
    [Tooltip("The amount of flame damage per tick done by the flame type of the weapon")]
    public float FlamePower;
    [Tooltip("The number of slow counters placed on enemies by the frost type of the weapon")]
    public int FrostPower;
}

[System.Serializable]
public class WeaponVariantValues : ConstantValues
{
    [Header("Glow Colors")]
    public Color FlameGlow;
    public Color FrostGlow;
    public Color TemperedGlow;
    public Color TemperedFlameGlow;
    public Color TemperedFrostGlow;
    public Color LegendaryGlow;

    [Space(20)]
    [Tooltip("The damage multiplier for tempered weapons")]
    public float TemperedMultiplier;
    [Tooltip("The duration of the burning effect")]
    public float BurnDuration;
    [Tooltip("The delay between ticks of burn damage")]
    public float BurnDelay;
    [Tooltip("The duration of the freezing effect")]
    public float FreezeDuration;
}

[System.Serializable]
public class DaggerValues : WeaponValues
{
    [Tooltip("Torque used to spin dagger")]
    public float SpinTorque;
}

[System.Serializable]
public class HoarfrostValues : DaggerValues 
{
    [Tooltip("Multiplier for Travel Time")]
    public float TravelTimeMultiplier;
    [Tooltip("Multiplier for Spin Torque")]
    public float SpinTorqueMultiplier;
}

[System.Serializable]
public class WarhammerValues : WeaponValues 
{
    [Tooltip("How far the warhammer travels")]
    public float Range;
}

[System.Serializable]
public class SpearValues : WeaponValues
{
    [Tooltip("How much the spear's velocity should change every frame")]
    public float VelocityMultiplier;
    [Tooltip("How much the spear's velocity should change upon hitting an enemy")]
    public float VelocityMultiplierHit;
    [Tooltip("How far the spear travels")]
    public float Range;
}

#region Decorations

[System.Serializable]
public class DecorationValues : ThrowableItemValues
{
    [Tooltip("The number of hits the decoration can take before being destroyed")]
    public float MaxHealth;
    [Tooltip("Area of Effect for decorations")]
    public float DecorationAreaOfEffect;
}

[System.Serializable]
public class BigCrateValues : DecorationValues { }

[System.Serializable]
public class SmallCrateValues : DecorationValues { }

[System.Serializable]
public class BarrelValues : DecorationValues { }

[System.Serializable]
public class CandleHolderValues : DecorationValues { }

[System.Serializable]
public class TableValues : DecorationValues { }

[System.Serializable]
public class IronMaidenValues : DecorationValues { }

[System.Serializable]
public class PlateValues : DecorationValues { }

[System.Serializable]
public class ForkValues : DecorationValues { }

[System.Serializable]
public class KnifeValues : DecorationValues { }

[System.Serializable]
public class ChairValues : DecorationValues { }

#endregion Decorations

#endregion Weapons

#region Armor

[System.Serializable]
public class ArmorValues : ItemValues
{
    [Tooltip("The number of hits the armor can take")]
    public int Health;
}

[System.Serializable]
public class LightArmorValues : ArmorValues { }

[System.Serializable]
public class ChainMailValues : ArmorValues { }

[System.Serializable]
public class PlateArmorValues : ArmorValues { }

#endregion Armor

#region Map

[System.Serializable]
public class MapValues : ConstantValues
{
    [Header("Map Generation")]
    [Tooltip("The number of grid cells to offset one module from the next along the X axis")]
    public int ModuleXOffset;
    [Tooltip("The number of grid cells to offset one module from the next along the Y axis")]
    public int ModuleYOffset;

    [Header("Shadows")]
    [Tooltip("The angle at which shadows are cast in the level (right = 0 degreees)")]
    public float Angle;
    [Tooltip("How far to offset shadows from objects")]
    public float Offset;
}

#endregion Map

#region Loot

[System.Serializable]
public class LootValues : ConstantValues
{
    [Tooltip("The starting chance for a drop to increase a tier")]
    public float StartChance;
    [Tooltip("The multiplier for how much the upgrade chance decreases each iteration")]
    public float ChanceMultiplier;
    [Tooltip("The combined rarity threshold from merchant items required for an uncommon item (Must be less than 10)")]
    public int UncommonThreshold;
    [Tooltip("The combined rarity threshold from merchant items required for a rare item (Must be less than 10)")]
    public int RareThreshold;
    [Tooltip("The combined rarity threshold from merchant items required for a legendary item (Must be less than 10)")]
    public int LegendaryThreshold;
}

#endregion Loot

#endregion Values