using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The indicator for the range of the player's current weapon
/// </summary>
public class ThrowRangeIndicator : MonoBehaviour
{
    // Variables
    float distance;                 // The distance that a weapon can be thrown, also the radius to make the circle
    ThrowableItem item;             // The item that the player is currently holding
    SpriteRenderer spriteRenderer;  // The indicator's sprite renderer

    /// <summary>
    /// The indicator's sprite renderer
    /// </summary>
    private SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
            return spriteRenderer;
        }
    }

    void Awake()
    {
        // Disable sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        // Set indicator opacity
        Color temp = spriteRenderer.color;
        temp.a = ((PlayerValues)Constants.Values(ContainerType.PLAYER)).RangeIndicatorOpaicty;
        temp.a = Mathf.Clamp01(temp.a);
        spriteRenderer.color = temp;
    }

    void Update()
    {
        UpdateIndicator();
    }

    /// <summary>
    /// Updates the circle to match whatever item the player is holding
    /// </summary>
    public void UpdateIndicator()
    {
        // Get any decoration the player is holding
        item = transform.parent.gameObject.GetComponent<PlayerThrow>().CurrentDecoration;

        // If the player isn't holding a decoration, get the top item in the throw stack instead
        if(item == null)
        {
            item = transform.parent.gameObject.GetComponent<PlayerThrow>().CurrentItem;
        }

        // If the player is holding an item that can be thrown
        if(item != null)
        {
            // Enable sprite renderer
            SpriteRenderer.enabled = true;

            // Calculate the distance that the weapon can travel
            distance = item.Range;

            // Scale the distance
            distance *= .054f;

            // Resize the ring accordingly
            transform.localScale = new Vector3(distance * 2, distance * 2, 1);
        }
        else
        {
            // Disable sprite renderer
            SpriteRenderer.enabled = false;
        }
    }
}
