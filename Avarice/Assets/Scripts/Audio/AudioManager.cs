using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Runtime.Versioning;
using UnityEngine;



////////////////////////////////////////////

// INSTRUCTIONS FOR ADDING AUDIO CLIPS

////////////////////////////////////////////

/*

 * 1. Add a new value to the AudioClipName enumeration that represents the sound

 * 2. Add an entry to the audioClips dictionary in the AudioManager.Initialize method

 *      - Use your new enumeration value as the key 

 *      - Use Resources.Load<AudioClip> with the file path to your sound as the value

*/

/// <summary>
/// An enumeration of audio clip names
/// </summary>
public enum AudioClipName
{
    AUDIOMANAGER_TEST_CLIP,

    #region Player & Weapons

    // Player sounds
    PLAYER_FOOTSTEP_1,
    PLAYER_FOOTSTEP_2,
    PLAYER_FOOTSTEP_3,
    PLAYER_WALL_BUMP,
    PLAYER_WET_WALL_BUMP,
    PICK_UP_SOUND,
    PLAYER_LIFTING,
    PLAYER_LIFTING2,
    NEW_ITEM_PICKUP,
    LOOT_DROP,
    LOOT_DROP_2,
    DEATH_BY_FIRE,

    // Dagger Sounds
    DAGGER_HIT_WALL,
    DAGGER_HIT_ENEMY,
    DAGGER_HIT_ENEMY2,
    DAGGER_PICKUP,
    DAGGER_THROW,
    FROST_DAGGER_THROW,
    FIRE_DAGGER_THROW,
    TEMPERED_DAGGER_THROW,

    // Spear sounds
    SPEAR_HIT_WALL,
    SPEAR_HIT_ENEMY,
    SPEAR_HIT_ENEMY2,
    SPEAR_THROW,
    FIRE_SPEAR_THROW,
    FROST_SPEAR_THROW,
    TEMPERED_SPEAR_THROW,

    // Warhammer
    WARHAMMER_THROW,
    WARHAMMER_SPIN,
    WARHAMMER_WALL_HIT,
    WARHAMMER_ENEMY_HIT,
    FIRE_WARHAMMER_THROW,
    FIRE_WARHAMMER_SPIN,
    FROST_WARHAMMER_THROW,
    FROST_WARHAMMER_SPIN,
    TEMPERED_WARHAMMER_THROW,

    //Armor
    LIGHT_ARMOR_EQUIP,
    ARMOR_EQUIP,
    LIGHT_ARMOR_HIT,
    LIGHT_ARMOR_HIT2,
    LIGHT_ARMOR_HIT3,
    LIGHT_ARMOR_HIT4,
    LIGHT_ARMOR_BREAK,
    LIGHT_ARMOR_BREAK2,
    LIGHT_ARMOR_BREAK3,
    LIGHT_ARMOR_BREAK4,
    CHAINMAIL_ARMOR_BREAK,
    PLATE_ARMOR_BREAK,
    PLATE_ARMOR_HIT,

    //Hoarfrost
    HOARFROST_THROW,
    HOARFROST_RETURN,

    //Gungnir
    GUNGNIR_THROW,

    //Morning Glory
    MORNING_GLORY_THROW,
    MORNING_GLORY_EXPLOSION,
    #endregion

    #region Enemies

    // Universal Death 
    UNIVERSAL_DEATH,

    // Algor Mortis
    ALGOR_MORTIS_AGGRESSIVEGROWL,
    ALGOR_MORTIS_AGGRESSIVEGROWL2,
    ALGOR_MORTIS_AGGRESSIVEGROWL3,
    ALGOR_MORTIS_AGGRESSIVEWIND,
    ALGOR_MORTIS_AGGRESSIVEWIND2,
    ALGOR_MORTIS_AGGRESSIVEWIND3,
    ALGOR_MORTIS_AGGRESSIVEWIND4,
    ALGOR_MORTIS_EXPLOSION,
    ALGOR_MORTIS_EXPLOSION2,
    ALGOR_MORTIS_EXPLOSION3,
    ALGOR_MORTIS_PASSIVEGROWL_OLD,
    ALGOR_MORTIS_PASSIVEGROWL2_OLD,
    ALGOR_MORTIS_PASSIVEGROWL3_OLD,
    ALGOR_MORTIS_PASSIVEWIND_OLD,
    ALGOR_MORTIS_PASSIVEWIND2_OLD,
    ALGOR_MORTIS_PASSIVEWIND3_OLD,
    ALGOR_MORTIS_PASSIVE,
    ALGOR_MORTIS_SHRIEK,
    ALGOR_MORTIS_SHRIEK2,
    ALGOR_MORTIS_SHRIEK3,
    ALGOR_MORTIS_SHRIEK4,
    ALGOR_MORTIS_SHRIEK5,
    ALGOR_MORTIS_SHRIEK6,

    // Bandit Ranger sounds
    BANDIT_RANGER_HURT,
    BANDIT_RANGER_SHOOT,
    BANDIT_RANGER_SHOOT2,
    BANDIT_RANGER_SHOOT3,
    BANDIT_RANGER_VOICE,
    BANDIT_RANGER_VOICE2,
    BANDIT_RANGER_VOICE3,
    BANDIT_RANGER_VOICE4,
    BANDIT_RANGER_VOICE5,
    BANDIT_RANGER_VOICE6,
    BANDIT_RANGER_VOICE7,

    // Brute
    BRUTE_ATTACK,
    BRUTE_ATTACK2,
    BRUTE_HURT,
    BRUTE_HURT2,
    BRUTE_HURT3,
    BRUTE_LIFT_SHIELD,
    BRUTE_SHIELD_HIT,
    BRUTE_SHIELD_BREAK,

