using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarokDamage : MonoBehaviour
{
    AudioSource audioSource;
    GameObject player;          // Player object

    // Start is called before the first frame update
    void Start()
    {
        // Start delay
        StartCoroutine(ExecuteAfterTime(6));

        // Play audio
        audioSource = GetComponent<AudioSource>();
        AudioManager.Play(AudioClipName.vsRagnarokCast, audioSource);
        AudioManager.Play(AudioClipName.vsRagnarokDuration, audioSource);

        player = GameObject.FindWithTag("Player");
        transform.parent = null;
        transform.position = player.transform.position;
        transform.parent = player.transform;
    }

    /// <summary>
    /// On collision destroy the other object
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            if(collision.gameObject.tag == "Enemy")
            {
                Destroy(collision.gameObject);
                if (Camera.main.gameObject.GetComponent<ScreenShake>() != null)
                    Camera.main.GetComponent<ScreenShake>().Shake(0.5f, 0.2f, 1.5f);
            }
        }
    }

    /// <summary>
    /// After delay destroy object
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        player.GetComponent<PlayerMove>().setMoveSpeed(ConfigurationUtils.PlayerMoveSpeed);
        Destroy(gameObject);
    }
}
