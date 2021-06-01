using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Knocks the player back on collision
/// </summary>
public class KnockBackPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMove>().KnockBack(transform.position);
        }
    }
}