    // Cherub sounds
    CHERUB_ATTACK,
    CHERUB_ATTACK2,
    CHERUB_ATTACK3,
    FIREBALL_FIRED,
    FIREBALL_HITTING_WALL,

    // Corpse Eater sounds
    CORPSE_EATER_ATTACK2,
    CORPSE_EATER_ATTACK3,
    CORPSE_EATER_ATTACK4,
    CORPSE_EATER_ATTACK5,
    CORPSE_EATER_HURT,
    CORPSE_EATER_RISE,
    CORPSE_EATER_RISE2,
    CORPSE_EATER_RISE3,
    CORPSE_EATER_RISE4,
    CORPSE_EATER_RISE5,

    // Demon King sounds
    DEMON_KING_ATTACK,
    DEMON_KING_CHARGEUP,
    DEMON_KING_HURT,
    DEMON_KING_HURT2,
    DEMON_KING_HURT3,
    DEMON_KING_VOICE_LINE,
    DEMON_KING_VOICE_LINE2,
    DEMON_KING_VOICE_LINE3,
    DEMON_KING_VOICE_LINE4,
    DEMON_KING_VOICE_LINE5,
    DEMON_KING_VOICE_LINE6,
    ROCK_CREATION,
    ROCK_THROW,
    ROCK_HIT_WALL,

    // Iron Maiden sounds
    IRON_MAIDEN_ATTACK,
    IRON_MAIDEN_CLOSE,
    IRON_MAIDEN_OPEN,

    // Shadowblade sounds
    SHADOWBLADE_ATTACK,
    SHADOWBLADE_ATTACK2,
    SHADOWBLADE_ATTACK3,
    SHADOWBLADE_DEATH,
    SHADOWBLADE_DEATH2,
    SHADOWBLADE_DEATH3,
    SHADOWBLADE_DEATH4,
    SHADOWBLADE_HURT,
    SHADOWBLADE_HURT2,
    SHADOWBLADE_HURT3,
    SHADOWBLADE_TELEPORT,

    // Skelo-Devil sounds
    SKELODEVIL_WALK_1,
    SKELODEVIL_WALK_2,
    SKELODEVIL_WALK_3,
    SKELODEVIL_ATTACK,
    SKELODEVIL_ATTACK2,
    SKELODEVIL_ATTACK3,
    SKELODEVIL_ATTACK5,
    SKELODEVIL_DIE,

    // Undead Hermit sounds
    UNDEAD_HERMIT_GRUNT,
    UNDEAD_HERMIT_GRUNT2,
    UNDEAD_HERMIT_GRUNT3,
    UNDEAD_HERMIT_GRUNT4,
    UNDEAD_HERMIT_GRUNT5,
    UNDEAD_HERMIT_JUMP_LAND,
    UNDEAD_HERMIT_JUMP_LAND2,
    UNDEAD_HERMIT_JUMP_LAND3,
    UNDEAD_HERMIT_JUMP_LAND4,
    UNDEAD_HERMIT_IDLE,
    UNDEAD_HERMIT_IDLE3,
    UNDEAD_HERMIT_IDLE5,
    UNDEAD_HERMIT_SLIDE,
    UNDEAD_HERMIT_SLIDE2,
    UNDEAD_HERMIT_HURT,

    #endregion

    #region Environment

    // Ambient Sounds
    DEMON_EATING,
    DEMONS_SNARLING,
    DEMON_FLYING,
    AMBIENT_WIND,
    AMBIENT_WIND2,
    DOOMED_SOUL,
    DOOMED_SOUL2,
    DOOMED_SOUL3,
    DEMON_ROAR,
    MICE_SOUND,
    SLEEPING_DEMON,
    GATE_OPENING,
    AMBIENT_LAVA4,
    AMBIENT_LAVA3,
    AMBIENT_LAVA2,
    AMBIENT_LAVA1,
    AMBIENT_TORCH,
    CRYPT_OPENING,
    DEMON_JAILER,

    // Environmental Weapons
    METAL_BREAKING,
    WOOD_BREAKING,
    WOOD_BREAKING2,

    // Hub sounds
    HUB_ASSORTED_NOISES,
    HUB_CREAK,
    HUB_CREAK2,
    HUB_CREAK3,
    HUB_MONSTER_SOUND,
    HUB_MONSTER_SOUND2,
    HUB_POUNDING,
    HUB_POUNDING2,
    HUB_SQUEAK,
    HUB_SQUEAK2,
    HUB_WIND,
    HUB_WIND2,
    HUB_WIND3,
    #endregion

    #region Status Effects

    BURNING,
    FREEZING,
    UNFREEZING,

    #endregion

    #region Menu

    PAUSEMENU_POPUP,
    BUTTON_ENTER,
    BUTTON_PRESS,

    #endregion

    #region Merchant

    CHEST_CLOSE,
    CHEST_OPEN,
    CHEST_SHAKE,
    MERCHANT_INTERACT,
    MERCHANT_TRADE,

    #endregion

    #region Music

    // Music
    MUSIC_CLASSICAL_DEMO,
    MUSIC_CLASSICAL_THE_SECOND_DEMO,
    MUSIC_DEATH,
    MUSIC_DEMON_KING,
    MUSIC_WIN_ANTHEM,

    #endregion

    #region Temporary Sounds

