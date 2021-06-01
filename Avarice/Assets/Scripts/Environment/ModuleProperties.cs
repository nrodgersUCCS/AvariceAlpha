using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to hold the serialized values for modules
/// </summary>
public class ModuleProperties : MonoBehaviour
{
    [Tooltip("The position (relative to (0, 0)) at which the player will spawn if this module is chosen as the starting module")]
    public Vector3 PlayerStartPosition;
}
