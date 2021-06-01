using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Legendary dagger weapon 
/// </summary>
public class Hoarfrost : Dagger
{
    // Serialize Fields for the route the dagger will follow
    [SerializeField]
    private Transform[] routes = null;

    // Support for Bezier Curve routing
    private float tParam;
    private Vector2 selfPos;

    //  Speed at which the dagger follows the curve
    protected float SPEED_MODIFIER;

    //  Support for collisions
    private List<Enemy> hitEnemies;                     // Enemies that have been hit by the dagger  
    Collider2D childP;                                  // Child of the dagger (player collision)

    /// <summary>
    /// Called before start
    /// </summary>
    protected override void Awake()
    {
        //  Set the weapon parameters to ensure its a legendary frost weapon
        IsLegendary = true;

        // Call parent method
        base.Awake();

        // Change throw sound
        // Change to Hoarfrost sound when it is in
        throwSound = AudioClipName.HOARFROST_THROW;

        // Initilize values for Bezier Curve routing
        tParam = 0f;
        SPEED_MODIFIER = ((HoarfrostValues)Constants.Values(ContainerType.HOARFROST)).TravelTime 
            * ((HoarfrostValues)Constants.Values(ContainerType.HOARFROST)).TravelTimeMultiplier;

        // Get SPIN_TORQUE from Hoarfrost value
        SPIN_TORQUE = ((HoarfrostValues)Constants.Values(ContainerType.HOARFROST)).SpinTorque 
            * ((HoarfrostValues)Constants.Values(ContainerType.HOARFROST)).SpinTorqueMultiplier;

        // Create list of enemies
        hitEnemies = new List<Enemy>();

        // Get child for player collision
        childP = gameObject.transform.GetChild(2).GetComponent<Collider2D>();
    }

    /// <summary>
    /// Detach the dagger from the player and start it along the path, spin the dagger as well
    /// </summary>
    public override void Detach()
    {
        // Detach from player
        Vector2 throwDirection = -transform.root.up;
        transform.parent = null;
        InFlight = true;
        canBePickedUp = false;

        //  Give the dagger a rigid body
        Rigidbody2D rb2d = gameObject.GetComponent<Rigidbody2D>();
        if (rb2d == null)
           rb2d = gameObject.AddComponent<Rigidbody2D>();

        // Enable collider
        collider.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("PlayerAttacks");
        collider.isTrigger = false;

        // Play item throw sound
        AudioManager.Play(throwSound, audioSource);

        // Start delay for player collision
        StartCoroutine(PlayerCollision(1));

        // Start routing for dagger
        StartCoroutine(Routing(0));

        // Start dagger spin
        GetComponent<Rigidbody2D>().AddTorque(SPIN_TORQUE, ForceMode2D.Force);
    }

    /// <summary>
    /// Delay for enabling player collider
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator PlayerCollision(float delay)
    {
        // Wait for delay time
        yield return new WaitForSeconds(delay);

        // Play return sound
        AudioManager.Play(AudioClipName.HOARFROST_RETURN, audioSource);

        // Enable childs collider (player)
        childP.enabled = true;
        childP.isTrigger = true;
    }


    /// <summary>
    /// Start pathing along the bezier curve
    /// </summary>
    /// <param name="routeNumber"></param>
    /// <returns></returns>
    private IEnumerator Routing(int routeNumber)
    {

        //  Get the positions of all the points along the route
        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;

        //  Travel along the route created by the points
        while (tParam < 1)
        {
            tParam += Time.deltaTime * SPEED_MODIFIER;

            //Bezier Route Equation
            selfPos = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = selfPos;
            yield return new WaitForEndOfFrame();
        }

        //  Reset the route timing so it can be thrown again
        tParam = 0f;

        //  Land the dagger at the end of the route
        StartCoroutine(Land(0f));        
    }

    /// <summary>
    /// Makes the object land after its travel time
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator Land(float delay)
    {
        yield return base.Land(delay);

        // Clear hit list of enemies
        hitEnemies.Clear(); 

        // Reset routing along Bezier curve
        tParam = 0f;
    }

    /// <summary>
    /// Called upon the child of the dagger making a collision
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //allow player to pick item up mid air
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null && InFlight)
        {
            StartCoroutine(Land(0f));

            // Stop routing along Bezier curve
            tParam = 1f;
        }
    }

    /// <summary>
    /// Called on collision
    /// </summary>
    /// <param name="col">Collision info</param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
        Decoration decoration = collision.gameObject.GetComponent<Decoration>();

        if (enemyScript != null && InFlight && !hitEnemies.Contains(enemyScript))
        {
            // Add enemy to the list
            hitEnemies.Add(enemyScript);

            // Play enemy hit sound
            AudioManager.Play(enemyHitSound, audioSource);

            // Damage enemy
            float damage = DAMAGE * player.GetComponent<PlayerInventory>().GreedMultiplier;
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, Type, FLAME_POWER, FROST_POWER);
        }

        // If colliding with a decoration
        if (decoration != null && InFlight)
        {
            // Stop movement if decoration is not destroyed
            if (decoration.CurrentHealth > 1)
            {
                StartCoroutine(Land(0f));

                // Stop routing along Bezier curve
                tParam = 1f;
            }

            // Damage decoration
            decoration.DamageItem(collision.transform.position);
        }

        // Ensure that its not colliding with an enemy or decoration 
        if (enemyScript == null && decoration == null)
        {
            // Play dagger hit wall sound
            AudioManager.Play(wallHitSound, audioSource);

            // Stop movement if dagger hits wall
            StartCoroutine(Land(0f));

            // Stop routing along Bezier curve
            tParam = 1f;
        }
    }
}
