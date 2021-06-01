using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A wall checker
/// </summary>
public class WallCheck : MonoBehaviour
{
    // Used by parent
    public bool wallHit { get; private set; }

    // Check if collided with wall or room decoration
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Walls") || 
            collision.gameObject.layer == LayerMask.NameToLayer("EnvironmentalObjects"))
        {
            wallHit = true;
        }
    }

    // Checks if stopped colliding with wall or room decoration
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Walls") ||
            collision.gameObject.layer == LayerMask.NameToLayer("EnvironmentalObjects"))
        {
            wallHit = false;
        }
    }
}
