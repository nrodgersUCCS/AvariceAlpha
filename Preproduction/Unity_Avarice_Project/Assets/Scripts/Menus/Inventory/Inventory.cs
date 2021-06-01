using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An inventory
/// </summary>
public class Inventory : MonoBehaviour
{
    // Holds items in slots and checks if they are full
    public bool[] isFull;
    public GameObject[] slots;
}