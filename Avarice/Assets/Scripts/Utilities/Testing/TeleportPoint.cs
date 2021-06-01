using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An object to teleport the player to a given point
/// </summary>
public class TeleportPoint : MonoBehaviour
{
    private GameObject player;              // The player

    [Tooltip("The key to press for teleportation")]
    public KeyCode Key = KeyCode.None;

    [Tooltip("A description of the location of the teleport point")]
    public string LocationDescription;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Find player object
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Teleport player on key press
        if (Input.GetKeyDown(Key))
        {
            Vector3 pos = (Vector2)(transform.position);
            pos.z = 0;
            player.transform.position = pos;
        }
    }
}
