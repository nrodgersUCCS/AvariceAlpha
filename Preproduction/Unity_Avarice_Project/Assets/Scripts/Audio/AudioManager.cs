
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to manage all game audio
/// </summary>
public static class AudioManager
{
    static bool initialized = false;                            // Whether the audio manager has been initialized
    static Dictionary<AudioClipName, AudioClip> audioClips =    // Dictionary of audio clips
        new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Whether the audio manager has been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Initializes the audio manager and loads all audio clips
    /// </summary>
    /// <param name="source">Source to play audio clips</param>
    public static void Initialize()
    {
        // Initialize audio manager
        initialized = true;

        // TODO: Add audio clips
        audioClips.Add(AudioClipName.vsSkeletonAttack, Resources.Load<AudioClip>("Sounds/Enemies/Skeleton/skeleton-attack"));
        audioClips.Add(AudioClipName.vsSkeletonDeath, Resources.Load<AudioClip>("Sounds/Enemies/Skeleton/skeleton-death"));
        audioClips.Add(AudioClipName.vsSkeletonHurt, Resources.Load<AudioClip>("Sounds/Enemies/Skeleton/skeleton-hurt"));
        audioClips.Add(AudioClipName.vsSkeletonWalk, Resources.Load<AudioClip>("Sounds/Enemies/Skeleton/skeleton-walk"));
        audioClips.Add(AudioClipName.vsQuestCompleted, Resources.Load<AudioClip>("Sounds/vs-quest-completed"));
        audioClips.Add(AudioClipName.vsQuestRecieved, Resources.Load<AudioClip>("Sounds/vs-quest-recieved"));
        audioClips.Add(AudioClipName.vsOpenInventory, Resources.Load<AudioClip>("Sounds/Inventory/vs-inventory-open"));
        audioClips.Add(AudioClipName.vsCloseInventory, Resources.Load<AudioClip>("Sounds/Inventory/vs-inventory-close"));
        audioClips.Add(AudioClipName.vsCharacterWalk, Resources.Load<AudioClip>("Sounds/Character/vs-character-walk"));
        audioClips.Add(AudioClipName.vsCharacterWallHit, Resources.Load<AudioClip>("Sounds/Character/vs-character-wall-hit"));
        audioClips.Add(AudioClipName.vsSwordMiss, Resources.Load<AudioClip>("Sounds/Weapon/vs-sword-miss"));
        audioClips.Add(AudioClipName.vsSwordHit, Resources.Load<AudioClip>("Sounds/Weapon/vs-sword-hit"));
        audioClips.Add(AudioClipName.vsBowShoot, Resources.Load<AudioClip>("Sounds/Weapon/vs-bow-shoot"));
        audioClips.Add(AudioClipName.vsCrossbowShoot, Resources.Load<AudioClip>("Sounds/Weapon/vs-crossbow-shoot"));
        audioClips.Add(AudioClipName.vsProjectileMiss, Resources.Load<AudioClip>("Sounds/Weapon/vs-projectile-miss"));
        audioClips.Add(AudioClipName.vsDaggerThrow, Resources.Load<AudioClip>("Sounds/Weapon/vs-dagger-throw"));
        audioClips.Add(AudioClipName.vsDaggerMiss, Resources.Load<AudioClip>("Sounds/Weapon/vs-dagger-miss"));
        audioClips.Add(AudioClipName.vsLancePoke, Resources.Load<AudioClip>("Sounds/Weapon/vs-lance-poke"));
        audioClips.Add(AudioClipName.vsLanceMiss, Resources.Load<AudioClip>("Sounds/Weapon/vs-lance-miss"));
        audioClips.Add(AudioClipName.vsRagnarokCast, Resources.Load<AudioClip>("Sounds/Weapon/vs-ragnarok-cast"));
        audioClips.Add(AudioClipName.vsRagnarokDuration, Resources.Load<AudioClip>("Sounds/Weapon/vs-ragnarok-duration"));
        audioClips.Add(AudioClipName.vsCherubRoar, Resources.Load<AudioClip>("Sounds/Enemies/Cherub/vs-cherub-roar"));
        audioClips.Add(AudioClipName.vsCherubHurt, Resources.Load<AudioClip>("Sounds/Enemies/Cherub/vs-cherub-hurt"));
        audioClips.Add(AudioClipName.vsCherubDie, Resources.Load<AudioClip>("Sounds/Enemies/Cherub/vs-cherub-die"));
        audioClips.Add(AudioClipName.vsTradeInteraction, Resources.Load<AudioClip>("Sounds/Trader/vs-trading-interaction-sound"));
        audioClips.Add(AudioClipName.vsTradeLeaving, Resources.Load<AudioClip>("Sounds/Trader/vs-trading-leaving-sound"));
        audioClips.Add(AudioClipName.vsTradeTransaction, Resources.Load<AudioClip>("Sounds/Trader/vs-trading-transaction"));
        audioClips.Add(AudioClipName.vsTradeFail, Resources.Load<AudioClip>("Sounds/Trader/vs-trading-fail"));
        audioClips.Add(AudioClipName.vsCoinPickup, Resources.Load<AudioClip>("Sounds/Money/vs-loot-coin-pickup"));
        audioClips.Add(AudioClipName.vsCoinDrop, Resources.Load<AudioClip>("Sounds/Money/vs-loot-coin-drop"));
        audioClips.Add(AudioClipName.vsSongDeath, Resources.Load<AudioClip>("Sounds/Character/vs-song-death"));
        audioClips.Add(AudioClipName.vsArmorPickup, Resources.Load<AudioClip>("Sounds/Inventory/vs-armor-pickup"));
        audioClips.Add(AudioClipName.vsSkeloDevilAttack, Resources.Load<AudioClip>("Sounds/Enemies/SkeloDevil/vs-skelodevil-attack"));
        audioClips.Add(AudioClipName.vsSkelodevilIdle, Resources.Load<AudioClip>("Sounds/Enemies/SkeloDevil/vs-skelodevil-idle"));
        audioClips.Add(AudioClipName.vsBackgroundSong, Resources.Load<AudioClip>("Sounds/vs-song-background"));
        audioClips.Add(AudioClipName.vsItemFanfare, Resources.Load<AudioClip>("Sounds/Character/vs-song-item-fanfare"));
    }

    /// <summary>
    /// Plays an audio clip
    /// </summary>
    /// <param name="name">Audio clip name</param>
    public static void Play(AudioClipName name, AudioSource source)
    {
        source.PlayOneShot(audioClips[name]);
    }
}

