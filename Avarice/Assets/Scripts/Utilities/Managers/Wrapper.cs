using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A abstract wrapper class for all the wrapper objects we need for saving/loading
/// </summary>
[System.Serializable]
public abstract class Wrapper 
{
    public ItemName WeaponType;
}

/// <summary>
/// Armor wrapper for saving and loading
/// </summary>
[System.Serializable]
public class ArmorWrapper : Wrapper
{
    public int Health;
    public ArmorWrapper(Armor aArmor)
    {
        WeaponType = aArmor.Name;
        Health = aArmor.Health;
    }
}

/// <summary>
/// Weapon wrapper
/// </summary>
[System.Serializable]
public class WeaponWrapper : Wrapper
{
    public DamageType DamageType;
    public bool IsTempered;
    public bool IsLegendary;

    // Instance of WeaponWrapper
    public WeaponWrapper(Weapon theWeapon)
    {
        WeaponType = theWeapon.Name;
        DamageType = theWeapon.Type;
        IsTempered = theWeapon.IsTempered;
        IsLegendary = theWeapon.IsLegendary;
    }
}

/// <summary>
/// Tutorial data wrapper
/// </summary>
[System.Serializable]
public class TutorialBooleans
{
    public bool DaggerTutorialSeen;                  // Whether the dagger tutorial has been seen
    public bool SpearTutorialSeen;                   // Whether the spear tutorial has been seen
    public bool WarhammerTutorialSeen;               // Whether the warhammer tutorial has been seen
    public bool ArmorTutorialSeen;                   // Whether the armor tutorial has been seen
    public bool EnvironmentalWeaponTutorialSeen;     // Whether the environmental weapon tutorial has been seen
    public bool FirstMessageSeen;                    // Whether the first message has been seen
    public bool LastMessageSeen;                     // Whether the last message has been seen
                                                     // Instance of PlayerSaveData
    public TutorialBooleans(bool daggerTutorialSeen, bool spearTutorialSeen, bool warhammerTutorialSeen, bool armorTutorialSeen,
        bool environmentalWeaponTutorialSeen, bool firstMessageSeen, bool lastMessageSeen)
    {
        DaggerTutorialSeen = daggerTutorialSeen;
        SpearTutorialSeen = spearTutorialSeen;
        WarhammerTutorialSeen = warhammerTutorialSeen;
        ArmorTutorialSeen = armorTutorialSeen;
        EnvironmentalWeaponTutorialSeen = environmentalWeaponTutorialSeen;
        FirstMessageSeen = firstMessageSeen;
        LastMessageSeen = lastMessageSeen;
    }
}

/// <summary>
/// Saved/ loaded data
/// </summary>
[System.Serializable]
public class PlayerSaveData
{
    public float CurrentKills;                          
    public float CurrentStackFerocity;                          
    public List<ArmorWrapper> ArmorStackData;
    public List<WeaponWrapper> ThrowStackData;
    public TutorialBooleans TutorialData;
    // Instance of PlayerSaveData
    public PlayerSaveData(List<ArmorWrapper> armorData, List<WeaponWrapper> weaponData, TutorialBooleans tutorialData, float currentKills, float currentStackFerocity)
    {
        ArmorStackData = armorData;
        ThrowStackData = weaponData;
        TutorialData = tutorialData;
        CurrentKills = currentKills;
        CurrentStackFerocity = currentStackFerocity;
    }
}
