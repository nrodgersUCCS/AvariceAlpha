using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Versioning;
using UnityEngine;

/// <summary>
/// A class to handle loot dropped by enemies
/// </summary>
static class LootManager
{
    // Constants
    private static float START_CHANCE;                              // The starting chance for a drop to increase a tier
    private static float CHANCE_MULTIPLIER;                         // The multiplier for how much the upgrade chance decreases each iteration
    private static int UNCOMMON_THRESHOLD;                          // The combined rarity threshold from merchant items required for an uncommon item
    private static int RARE_THRESHOLD;                              // The combined rarity threshold from merchant items required for a rare item
    private static int LEGENDARY_THRESHOLD;                         // The combined rarity threshold from merchant items required for a legendary item

    private static Dictionary<ItemName, GameObject> lootDrops =     // Dictionary of loot that can be dropped
        new Dictionary<ItemName, GameObject>();
    private static bool initialized;                                // Whether the loot manager has been innitialized

    /// <summary>
    /// Initializes the loot manager and loads all prefabs
    /// </summary>
    public static void Initialize()
    {
        // Initialize loot manager
        initialized = true;

        // Set constants
        LootValues container = (LootValues)Constants.Values(ContainerType.LOOT);
        START_CHANCE = container.StartChance;
        CHANCE_MULTIPLIER = container.ChanceMultiplier;
        UNCOMMON_THRESHOLD = container.UncommonThreshold;
        RARE_THRESHOLD = container.RareThreshold;
        LEGENDARY_THRESHOLD = container.LegendaryThreshold;

        // Weapons
        lootDrops.Add(ItemName.DAGGER, Resources.Load<GameObject>("Prefabs/Weapons/Dagger"));
        lootDrops.Add(ItemName.WARHAMMER, Resources.Load<GameObject>("Prefabs/Weapons/Warhammer"));
        lootDrops.Add(ItemName.SPEAR, Resources.Load<GameObject>("Prefabs/Weapons/Spear"));

        // Armor
        lootDrops.Add(ItemName.LIGHT_ARMOR, Resources.Load<GameObject>("Prefabs/Armor/LightArmor"));
        lootDrops.Add(ItemName.CHAINMAIL, Resources.Load<GameObject>("Prefabs/Armor/ChainMail"));
        lootDrops.Add(ItemName.PLATE_ARMOR, Resources.Load<GameObject>("Prefabs/Armor/PlateArmor"));
    }

    /// <summary>
    /// Drops loot at a given position
    /// </summary>
    /// <param name="location">Location to drop loot at</param>
    /// <param name="tier">The minimum tier of rarity (for merchant drops)</param>
    /// <param name="lootList">Optional array of additional loot to drop (for bandit ranger)</param>
    public static void DropLoot(Vector3 location, Rarity tier = (Rarity)(-1), List<GameObject> lootList = null)
    {
        // Initialize the loot manager
        if (!initialized)
        {
            Initialize();
        }

        // Determine item tier
        if (tier == (Rarity)(-1))
            tier = GetRarity();

        // Determine which item to spawn
        GameObject item = null;

        // If item is a legendary weapon
        if (tier == Rarity.Legendary)
        {
            // TODO: Pick one of the 3 legendary weapons
            Debug.Log("Drop Legendary Weapon");
            //int rand = Random.Range(0, 3);
            //switch (rand)
            //{
            //    case 0: // Hoarforst
            //        break;

            //    case 1: // Gungnir
            //        break;

            //    case 2: // Morning Glory
            //        break;
            //}

            //// Spawn the weapon
            //Weapon weapon = Object.Instantiate(item).GetComponent<Weapon>();

            //// Set weapon values
            //weapon.SetType(false, DamageType.NORMAL, isLegendary:true);

            return;
        }

        else
        {
            // For other tiers: 25% armor, 25% for each type of weapon
            int rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0: // Armor
                    if (tier == Rarity.Common)
                        item = lootDrops[ItemName.LIGHT_ARMOR];
                    else if (tier == Rarity.Uncommon)
                        item = lootDrops[ItemName.CHAINMAIL];
                    else
                        item = lootDrops[ItemName.PLATE_ARMOR];
                    break;

                case 1: // Dagger
                    item = lootDrops[ItemName.DAGGER];
                    break;

                case 2: // Spear
                    item = lootDrops[ItemName.SPEAR];
                    break;

                default: // Warhammer
                    item = lootDrops[ItemName.WARHAMMER];
                    break;
            }

            // Spawn the item
            Item droppedItem = Object.Instantiate(item, (Vector2)location, Quaternion.identity).GetComponent<Item>();

            // Set weapon attributes
            if (droppedItem is Weapon)
            {
                Weapon weapon = (Weapon)droppedItem;    // The weapon
                bool isTempered = false;                // If the tempered attribute has been added
                DamageType type = DamageType.NORMAL;    // The weapon's damage type attribute

                // If tier is at least uncommon, add an attribute
                if ((int)tier > 0)
                {
                    // 50% tempered, 50% elemental
                    if (Random.value < 0.5f)
                        isTempered = true;
                    else
                    {
                        // 50% flame, 50% frost
                        if (Random.value < 0.5f)
                            type = DamageType.FLAME;
                        else
                            type = DamageType.FROST;
                    }
                }

                // If tier is rare, add the other attribute
                if ((int)tier > 1)
                {
                    if (!isTempered)
                        isTempered = true;
                    else
                    {
                        if (Random.value < 0.5f)
                            type = DamageType.FLAME;
                        else
                            type = DamageType.FROST;
                    }
                }

                // Set weapon values
                weapon.SetType(isTempered, type);
            }
        }

        // Have the loot play a sound
        AudioManager.Play(AudioClipName.LOOT_DROP + Random.Range(0, 2), item.transform.position);

        // If there are additional pieces of loot to spawn
        if (lootList != null)
        {
            // Go through every piece of loot
            foreach (GameObject extraItem in lootList)
            {
                // Enable the item
                extraItem.SetActive(true);
                extraItem.transform.SetParent(null);

                // Move the item to the correct location
                extraItem.transform.position = location;
            }
        }
    }

    /// <summary>
    /// Returns a rarity tier based on probabilities of each rarity being dropped
    /// </summary>
    /// <param name="minTier">The minimum tier of rarity to return</param>
    /// <param name="itemRarities">A list of item rarities for trade-in items (merhchant)</param>
    /// <returns></returns>
    public static Rarity GetRarity(List<Rarity> itemRarities = null)
    {
        // Start at first tier
        int tier = 0;

        // Set a decreasing probability of increasing the tier
        float upgradeChance = START_CHANCE;

        // If dropping an item from the merchant
        if (itemRarities != null)
        {
            // Add rarity values
            int sum = 0;
            foreach (Rarity rarity in itemRarities)
                sum += (int)rarity;

            // Increase tier for each threshold passed
            if (sum >= UNCOMMON_THRESHOLD)
                tier++;
            if (sum >= RARE_THRESHOLD)
                tier++;
            if (sum >= LEGENDARY_THRESHOLD)
                tier++;
        }

        // For tiers up to legendary, increase the tier if the probability is met
        for (int i = tier; i < (int)Rarity.Legendary; i++)
        {
            if (Random.value < upgradeChance)
            {
                tier++;
                upgradeChance *= CHANCE_MULTIPLIER;
            }
        }

        // Convert tier to rarity
        return (Rarity)tier;
    }
}
