using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An arrow
/// </summary>
public class Arrow : MonoBehaviour
{
    // Where the arrow should go
    public Vector3 AimLocation { private get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // Makes the arrow face the aim location
        Vector3 targ = AimLocation - transform.position;
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        // Makes the arrow fly in the direction it's facing
        GetComponent<Rigidbody2D>().AddForce(targ * 135f, ForceMode2D.Force);
    }

    /// <summary>
    /// Arrow collision behaviour
    /// </summary>
    /// <param name="collision">The collided object</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy arrow if collided with player or environmental object
        if (collision.gameObject.tag == "Player" || collision.gameObject.layer == LayerMask.NameToLayer("EnvironmentalObjects"))
        {
            Destroy(gameObject);
        }
        // Stop arrow & remove collider and rigidbody
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject.GetComponent<Rigidbody2D>());
        }
    }
}