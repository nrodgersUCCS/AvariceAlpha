using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ObjectShadows : MonoBehaviour
{
    GameObject shadow;                  // The object's shadow
    float ANGLE;                        // Angle at which to cast the shadow
    float OFFSET;                       // Shadow's offset position
    Vector2 OffsetVector;               // The vector offset of the shadow
    SpriteRenderer parentRenderer;      // Parent object sprite renderer
    SpriteRenderer shadowRenderer;      // Shadow object sprite renderer

    // Start is called before the first frame update
    void Start()
    {
        // Get angle and offset constants
        ANGLE = ((MapValues)Constants.Values(ContainerType.MAP)).Angle;
        OFFSET = ((MapValues)Constants.Values(ContainerType.MAP)).Offset;
        float radians = ANGLE * Mathf.Deg2Rad;
        OffsetVector = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized * OFFSET;

        // Create Shadow object and make it a child of the object
        shadow = new GameObject("Shadow");
        shadow.transform.parent = transform;

        // Sets the shadow's postion and rotation
        shadow.transform.localPosition = OffsetVector;
        shadow.transform.localRotation = Quaternion.identity;

        // If armor, adjust the shadow
        if (shadow.GetComponentInParent<Armor>())
            shadow.transform.localPosition = OffsetVector / 2f;

        // Creates shadow sprite renderer, and sets it to the parent sprite
        parentRenderer = GetComponent<SpriteRenderer>();
        shadowRenderer = shadow.AddComponent<SpriteRenderer>();
        shadowRenderer.sprite = parentRenderer.sprite;
        shadowRenderer.transform.localScale = Vector3.one;

        // Sets material passed through inspector
        shadowRenderer.material = Resources.Load<Material>("Materials/ShadowMaterial");

        // The spear sprite needs to be flipped by the Y-axis
        shadowRenderer.flipY = parentRenderer.flipY;

        // Sets sorting layer as the same as the parent's, and the sorting order one layer behind the parent
        shadowRenderer.sortingLayerName = "Shadows";
    }

    /// <summary>
    /// Called after all Update functions have been called
    /// </summary>
    void LateUpdate()
    {
        //Get the throwableItem Script attached if there is one and get the color of the shadow
        ThrowableItem parentObject = GetComponentInParent<ThrowableItem>();
        Color shadowAlpha = shadowRenderer.color;

        //Set some objects to have no shadow when on the ground
        if (parentObject != null && !parentObject.InFlight)
        {
            if (parentObject.GetComponent<Weapon>() != null ||
                parentObject.Name == ItemName.PLATE ||
                parentObject.Name == ItemName.FORK ||
                parentObject.Name == ItemName.KNIFE)
            {
                shadowAlpha.a = 0.0f;
            }
        }
        else
        {
            //Else set it to 1
            shadowAlpha.a = 1.0f;
        }

        //Re assign the color so the alpha changes are seen
        shadowRenderer.color = shadowAlpha;

        // Updates the shadow sprite to the same as the parent
        shadowRenderer.sprite = parentRenderer.sprite;

        //Move the shadow so all items act like there is just one source of light
        shadow.transform.position = transform.position + (Vector3)OffsetVector;
    }
}
