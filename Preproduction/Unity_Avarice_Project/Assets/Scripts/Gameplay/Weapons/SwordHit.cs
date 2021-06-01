using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHit : MonoBehaviour
{
    private AudioSource audioSource;    // Sword audio source

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// On collision destroy the other object
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(Camera.main.GetComponent<ScreenShake>() != null)
            {
                Camera.main.GetComponent<ScreenShake>().Shake(0.5f, 0.2f, 1.5f);
            }
            
            if (collision.gameObject.TryGetComponent<Zombie>(out Zombie zombie))
            {
                zombie.TakeDamage(3);
            }
            AudioManager.Play(AudioClipName.vsSwordHit, audioSource);
        }
    }
}
