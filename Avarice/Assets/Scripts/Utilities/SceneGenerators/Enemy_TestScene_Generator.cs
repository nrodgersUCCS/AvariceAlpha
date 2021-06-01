using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TestScene_Generator : MonoBehaviour
{
    private GameObject playerPrefab;    // Player prefab
    private GameObject terrain;         // Scene terrain prefab
    //Allows for enemy to be choosen by the person playing
    [SerializeField]
    private GameObject enemy = null;    // Enemy prefab

    [SerializeField]
    private int enemyCount = 0;         // Enemy prefab


    /// <summary>
    /// Called before start
    /// </summary>
    private void Awake()
    {
        // Get prefab assets
        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        terrain = Resources.Load<GameObject>("Prefabs/Environment/Terrain/Enemy_TestScene_Terrain");

        // Instantiate player
        Instantiate(playerPrefab, new Vector3(-28f, 3f, 0f), Quaternion.identity);

        // Instantiate terrain
        Instantiate(terrain);

        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemy, new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-1f, 1f)), Quaternion.identity);
        }
    }
}
