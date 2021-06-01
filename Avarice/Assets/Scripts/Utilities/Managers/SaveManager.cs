using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Saves & loads game data
/// </summary>
public class SaveManager : MonoBehaviour
{
    public bool DaggerTutorialSeen;                          // Whether the dagger tutorial has been seen
    public bool SpearTutorialSeen;                           // Whether the spear tutorial has been seen
    public bool WarhammerTutorialSeen;                       // Whether the warhammer tutorial has been seen
    public bool ArmorTutorialSeen;                           // Whether the armor tutorial has been seen
    public bool EnvironmentalWeaponTutorialSeen;             // Whether the environmental weapon tutorial has been seen
    public bool FirstMessageSeen;                            // Whether the first message has been seen
    public bool LastMessageSeen;                             // Whether the last message has been seen
    public Stack<Weapon> ThrowStack = new Stack<Weapon>();   // The player's throw stack
    public Stack<Armor> ArmorStack = new Stack<Armor>();     // The player's armor stack
    public static GameObject DaggerReference;                // Prefab of the dagger
    public static GameObject SpearReference;                 // Prefab of the spear
    public static GameObject WarhammerReference;             // Prefab of the warhammer
    public static GameObject LightArmorReference;            // Prefab of the lightarmor
    public static GameObject PlateArmorReference;            // Prefab of the platearmor
    public static GameObject ChainmailArmorReference;        // Prefab of the chainmail
    public static GameObject HoarFrostReference;             // Prefab of the HoarFrost
    public static GameObject MorningGloryReference;          // Prefab of the Morning Glory
    public static GameObject GungnirReference;               // Prefab of the Gungnir 
    public static GameObject thePlayer;                      // Reference of the player
    private static SaveManager instance;                     // Singleton reference

    /// <summary>
    /// Reference for the player object in the current scene
    /// </summary>
    public static GameObject Player {
        private get
        {
            thePlayer = FindObjectOfType<PlayerInventory>().gameObject;
            return thePlayer;
        } 
        set
        {
            thePlayer = value;
        }
    }

