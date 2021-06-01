using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The parts of the SkeloDevil Enemy
/// </summary>
public class SkeloDevilBodyPart : MonoBehaviour
{ 
    private bool inFlight;
    private Vector3 lastPos;    // Determines if part is still on skelodevil or not


    //targeting vars
    Vector3 newTarget;

    // Update is called once per frame
    private void Update()
    {
        int i = 5;

        //If the Part is attacking
        if (inFlight)
        {
            // Makes the body part spin if game isn't paused
            if (!PauseManager.Instance.IsPaused)
            {
                gameObject.transform.Rotate(0, 0, i);
                i += 5;
            }

            //moves towards target location
            if (gameObject.transform.position != newTarget)
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, newTarget, 
                    6.0f * Time.deltaTime);

            else
                inFlight = false;
        }
    }

    /// <summary>
    /// Moves part back to parent
    /// </summary>
    public void Assemble()
    {
        inFlight = false;
        gameObject.transform.position = new Vector3(gameObject.transform.parent.position.x, 
            gameObject.transform.parent.position.y);
        gameObject.transform.rotation = gameObject.transform.parent.rotation;
    }

    /// <summary>
    /// Uses passed in Vector3 to start pathing
    /// </summary>
    /// <param name="attackLocation"></param>
    public void Attack(Vector3 attackLocation)
    {
        lastPos = transform.position; // Sets Lastpos to current position before throwing part
        newTarget = attackLocation;
        inFlight = true;
    }

    /// <summary>
    /// Runs when a collision is detected
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If piece is not attached to the skelodevil & it gets hit by a thrown weapon, stun skelodevil
        if (collision.gameObject.GetComponent<ThrowableItem>() != null && GetComponent<SpriteRenderer>().enabled && transform.position != lastPos)
        {
            if (collision.gameObject.GetComponent<ThrowableItem>().InFlight || collision.gameObject.GetComponent<Warhammer>())
            {
                inFlight = false;
                transform.parent.gameObject.GetComponent<SkeloDevil>().Stun(gameObject);
            }
        }

        // Stops part from moving when it hits a wall
        if(collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
            inFlight = false;

        // If a decoration was hit, damage it
        if (collision.gameObject.GetComponent<Decoration>() != null)
            collision.gameObject.GetComponent<Decoration>().DamageItem(collision.gameObject.transform.position);
    }
}
