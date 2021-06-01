using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback_Test_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject playerPrefab;
    private GameObject armorPrefab;
    private GameObject worldBoundsPrefab;
    private GameObject fadeCanvasPrefab;
    private GameObject dummyPrefab;

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        ///////////////////////////////////////
        // LOAD EACH PREFAB RESOURCE ONLY ONCE
        ///////////////////////////////////////
        playerPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Player/Player");
        armorPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Armor/Enchanted Armor");
        worldBoundsPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/WorldBounds");
        fadeCanvasPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/FadeCanvas");
        dummyPrefab = Resources.Load<GameObject>("Prefabs/DummyObject");

        ///////////////////////////////////////////
        // ADD IN OBJECTS
        // PERFORM ANY NECEESARY ACTIONS 
        // (ROTATIONS, SCALING, ADD SCRIPTS, ETC.)
        ///////////////////////////////////////////

        // World bounds
        Instantiate(worldBoundsPrefab);

        // Fade canvas
        Instantiate(fadeCanvasPrefab);

        // Player
        GameObject player = Instantiate(playerPrefab);
        {
            // Add armor
            player.GetComponent<PlayerInventory>().AddArmor(armorPrefab);
        }

        // Skeleton dummy
        GameObject dummy = Instantiate<GameObject>(dummyPrefab, new Vector2(4.25f, 0f), Quaternion.identity);
        {
            // Add sprite and collider
            dummy.AddComponent<SpriteRenderer>();
            dummy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Enemies/FleshySkeleton/spr_EnemyFleshySkeleton0");
            BoxCollider2D col = dummy.AddComponent<BoxCollider2D>();
            col.isTrigger = true;

            // Add knockback script
            dummy.AddComponent<KnockBackPlayer>();
        }
    }
}