    // Temp sounds
    ARROW,
    BOW_SHOOT,
    CLOSE,
    COMPLETED,
    DOOR_OPEN,
    DROP,
    ENEMY_ATTACK1,
    ENEMY_ATTACK2,
    ENEMY_ATTACK3,
    ENEMY_ATTACK4,
    ENEMY_ATTACK5,
    ENEMY_ATTACK6,
    ENEMY_DAMAGE,
    ENEMY_DEATH,
    ENEMY_DEATH2,
    ENEMY_HURT,
    ENEMY_IDLE,
    ENEMY_IDLE2,
    ENEMY_WALK,
    FIRE_ARROW,
    INSTRUCTION_CLICK,
    INVENTORY_CLOSE,
    INVENTORY_OPEN,
    MENU_SCROLL,
    MENU_SELECT,
    OPEN,
    PICKUP,
    PICKUP2,
    PORTAL_OPEN,
    PORTAL_SUMMON,
    RECIEVED,
    SHOOT,
    SHOOT2,
    SWORD_MISS,
    SWORD_PICKUP,
    TELEPORT,
    WALK,
    WEAPON_DROP,
    WEAPON_MISS,
    WEAPON_MISS2,
    WEAPON_PICKUP,
    WEAPON_PICKUP2,
    WEAPON_RELOAD,
    WEAPON_THROW

    #endregion
}

/// <summary>
/// Class to manage all game audio
/// </summary>
public static class AudioManager
{
    static Dictionary<AudioClipName, AudioClip> audioClips =    // Dictionary of audio clips
        new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Auto-initialize audio manager
    /// </summary>
    public static bool Initialized { get; private set; }

