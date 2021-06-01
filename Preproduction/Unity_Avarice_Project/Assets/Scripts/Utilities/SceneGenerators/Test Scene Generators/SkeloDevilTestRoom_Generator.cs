using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeloDevilTestRoom_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject SekeloDevilPrefab;
    private GameObject PlayerPrefab;
    private GameObject worldBoundsPrefab;
    private GameObject SwordPrefab;
    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        ///////////////////////////////////////
        // LOAD EACH PREFAB RESOURCE ONLY ONCE
        ///////////////////////////////////////
        SekeloDevilPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/SkeloDevil");
        PlayerPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Player/Player");
        worldBoundsPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/WorldBounds");
        SwordPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Weapons/Sword");
        ///////////////////////////////////////////
        // ADD IN OBJECTS
        // PERFORM ANY NECEESARY ACTIONS 
        // (ROTATIONS, SCALING, ADD SCRIPTS, ETC.)
        ///////////////////////////////////////////

        // World Bounds
        GameObject worldBounds = Instantiate(worldBoundsPrefab);
        {
            worldBounds.GetComponent<BoxCollider2D>().offset = new Vector2(0.01607323f, 0.0335722f);
            worldBounds.GetComponent<BoxCollider2D>().size = new Vector2(35.36175f, 19.60032f);
        }

        // Player
        GameObject player = Instantiate<GameObject>(PlayerPrefab, new Vector3(-6.13f, -2.78f), Quaternion.identity);
        {
            // Set tag
            player.tag = "Player";
        }

        // SkeloDevil
        GameObject skeloDevil = Instantiate<GameObject>(SekeloDevilPrefab, new Vector3(6.13f, 2.78f), Quaternion.identity);
        {
            skeloDevil.tag = "Enemy";
        }

        GameObject sword = Instantiate(SwordPrefab, player.transform);
        sword.transform.localScale = new Vector3(2f, 2f, 1f);
    }
}
