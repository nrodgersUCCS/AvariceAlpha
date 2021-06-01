using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot_TestScene_Generator : MonoBehaviour
{
    private GameObject playerPrefab;    // Player prefab
    private GameObject terrain;         // Scene terrain prefab
    private GameObject hermit;          // Hermit prefab
    private GameObject cherub;          // Cherub prefab
    private GameObject eater;           // Corpse Eater prefab
    private GameObject bandit;          // Bandit Ranger prefab
    private GameObject dagger;          // Dagger prefab

    /// <summary>
    /// Called before start
    /// </summary>
    private void Awake()
    {
        // Get prefab assets
        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        terrain = Resources.Load<GameObject>("Prefabs/Environment/Terrain/PlayerMovement_TestSCene_Terrain");
        hermit = Resources.Load<GameObject>("Prefabs/Enemies/Hermit");
        cherub = Resources.Load<GameObject>("Prefabs/Enemies/Cherub/Cherub");
        eater = Resources.Load<GameObject>("Prefabs/Enemies/CorpseEater");
        bandit = Resources.Load<GameObject>("Prefabs/Enemies/BanditRanger/BanditRanger");
        dagger = Resources.Load<GameObject>("Prefabs/Weapons/Dagger");

        // Instantiate player
        Instantiate(playerPrefab);

        // Instantiate terrain
        Instantiate(terrain);

        // Instantiate enemies
        Instantiate(hermit, new Vector3(20f, 10f, 0f), Quaternion.identity);
        Instantiate(cherub, new Vector3(20f, -10f, 0f), Quaternion.identity);
        Instantiate(eater, new Vector3(-20f, 10f, 0f), Quaternion.identity);
        Instantiate(bandit, new Vector3(-20f, -10f, 0f), Quaternion.identity);

        // Instantiate daggers
        Instantiate<GameObject>(dagger, Vector3.zero, Quaternion.identity);
        Instantiate<GameObject>(dagger, Vector3.zero, Quaternion.identity);
        Instantiate<GameObject>(dagger, Vector3.zero, Quaternion.identity);
        Instantiate<GameObject>(dagger, Vector3.zero, Quaternion.identity);
        // dagger.transform.position = new Vector3(1.27f, -1.45f);
    }
}