    /// <summary>
    /// Initializes the audio manager and loads all audio clips
    /// </summary>
    public static void Initialize()
    {
        // Initialize audio manager
        Initialized = true;

        ///////////////////////////////////////
        // ADD AUDIO CLIPS
        ///////////////////////////////////////

        audioClips.Add(AudioClipName.AUDIOMANAGER_TEST_CLIP, Resources.Load<AudioClip>("Sounds/audioManagerTestClip"));

        #region Player, Armor & Weapons

        // Player sounds
        audioClips.Add(AudioClipName.PLAYER_FOOTSTEP_1, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/player-footsteps-1"));
        audioClips.Add(AudioClipName.PLAYER_FOOTSTEP_2, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/player-footsteps-2"));
        audioClips.Add(AudioClipName.PLAYER_FOOTSTEP_3, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/player-footsteps-3"));
        audioClips.Add(AudioClipName.PLAYER_WALL_BUMP, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/player-wall-bump"));
        audioClips.Add(AudioClipName.PLAYER_WET_WALL_BUMP, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/player-wet-wall-bump"));
        audioClips.Add(AudioClipName.NEW_ITEM_PICKUP, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/player-newitem"));
        audioClips.Add(AudioClipName.PICK_UP_SOUND, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/player-pickup"));
        audioClips.Add(AudioClipName.PLAYER_LIFTING, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/player-lifting"));
        audioClips.Add(AudioClipName.PLAYER_LIFTING2, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/player-lifting2"));
        audioClips.Add(AudioClipName.LOOT_DROP, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/loot-drop"));
        audioClips.Add(AudioClipName.LOOT_DROP_2, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/loot-drop2"));
        audioClips.Add(AudioClipName.DEATH_BY_FIRE, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/death-by-fire"));

        // Dagger Sounds
        audioClips.Add(AudioClipName.DAGGER_HIT_WALL, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Dagger/dagger-hit-wall"));
        audioClips.Add(AudioClipName.DAGGER_HIT_ENEMY, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Dagger/dagger-hit-enemy"));
        audioClips.Add(AudioClipName.DAGGER_HIT_ENEMY2, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Dagger/dagger-hit-enemy2"));
        audioClips.Add(AudioClipName.DAGGER_PICKUP, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Dagger/dagger-pickup"));
        audioClips.Add(AudioClipName.DAGGER_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Dagger/dagger-throw"));
        audioClips.Add(AudioClipName.FIRE_DAGGER_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Dagger/fire-dagger-throw"));
        audioClips.Add(AudioClipName.FROST_DAGGER_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Dagger/frost-dagger-throw"));
        audioClips.Add(AudioClipName.TEMPERED_DAGGER_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Dagger/tempered-dagger-throw"));

        // Spear Sounds
        audioClips.Add(AudioClipName.SPEAR_HIT_ENEMY, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Spear/spear-hit-enemy"));
        audioClips.Add(AudioClipName.SPEAR_HIT_ENEMY2, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Spear/spear-hit-enemy2"));
        audioClips.Add(AudioClipName.SPEAR_HIT_WALL, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Spear/spear-hit-wall"));
        audioClips.Add(AudioClipName.SPEAR_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Spear/spear-throw"));
        audioClips.Add(AudioClipName.FIRE_SPEAR_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Spear/fire-spear-throw"));
        audioClips.Add(AudioClipName.FROST_SPEAR_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Spear/frost-spear-throw"));
        audioClips.Add(AudioClipName.TEMPERED_SPEAR_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Spear/tempered-spear-throw"));

        // Warhammer
        audioClips.Add(AudioClipName.WARHAMMER_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Warhammer/warhammer-throw"));
        audioClips.Add(AudioClipName.WARHAMMER_SPIN, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Warhammer/warhammer-spin"));
        audioClips.Add(AudioClipName.WARHAMMER_WALL_HIT, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Warhammer/warhammer-wall-hit"));
        audioClips.Add(AudioClipName.WARHAMMER_ENEMY_HIT, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Warhammer/warhammer-enemy-hit"));
        audioClips.Add(AudioClipName.FIRE_WARHAMMER_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Warhammer/fire-warhammer-throw"));
        audioClips.Add(AudioClipName.FIRE_WARHAMMER_SPIN, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Warhammer/fire-warhammer-spin"));
        audioClips.Add(AudioClipName.FROST_WARHAMMER_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Warhammer/frost-warhammer-throw"));
        audioClips.Add(AudioClipName.FROST_WARHAMMER_SPIN, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Warhammer/frost-warhammer-spin"));
        audioClips.Add(AudioClipName.TEMPERED_WARHAMMER_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Warhammer/tempered-warhammer-throw"));

        //Armor
        audioClips.Add(AudioClipName.LIGHT_ARMOR_EQUIP, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Armor/light-armor-equip"));
        audioClips.Add(AudioClipName.ARMOR_EQUIP, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Armor/armor-equip"));
        audioClips.Add(AudioClipName.LIGHT_ARMOR_HIT, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Armor/light-armor-hit"));
        audioClips.Add(AudioClipName.LIGHT_ARMOR_HIT2, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Armor/light-armor-hit2"));
        audioClips.Add(AudioClipName.LIGHT_ARMOR_HIT3, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Armor/light-armor-hit3"));
        audioClips.Add(AudioClipName.LIGHT_ARMOR_HIT4, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Armor/light-armor-hit4"));
        audioClips.Add(AudioClipName.LIGHT_ARMOR_BREAK, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Armor/light-armor-break"));
        audioClips.Add(AudioClipName.LIGHT_ARMOR_BREAK2, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Armor/light-armor-break2"));
        audioClips.Add(AudioClipName.LIGHT_ARMOR_BREAK3, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Armor/light-armor-break3"));
        audioClips.Add(AudioClipName.LIGHT_ARMOR_BREAK4, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Armor/light-armor-break4"));
        audioClips.Add(AudioClipName.CHAINMAIL_ARMOR_BREAK, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Armor/chainmail-armor-break"));
        audioClips.Add(AudioClipName.PLATE_ARMOR_BREAK, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Armor/plate-armor-break"));
        audioClips.Add(AudioClipName.PLATE_ARMOR_HIT, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Armor/plate-armor-hit"));

        // Hoarfrost
        audioClips.Add(AudioClipName.HOARFROST_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Hoarfrost/hoarfrost-throw"));
        audioClips.Add(AudioClipName.HOARFROST_RETURN, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/Hoarfrost/hoarfrost-return"));

        //Morning Glory
        audioClips.Add(AudioClipName.MORNING_GLORY_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/MorningGlory/morning-glory-throw"));
        audioClips.Add(AudioClipName.MORNING_GLORY_EXPLOSION, Resources.Load<AudioClip>("Sounds/Sound Effects/Player/Weapons/MorningGlory/morning-glory-explosion"));


        #endregion

        #region Enemies

        // Universal Death sound
        audioClips.Add(AudioClipName.UNIVERSAL_DEATH, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/universal-death"));

        // Algor Mortis sounds
        audioClips.Add(AudioClipName.ALGOR_MORTIS_AGGRESSIVEGROWL, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-aggressive"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_AGGRESSIVEWIND, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-aggressive2"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_AGGRESSIVEGROWL2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-aggressive3"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_AGGRESSIVEGROWL3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-aggressive4"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_AGGRESSIVEWIND2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-aggressive5"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_AGGRESSIVEWIND3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-aggressive6"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_AGGRESSIVEWIND4, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-aggressive7"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_EXPLOSION, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-explosion"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_EXPLOSION2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-explosion2"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_EXPLOSION3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-explosion3"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_PASSIVE, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-passive"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_PASSIVEGROWL_OLD, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-passive-old"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_PASSIVEWIND_OLD, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-passive2-old"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_PASSIVEGROWL2_OLD, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-passive3-old"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_PASSIVEGROWL3_OLD, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-passive4-old"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_PASSIVEWIND2_OLD, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-passive5-old"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_PASSIVEWIND3_OLD, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-passive6-old"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_SHRIEK, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-shriek"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_SHRIEK2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-shriek2"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_SHRIEK3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-shriek3"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_SHRIEK4, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-shriek4"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_SHRIEK5, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-shriek5"));
        audioClips.Add(AudioClipName.ALGOR_MORTIS_SHRIEK6, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Algor Mortis/algor-mortis-shriek6"));

        // Bandit Ranger sounds
        audioClips.Add(AudioClipName.BANDIT_RANGER_HURT, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Bandit Ranger/bandit-ranger-hurt"));
        audioClips.Add(AudioClipName.BANDIT_RANGER_SHOOT, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Bandit Ranger/bandit-ranger-shoot"));
        audioClips.Add(AudioClipName.BANDIT_RANGER_SHOOT2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Bandit Ranger/bandit-ranger-shoot2"));
        audioClips.Add(AudioClipName.BANDIT_RANGER_SHOOT3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Bandit Ranger/bandit-ranger-shoot3"));
        audioClips.Add(AudioClipName.BANDIT_RANGER_VOICE, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Bandit Ranger/bandit-ranger-voice"));
        audioClips.Add(AudioClipName.BANDIT_RANGER_VOICE2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Bandit Ranger/bandit-ranger-voice2"));
        audioClips.Add(AudioClipName.BANDIT_RANGER_VOICE3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Bandit Ranger/bandit-ranger-voice3"));
        audioClips.Add(AudioClipName.BANDIT_RANGER_VOICE4, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Bandit Ranger/bandit-ranger-voice4"));
        audioClips.Add(AudioClipName.BANDIT_RANGER_VOICE5, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Bandit Ranger/bandit-ranger-voice5"));
        audioClips.Add(AudioClipName.BANDIT_RANGER_VOICE6, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Bandit Ranger/bandit-ranger-voice6"));
        audioClips.Add(AudioClipName.BANDIT_RANGER_VOICE7, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Bandit Ranger/bandit-ranger-voice7"));

        // Brute sounds
        audioClips.Add(AudioClipName.BRUTE_ATTACK, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Brute/brute-attack"));
        audioClips.Add(AudioClipName.BRUTE_ATTACK2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Brute/brute-attack2"));
        audioClips.Add(AudioClipName.BRUTE_HURT, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Brute/brute-hurt"));
        audioClips.Add(AudioClipName.BRUTE_HURT2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Brute/brute-hurt2"));
        audioClips.Add(AudioClipName.BRUTE_HURT3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Brute/brute-hurt3"));
        audioClips.Add(AudioClipName.BRUTE_LIFT_SHIELD, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Brute/brute-lift-shield"));
        audioClips.Add(AudioClipName.BRUTE_SHIELD_HIT, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Brute/brute-shield-hit"));
        audioClips.Add(AudioClipName.BRUTE_SHIELD_BREAK, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Brute/brute-shield-break"));

        // Cherub sounds
        audioClips.Add(AudioClipName.CHERUB_ATTACK, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Cherub/cherub-attack"));
        audioClips.Add(AudioClipName.CHERUB_ATTACK2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Cherub/cherub-attack2"));
        audioClips.Add(AudioClipName.CHERUB_ATTACK3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Cherub/cherub-attack3"));
        audioClips.Add(AudioClipName.FIREBALL_FIRED, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Cherub/Fireball Sounds/fireball-fired"));
        audioClips.Add(AudioClipName.FIREBALL_HITTING_WALL, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Cherub/Fireball Sounds/fireball-hitting-wall"));

        // Corpse Eater sounds
        audioClips.Add(AudioClipName.CORPSE_EATER_ATTACK2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Corpse Eater/corpse-eater-attack2"));
        audioClips.Add(AudioClipName.CORPSE_EATER_ATTACK3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Corpse Eater/corpse-eater-attack3"));
        audioClips.Add(AudioClipName.CORPSE_EATER_ATTACK4, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Corpse Eater/corpse-eater-attack4"));
        audioClips.Add(AudioClipName.CORPSE_EATER_ATTACK5, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Corpse Eater/corpse-eater-attack5"));
        audioClips.Add(AudioClipName.CORPSE_EATER_HURT, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Corpse Eater/corpse-eater-hurt"));
        audioClips.Add(AudioClipName.CORPSE_EATER_RISE, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Corpse Eater/corpse-eater-rise"));
        audioClips.Add(AudioClipName.CORPSE_EATER_RISE2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Corpse Eater/corpse-eater-rise2"));
        audioClips.Add(AudioClipName.CORPSE_EATER_RISE3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Corpse Eater/corpse-eater-rise3"));
        audioClips.Add(AudioClipName.CORPSE_EATER_RISE4, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Corpse Eater/corpse-eater-rise4"));
        audioClips.Add(AudioClipName.CORPSE_EATER_RISE5, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Corpse Eater/corpse-eater-rise5"));

        // Demon King sounds
        audioClips.Add(AudioClipName.DEMON_KING_ATTACK, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Demon King/demon-king-attack"));
        audioClips.Add(AudioClipName.DEMON_KING_CHARGEUP, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Demon King/demon-king-chargeup"));
        audioClips.Add(AudioClipName.DEMON_KING_HURT, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Demon King/demon-king-hurt"));
        audioClips.Add(AudioClipName.DEMON_KING_HURT2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Demon King/demon-king-hurt2"));
        audioClips.Add(AudioClipName.DEMON_KING_HURT3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Demon King/demon-king-hurt3"));
        audioClips.Add(AudioClipName.DEMON_KING_VOICE_LINE, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Demon King/demon-king-voice-line"));
        audioClips.Add(AudioClipName.DEMON_KING_VOICE_LINE2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Demon King/demon-king-voice-line2"));
        audioClips.Add(AudioClipName.DEMON_KING_VOICE_LINE3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Demon King/demon-king-voice-line3"));
        audioClips.Add(AudioClipName.DEMON_KING_VOICE_LINE4, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Demon King/demon-king-voice-line4"));
        audioClips.Add(AudioClipName.DEMON_KING_VOICE_LINE5, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Demon King/demon-king-voice-line5"));
        audioClips.Add(AudioClipName.DEMON_KING_VOICE_LINE6, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Demon King/demon-king-voice-line6"));
        audioClips.Add(AudioClipName.ROCK_CREATION, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Demon King/rock-creation"));
        audioClips.Add(AudioClipName.ROCK_HIT_WALL, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Demon King/rock-hit-wall"));
        audioClips.Add(AudioClipName.ROCK_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Demon King/rock-throw"));

        // Iron Maiden sounds
        audioClips.Add(AudioClipName.IRON_MAIDEN_ATTACK, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Iron Maiden/iron-maiden-attack"));
        audioClips.Add(AudioClipName.IRON_MAIDEN_CLOSE, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Iron Maiden/iron-maiden-close"));
        audioClips.Add(AudioClipName.IRON_MAIDEN_OPEN, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Iron Maiden/iron-maiden-open"));

        // Shadowblade sounds
        audioClips.Add(AudioClipName.SHADOWBLADE_ATTACK, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Shadowblade/shadowblade-attack"));
        audioClips.Add(AudioClipName.SHADOWBLADE_ATTACK2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Shadowblade/shadowblade-attack2"));
        audioClips.Add(AudioClipName.SHADOWBLADE_ATTACK3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Shadowblade/shadowblade-attack3"));
        audioClips.Add(AudioClipName.SHADOWBLADE_DEATH, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Shadowblade/shadowblade-death"));
        audioClips.Add(AudioClipName.SHADOWBLADE_DEATH2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Shadowblade/shadowblade-death2"));
        audioClips.Add(AudioClipName.SHADOWBLADE_DEATH3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Shadowblade/shadowblade-death3"));
        audioClips.Add(AudioClipName.SHADOWBLADE_DEATH4, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Shadowblade/shadowblade-death4"));
        audioClips.Add(AudioClipName.SHADOWBLADE_HURT, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Shadowblade/shadowblade-hurt"));
        audioClips.Add(AudioClipName.SHADOWBLADE_HURT2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Shadowblade/shadowblade-hurt2"));
        audioClips.Add(AudioClipName.SHADOWBLADE_HURT3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Shadowblade/shadowblade-hurt3"));
        audioClips.Add(AudioClipName.SHADOWBLADE_TELEPORT, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Shadowblade/shadowblade-teleport"));

        // Skelodevil sounds
        audioClips.Add(AudioClipName.SKELODEVIL_WALK_1, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Skelo-Devil/skelodevil-walk1"));
        audioClips.Add(AudioClipName.SKELODEVIL_WALK_2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Skelo-Devil/skelodevil-walk2"));
        audioClips.Add(AudioClipName.SKELODEVIL_WALK_3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Skelo-Devil/skelodevil-walk3"));
        audioClips.Add(AudioClipName.SKELODEVIL_ATTACK, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Skelo-Devil/skelodevil-attack"));
        audioClips.Add(AudioClipName.SKELODEVIL_ATTACK2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Skelo-Devil/skelodevil-attack2"));
        audioClips.Add(AudioClipName.SKELODEVIL_ATTACK3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Skelo-Devil/skelodevil-attack3"));
        audioClips.Add(AudioClipName.SKELODEVIL_ATTACK5, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Skelo-Devil/skelodevil-attack5"));
        audioClips.Add(AudioClipName.SKELODEVIL_DIE, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Skelo-Devil/skelodevil-death"));

        // Undead Hermit sounds
        audioClips.Add(AudioClipName.UNDEAD_HERMIT_GRUNT, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Undead Hermit/undead-hermit-jump-grunt"));
        audioClips.Add(AudioClipName.UNDEAD_HERMIT_GRUNT2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Undead Hermit/undead-hermit-jump-grunt2"));
        audioClips.Add(AudioClipName.UNDEAD_HERMIT_GRUNT3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Undead Hermit/undead-hermit-jump-grunt3"));
        audioClips.Add(AudioClipName.UNDEAD_HERMIT_GRUNT4, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Undead Hermit/undead-hermit-jump-grunt4"));
        audioClips.Add(AudioClipName.UNDEAD_HERMIT_GRUNT5, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Undead Hermit/undead-hermit-jump-grunt5"));
        audioClips.Add(AudioClipName.UNDEAD_HERMIT_JUMP_LAND, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Undead Hermit/undead-hermit-jump-land"));
        audioClips.Add(AudioClipName.UNDEAD_HERMIT_JUMP_LAND2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Undead Hermit/undead-hermit-jump-land2"));
        audioClips.Add(AudioClipName.UNDEAD_HERMIT_JUMP_LAND3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Undead Hermit/undead-hermit-jump-land3"));
        audioClips.Add(AudioClipName.UNDEAD_HERMIT_JUMP_LAND4, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Undead Hermit/undead-hermit-jump-land4"));
        audioClips.Add(AudioClipName.UNDEAD_HERMIT_IDLE, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Undead Hermit/undead-hermit-idle"));
        audioClips.Add(AudioClipName.UNDEAD_HERMIT_IDLE3, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Undead Hermit/undead-hermit-idle3"));
        audioClips.Add(AudioClipName.UNDEAD_HERMIT_IDLE5, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Undead Hermit/undead-hermit-idle5"));
        audioClips.Add(AudioClipName.UNDEAD_HERMIT_SLIDE, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Undead Hermit/undead-hermit-slide"));
        audioClips.Add(AudioClipName.UNDEAD_HERMIT_SLIDE2, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Undead Hermit/undead-hermit-slide2"));
        audioClips.Add(AudioClipName.UNDEAD_HERMIT_HURT, Resources.Load<AudioClip>("Sounds/Sound Effects/Enemies/Undead Hermit/undead-hermit-hurt"));

        #endregion

        #region Environment

        // Ambient Sounds
        audioClips.Add(AudioClipName.DEMON_EATING, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Ambient Sounds/ambient-demons-eating"));
        audioClips.Add(AudioClipName.DEMONS_SNARLING, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Ambient Sounds/ambient-demons-snarling"));
        audioClips.Add(AudioClipName.DEMON_FLYING, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Ambient Sounds/ambient-wings"));
        audioClips.Add(AudioClipName.AMBIENT_WIND, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Ambient Sounds/ambient-wind"));
        audioClips.Add(AudioClipName.AMBIENT_WIND2, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Ambient Sounds/ambient-wind2"));
        audioClips.Add(AudioClipName.DOOMED_SOUL, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Ambient Sounds/ambient-torture"));
        audioClips.Add(AudioClipName.DOOMED_SOUL2, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Ambient Sounds/ambient-torture2"));
        audioClips.Add(AudioClipName.DOOMED_SOUL3, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Ambient Sounds/ambient-torture3"));
        audioClips.Add(AudioClipName.DEMON_ROAR, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Ambient Sounds/demon-roar"));
        audioClips.Add(AudioClipName.MICE_SOUND, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Ambient Sounds/mice-sound"));
        audioClips.Add(AudioClipName.SLEEPING_DEMON, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Ambient Sounds/sleeping-demon"));
        audioClips.Add(AudioClipName.GATE_OPENING, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Ambient Sounds/gate-opening"));
        audioClips.Add(AudioClipName.DEMON_JAILER, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Ambient Sounds/ambient-demon-jailer"));
        audioClips.Add(AudioClipName.CRYPT_OPENING, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Ambient Sounds/ambient-crypt-opening"));
        audioClips.Add(AudioClipName.AMBIENT_TORCH, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Ambient Sounds/ambient-torch"));

        // Environmental Weapons
        audioClips.Add(AudioClipName.METAL_BREAKING, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Environmental Weapons/metal-breaking"));
        audioClips.Add(AudioClipName.WOOD_BREAKING, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Environmental Weapons/wood-breaking"));
        audioClips.Add(AudioClipName.WOOD_BREAKING2, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Environmental Weapons/wood-breaking2"));

        // Hub sounds
        audioClips.Add(AudioClipName.HUB_ASSORTED_NOISES, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Hub Sounds/hub-assorted-noises"));
        audioClips.Add(AudioClipName.HUB_CREAK, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Hub Sounds/hub-creak"));
        audioClips.Add(AudioClipName.HUB_CREAK2, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Hub Sounds/hub-creak2"));
        audioClips.Add(AudioClipName.HUB_CREAK3, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Hub Sounds/hub-creak3"));
        audioClips.Add(AudioClipName.HUB_MONSTER_SOUND, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Hub Sounds/hub-monster-sound"));
        audioClips.Add(AudioClipName.HUB_MONSTER_SOUND2, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Hub Sounds/hub-monster-sound2"));
        audioClips.Add(AudioClipName.HUB_POUNDING, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Hub Sounds/hub-pounding"));
        audioClips.Add(AudioClipName.HUB_POUNDING2, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Hub Sounds/hub-pounding2"));
        audioClips.Add(AudioClipName.HUB_SQUEAK, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Hub Sounds/hub-squeak"));
        audioClips.Add(AudioClipName.HUB_SQUEAK2, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Hub Sounds/hub-squeak2"));
        audioClips.Add(AudioClipName.HUB_WIND, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Hub Sounds/hub-wind"));
        audioClips.Add(AudioClipName.HUB_WIND2, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Hub Sounds/hub-wind2"));
        audioClips.Add(AudioClipName.HUB_WIND3, Resources.Load<AudioClip>("Sounds/Sound Effects/Environment/Hub Sounds/hub-wind3"));

        #endregion

        #region Status Effects

        audioClips.Add(AudioClipName.BURNING, Resources.Load<AudioClip>("Sounds/Sound Effects/Status Effects/burning"));
        audioClips.Add(AudioClipName.FREEZING, Resources.Load<AudioClip>("Sounds/Sound Effects/Status Effects/freezing"));
        audioClips.Add(AudioClipName.UNFREEZING, Resources.Load<AudioClip>("Sounds/Sound Effects/Status Effects/unfreezing"));

        #endregion

        #region Menu

        audioClips.Add(AudioClipName.PAUSEMENU_POPUP, Resources.Load<AudioClip>("Sounds/Sound Effects/Menu/pausemenu-popup"));
        audioClips.Add(AudioClipName.BUTTON_ENTER, Resources.Load<AudioClip>("Sounds/Sound Effects/Menu/button-enter"));
        audioClips.Add(AudioClipName.BUTTON_PRESS, Resources.Load<AudioClip>("Sounds/Sound Effects/Menu/button-press"));

        #endregion

        #region Merchant

        audioClips.Add(AudioClipName.CHEST_CLOSE, Resources.Load<AudioClip>("Sounds/Sound Effects/Merchant/chest-close"));
        audioClips.Add(AudioClipName.CHEST_SHAKE, Resources.Load<AudioClip>("Sounds/Sound Effects/Merchant/chest-shake"));
        audioClips.Add(AudioClipName.CHEST_OPEN, Resources.Load<AudioClip>("Sounds/Sound Effects/Merchant/chest-open"));
        audioClips.Add(AudioClipName.MERCHANT_INTERACT, Resources.Load<AudioClip>("Sounds/Sound Effects/Merchant/merchant-interact"));
        audioClips.Add(AudioClipName.MERCHANT_TRADE, Resources.Load<AudioClip>("Sounds/Sound Effects/Merchant/merchant-trade"));

        #endregion

        #region Music

        // Music
        audioClips.Add(AudioClipName.MUSIC_CLASSICAL_DEMO, Resources.Load<AudioClip>("Sounds/Music/music-classical-demo"));
        audioClips.Add(AudioClipName.MUSIC_CLASSICAL_THE_SECOND_DEMO, Resources.Load<AudioClip>("Sounds/Music/music-classical-the-second-demo"));
        audioClips.Add(AudioClipName.MUSIC_DEATH, Resources.Load<AudioClip>("Sounds/Music/music-death"));
        audioClips.Add(AudioClipName.MUSIC_DEMON_KING, Resources.Load<AudioClip>("Sounds/Music/music-demon-king"));
        audioClips.Add(AudioClipName.MUSIC_WIN_ANTHEM, Resources.Load<AudioClip>("Sounds/Music/music-win-anthem"));

        #endregion

        #region Temporary sounds

        // Temp sounds
        audioClips.Add(AudioClipName.ARROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-arrow"));
        audioClips.Add(AudioClipName.BOW_SHOOT, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-bow-shoot"));
        audioClips.Add(AudioClipName.CLOSE, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-close"));
        audioClips.Add(AudioClipName.COMPLETED, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-completed"));
        audioClips.Add(AudioClipName.DOOR_OPEN, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-door-open"));
        audioClips.Add(AudioClipName.DROP, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-drop"));
        audioClips.Add(AudioClipName.ENEMY_ATTACK1, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-enemy-attack"));
        audioClips.Add(AudioClipName.ENEMY_ATTACK2, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-enemy-attack2"));
        audioClips.Add(AudioClipName.ENEMY_ATTACK3, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-enemy-attack3"));
        audioClips.Add(AudioClipName.ENEMY_ATTACK4, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-enemy-attack4"));
        audioClips.Add(AudioClipName.ENEMY_ATTACK5, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-enemy-attack5"));
        audioClips.Add(AudioClipName.ENEMY_ATTACK6, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-enemy-attack6"));
        audioClips.Add(AudioClipName.ENEMY_DAMAGE, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-enemy-damage"));
        audioClips.Add(AudioClipName.ENEMY_DEATH, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-enemy-death"));
        audioClips.Add(AudioClipName.ENEMY_DEATH2, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-enemy-death2"));
        audioClips.Add(AudioClipName.ENEMY_HURT, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-enemy-hurt"));
        audioClips.Add(AudioClipName.ENEMY_IDLE, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-enemy-idle"));
        audioClips.Add(AudioClipName.ENEMY_IDLE2, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-enemy-idle2"));
        audioClips.Add(AudioClipName.ENEMY_WALK, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-enemy-walk"));
        audioClips.Add(AudioClipName.FIRE_ARROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-fire-arrow"));
        audioClips.Add(AudioClipName.INSTRUCTION_CLICK, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-instruction-click"));
        audioClips.Add(AudioClipName.INVENTORY_CLOSE, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-inventory-close"));
        audioClips.Add(AudioClipName.INVENTORY_OPEN, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-inventory-open"));
        audioClips.Add(AudioClipName.MENU_SCROLL, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-menu-scroll"));
        audioClips.Add(AudioClipName.MENU_SELECT, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-menu-select"));
        audioClips.Add(AudioClipName.OPEN, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-open"));
        audioClips.Add(AudioClipName.PICKUP, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-pickup"));
        audioClips.Add(AudioClipName.PICKUP2, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-pickup2"));
        audioClips.Add(AudioClipName.PORTAL_OPEN, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-portal-open"));
        audioClips.Add(AudioClipName.PORTAL_SUMMON, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-portal-summon"));
        audioClips.Add(AudioClipName.RECIEVED, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-recieved"));
        audioClips.Add(AudioClipName.SHOOT, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-shoot"));
        audioClips.Add(AudioClipName.SHOOT2, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-shoot2"));
        audioClips.Add(AudioClipName.SWORD_MISS, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-sword-miss"));
        audioClips.Add(AudioClipName.SWORD_PICKUP, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-sword-pickup"));
        audioClips.Add(AudioClipName.TELEPORT, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-teleport"));
        audioClips.Add(AudioClipName.WALK, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-walk"));
        audioClips.Add(AudioClipName.WEAPON_DROP, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-weapon-drop"));
        audioClips.Add(AudioClipName.WEAPON_MISS, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-weapon-miss"));
        audioClips.Add(AudioClipName.WEAPON_MISS2, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-weapon-miss2"));
        audioClips.Add(AudioClipName.WEAPON_PICKUP, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-weapon-pickup"));
        audioClips.Add(AudioClipName.WEAPON_PICKUP2, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-weapon-pickup2"));
        audioClips.Add(AudioClipName.WEAPON_RELOAD, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-weapon-reload"));
        audioClips.Add(AudioClipName.WEAPON_THROW, Resources.Load<AudioClip>("Sounds/Sound Effects/Temp Sounds/temp-weapon-throw"));

        #endregion
    }

    /// <summary>
    /// Plays an audio clip
    /// </summary>
    /// <param name="name">Audio clip name</param>
    /// <param name="source">Source to play audio clips</param>
    public static void Play(AudioClipName name, AudioSource source)
    {
        source.PlayOneShot(audioClips[name]);
    }

    /// <summary>
    /// Plays an audio clip
    /// </summary>
    /// <param name="name">Audio clip name</param>
    /// <param name="location">Location to play audio clips</param>
    public static void Play(AudioClipName name, Vector3 location)
    {
        // Create new game object for temp audio source
        GameObject tempGameObj = new GameObject("Audio one shot");

        // Set the object's position
        tempGameObj.transform.position = location;

        // Add an audio source with our 3D audio settings
        AudioSource tempAudioSource = tempGameObj.AddComponent<AudioSource>();
        tempAudioSource.spatialBlend = 1f;

        tempAudioSource.maxDistance = 20f;

        tempAudioSource.rolloffMode = AudioRolloffMode.Linear;

        // Play the clip
        Play(name, tempAudioSource);

        // Destroy the object after the clip is finished
        Object.Destroy(tempGameObj, audioClips[name].length);
    }

    /// <summary>
    /// Plays an audio clip on a loop
    /// </summary>
    /// <param name="name">Audio clip name</param>
    /// <param name="source">Source to play audio clip</param>
    public static void PlayLoop(AudioClipName name, AudioSource source)
    {
        source.clip = audioClips[name];
        source.loop = true;
        source.Play();
    }

    /// <summary>
    /// Returns the length of a clip in the dictionary
    /// </summary>
    /// <param name="name">Clip name</param>
    /// <returns>Clip length</returns>
    public static float ClipLength(AudioClipName name)
    {
        return audioClips[name].length;
    }
}