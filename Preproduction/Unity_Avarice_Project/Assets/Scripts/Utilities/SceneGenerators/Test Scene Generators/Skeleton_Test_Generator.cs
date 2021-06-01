using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Test_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject skeletonPrefab;
    private GameObject dummyPrefab;

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        ///////////////////////////////////////
        // LOAD EACH PREFAB RESOURCE ONLY ONCE
        ///////////////////////////////////////
        skeletonPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/Skeleton");
        dummyPrefab = Resources.Load<GameObject>("Prefabs/DummyObject");

        ///////////////////////////////////////////
        // ADD IN OBJECTS
        // PERFORM ANY NECEESARY ACTIONS 
        // (ROTATIONS, SCALING, ADD SCRIPTS, ETC.)
        ///////////////////////////////////////////

        // Player
        GameObject player = Instantiate<GameObject>(dummyPrefab, new Vector3(-6.13f, -2.78f), Quaternion.identity);
        {
            // Set tag
            player.tag = "Player";

            // Add sprite renderer
            SpriteRenderer sr = player.AddComponent<SpriteRenderer>();
            sr.sprite = Resources.Load<Sprite>("Sprites/Player/spr_CharacterFrame1");
        }

        // Skeleton
        Instantiate<GameObject>(skeletonPrefab);

        // Screen shake
        Camera.main.gameObject.AddComponent<ScreenShake>();
    }
}