    /// <summary>
    /// Singleton of the save manager
    /// </summary>
    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("saveGameData").AddComponent<SaveManager>();
                DaggerReference = Resources.Load<GameObject>("Prefabs/Weapons/Dagger");
                SpearReference = Resources.Load<GameObject>("Prefabs/Weapons/Spear");
                WarhammerReference = Resources.Load<GameObject>("Prefabs/Weapons/Warhammer");
                LightArmorReference = Resources.Load<GameObject>("Prefabs/Armor/LightArmor");
                PlateArmorReference = Resources.Load<GameObject>("Prefabs/Armor/PlateArmor");
                ChainmailArmorReference = Resources.Load<GameObject>("Prefabs/Armor/ChainMail");
                HoarFrostReference = Resources.Load<GameObject>("Prefabs/Weapons/HoarFrost");
                //TODO: Uncomment these once these two weapons are in
                //MorningGloryReference = Resources.Load<GameObject>("Prefabs/Weapons/MorningGlory");
                //GungnirReference = Resources.Load<GameObject>("Prefabs/Weapons/Gungnir");
            }
            return instance;
        }
    }


    /// <summary>
    /// This method takes the player's data and saves it to a json file.
    /// </summary>
    public void SaveData(bool saveStacks = true, bool saveFerocity = true)
    {
        // Set up the file and the json string for the file
        string path = Application.persistentDataPath + "/save.dat"; // Where to save data
        string jsonSave = string.Empty;
       
        // For each weapon in the weapon stack make a weapon wrapper and put it on a json version of the stack
        WeaponWrapper weaponWrapped;
        List<WeaponWrapper> throwStack = new List<WeaponWrapper>();
        if(saveStacks)
        {
            foreach (Weapon aWeapon in Player.GetComponent<PlayerInventory>().ThrowStack)
            {
                weaponWrapped = new WeaponWrapper(aWeapon);
                throwStack.Add(weaponWrapped);
            }
        }

        // For each armor in the armor stack make a armor wrapper and put in on a json version of the stack
        ArmorWrapper armorWrapped;
        List<ArmorWrapper> armorStack = new List<ArmorWrapper>();
        if(saveStacks)
        {
            foreach (Armor aArmor in Player.GetComponent<PlayerInventory>().ArmorStack)
            {
                armorWrapped = new ArmorWrapper(aArmor);
                armorStack.Add(armorWrapped);
            }
        }

        float currentKillsJson = 0.0f;
        float currentStackFerocityJson = 0.0f;
        if (saveFerocity)
        {
            currentKillsJson = FerocityManager.Instance.CurrentKills;
            currentStackFerocityJson = FerocityManager.Instance.CurrentStack;
        }

        // Make a wrapper for the data on the tutorial save data and make a wrapper on it.
        TutorialBooleans tutorialData = new TutorialBooleans(DaggerTutorialSeen, SpearTutorialSeen, WarhammerTutorialSeen, ArmorTutorialSeen,
        EnvironmentalWeaponTutorialSeen, FirstMessageSeen, LastMessageSeen);

        // With all the wrappers, make a player save data object which holds all the data to save
        PlayerSaveData data = new PlayerSaveData(armorStack, throwStack, tutorialData, currentKillsJson, currentStackFerocityJson);

        // Make the save data into a json file
        jsonSave = JsonUtility.ToJson(data);

        // Write the json to the file
        File.WriteAllText(path, jsonSave);
    }

    /// <summary>
    /// This method looks for a save file on the player save data and loads it up
    /// </summary>
    public void LoadData()
    {
        // Set up the file path and json string
        string path = Application.persistentDataPath + "/save.dat";  // Where to load data
        string json = "";

        //If the file does not exist make one
        if (File.Exists(path))
            json = File.ReadAllText(path);
        else
            File.WriteAllText(path, json);

        // Get the loaded data from the file we found/made. Take it from json to PlayerSaveData
        PlayerSaveData loadedData = JsonUtility.FromJson<PlayerSaveData>(json);

        //If there is no data to load, return
        if (json.Equals(""))
            return;

        // Sets current values to the loaded values
        DaggerTutorialSeen = loadedData.TutorialData.DaggerTutorialSeen;
        SpearTutorialSeen = loadedData.TutorialData.SpearTutorialSeen;
        WarhammerTutorialSeen = loadedData.TutorialData.WarhammerTutorialSeen;
        ArmorTutorialSeen = loadedData.TutorialData.ArmorTutorialSeen;
        EnvironmentalWeaponTutorialSeen = loadedData.TutorialData.EnvironmentalWeaponTutorialSeen;
        FirstMessageSeen = loadedData.TutorialData.FirstMessageSeen;
        LastMessageSeen = loadedData.TutorialData.LastMessageSeen;

        // If the ArmorStackData is not null, for loop over the entire stack to put it on the player armor stack
        if (loadedData.ArmorStackData != null)
        { 
            for (int i = loadedData.ArmorStackData.Count - 1; i > -1; --i)
            {
                Player.GetComponent<PlayerPickup>().PickUpItem((JsonToWeapon(loadedData.ArmorStackData[i]).GetComponent<Armor>()), false);
            }
        }

        // If the ThrowStackData is not null, for loop over the entire stack to put it on the player throw stack
        if (loadedData.ThrowStackData != null)
        {
            for (int i = loadedData.ThrowStackData.Count - 1; i > -1; --i)
            {
                Player.GetComponent<PlayerPickup>().PickUpItem((JsonToWeapon(loadedData.ThrowStackData[i]).GetComponent<Weapon>()), false);
            }
        }

        // Set the player Ferocity from the save file
        FerocityManager.Instance.CurrentKills = loadedData.CurrentKills;
        FerocityManager.Instance.CurrentStack = loadedData.CurrentStackFerocity;

        FerocityManager.Instance.SetUIText();
    }

    /// <summary>
    /// This method takes in a json representation of a item from the player inventory and makes it into the actual item
    /// </summary>
    /// <param name="jsonWeapon"></param>
    /// <returns></returns>
    private GameObject JsonToWeapon(Wrapper jsonWeapon)
    {
        GameObject theWeapon = null;
        switch(jsonWeapon.WeaponType)
        {
            case ItemName.SPEAR:
                theWeapon = Instantiate(SpearReference);
                break;
            case ItemName.WARHAMMER:
                theWeapon = Instantiate(WarhammerReference);
                break;
            case ItemName.DAGGER:
                theWeapon = Instantiate(DaggerReference);
                break;
            case ItemName.LIGHT_ARMOR:
                theWeapon = Instantiate(LightArmorReference);
                break;
            case ItemName.CHAINMAIL:
                theWeapon = Instantiate(ChainmailArmorReference);
                break;
            case ItemName.PLATE_ARMOR:
                theWeapon = Instantiate(PlateArmorReference);
                break;
            case ItemName.HOARFROST:
                theWeapon = Instantiate(HoarFrostReference);
                break;
           //TODO: Uncomment these once these two weapons are in
                /* case ItemName.GUNGNIR:
                theWeapon = Instantiate(HoarFrostReference);
                break;
            case ItemName.MORNINGGLORY:
                theWeapon = Instantiate(HoarFrostReference);
                break; */
        }

        // If a weapon or a armor piece, set the type or health of it
        if (theWeapon.GetComponent<Weapon>())
        {
            WeaponWrapper weaponWrapper = (WeaponWrapper)jsonWeapon;
            theWeapon.GetComponent<Weapon>().SetType(weaponWrapper.IsTempered, weaponWrapper.DamageType, weaponWrapper.IsLegendary);
        }
        else if (theWeapon.GetComponent<Armor>())
        {
            ArmorWrapper armorWrapper = (ArmorWrapper)jsonWeapon;
            theWeapon.GetComponent<Armor>().Health = armorWrapper.Health;
        }

        return theWeapon;
    }
}
