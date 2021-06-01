using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hermit_Test_SceneGenerator : MonoBehaviour
{
    private GameObject playerPrefab;    // Player prefab
    private GameObject terrain;         // Scene terrain prefab
    private GameObject hermit;          // Hermit prefab
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
        dagger = Resources.Load<GameObject>("Prefabs/Weapons/Dagger");

        // Instantiate player
        Instantiate(playerPrefab);

        // Instantiate terrain
        Instantiate(terrain);

        // Instantiate hermit
        Instantiate(hermit, new Vector3(20f, 0f, 0f), Quaternion.identity);

        // Instantiate the dagger
        Instantiate<GameObject>(dagger, Vector3.zero, Quaternion.identity);
        dagger.transform.position = new Vector3(1.27f, -1.45f);
    }
}
