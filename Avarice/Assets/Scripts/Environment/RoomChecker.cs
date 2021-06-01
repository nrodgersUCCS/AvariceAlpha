using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks room for enemies & player
/// </summary>
public class RoomChecker : MonoBehaviour
{
    private bool enemiesInRoom;                                             // Whether enemies are in the room
    private bool playerInRoom;                                              // Whether the player is in the room

    private int enemyCount;                                                 // How many enemies are in the room

    private List<GateController> gates = new List<GateController>();        // A list of gates in the room

    /// <summary>
    /// Adds a gate to the room checker
    /// </summary>
    public void AddGate(GateController gate)
    {
        gates.Add(gate);
    }

    /// <summary>
    /// Runs when a something enter the gameObject's trigger
    /// </summary>
    /// <param name="collision">The </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If enemy entered, add 1 to enemies in room
        if (collision.gameObject.GetComponent<Enemy>() != null)
            enemyCount++;

        // If player entered, set player in room to true
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
            playerInRoom = true;

        // If player enters the room when there are enemies, close the gates
        if (playerInRoom && enemyCount > 0)
        {
            foreach (GateController gate in gates)
            {
                if (gate.IsOpen)
                    gate.CloseGate();
            }
        }
    }

    /// <summary>
    /// Runs when a something exits the gameObject's trigger
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        // If enemy left room, remove 1 from enemies in room
        if (collision.gameObject.GetComponent<Enemy>() != null)
            enemyCount--;

        // If player left, set player in room to false
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
            playerInRoom = false;

        // If the player kills the last enemy in the room, open the gates
        if (enemyCount <= 0 && playerInRoom)
        {
            foreach (GateController gate in gates)
            {
                if (!gate.IsOpen)
                    gate.OpenGate();
            }
        }
    }
}
