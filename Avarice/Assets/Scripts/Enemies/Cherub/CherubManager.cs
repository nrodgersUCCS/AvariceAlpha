using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to handle multiple fallen cherubs that are aggressive toward the player
/// </summary>
public class CherubManager : MonoBehaviour
{
    // Constants
    private float PLAYER_DISTANCE;                          // Distance from the player that the cherubs should be when encircling the player
    private float ROTATION_SPEED;                           // How fast the cherubs rotate around the player

    // Variables
    private static GameObject instance;                     // The singleton instance
    private List<GameObject> cherubs;                       // A list of fallen cherubs
    private GameObject player;                              // The player
    private float startingAngle = 0;                        // The angle of the first cherub from the player

    /// <summary>
    /// The cherub manager singleton object
    /// </summary>
    public static GameObject Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("CherubManager");
                instance.AddComponent<CherubManager>();
            }
            return instance;
        }
    }

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Start()
    {
        // Set Constants
        CherubValues container = (CherubValues)Constants.Values(ContainerType.CHERUB);
        PLAYER_DISTANCE = container.PlayerDistance;
        ROTATION_SPEED = container.RotationSpeed;

        // Set up prefabs
        player = GameObject.FindWithTag("Player");

        // Set up list of cherubs
        cherubs = new List<GameObject>();

    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        EncirclePlayer(startingAngle);
        startingAngle += ROTATION_SPEED;
    }

    /// <summary>
    /// Adds a cherub to the list of cherubs
    /// </summary>
    /// <param name="cherub">The cherub to be added</param>
    public void AddCherub(GameObject cherub)
    {
        cherubs.Add(cherub);
    }

    /// <summary>
    /// Removes a cherub from the list of cherubs
    /// </summary>
    /// <param name="cherub"></param>
    public void RemoveCherub(GameObject cherub)
    {
        cherubs.Remove(cherub);
    }

    /// <summary>
    /// Makes the fallen cherubs encircle the player, cherubs will attempt to
    /// get into positions within a circle with the player at the center with a radius of DISTANCE
    /// </summary>
    /// <param name="firstAngle">The starting angle</param>
    private void EncirclePlayer(float firstAngle)
    {
        float angle = firstAngle;                                           // The angle from the player a cherub needs to be
        Vector2 position;                                                   // The position a cherub needs to go to

        // For each cherub send them to a position within a circle
        foreach( GameObject cherub in cherubs )
        {
            // Find the position to send a cherub
            position = new Vector3((float)Mathf.Sin(Mathf.Deg2Rad * angle) * PLAYER_DISTANCE + player.transform.position.x, (float)Mathf.Cos(Mathf.Deg2Rad * angle) * PLAYER_DISTANCE + player.transform.position.y, 0);
            cherub.GetComponent<Cherub>().SetDestination(position);

            // Increase the angle for the next cherub
            angle += 360 / (cherubs.Count);
        }
    }
}
