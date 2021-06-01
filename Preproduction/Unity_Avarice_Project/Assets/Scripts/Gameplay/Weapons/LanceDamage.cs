using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceDamage : MonoBehaviour
{
    /// <summary>
    /// On collision destroy the other object
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
