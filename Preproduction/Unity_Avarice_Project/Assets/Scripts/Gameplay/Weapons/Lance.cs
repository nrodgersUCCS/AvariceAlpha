using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lance : MonoBehaviour
{
    Animator animator;
    //AudioSource audioSource;
    private bool lanceCooldown = false;
    private PlayerMove playerMoving;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMoving = GetComponent<PlayerMove>();
        //audioSource = transform.parent.GetComponent<AudioSource>();
        //AudioManager.Initialize(audioSource);
    }

    // Update is called once per frame
    void Update()
    {
        // On keydown press for space set the trigger for the attack animation
        if (Input.GetKeyDown("space") && lanceCooldown == false)
        {
            animator.SetTrigger("LanceAttack");
            lanceCooldown = true;
            playerMoving.setMoveSpeed(10);
            StartCoroutine(ExecuteAfterTime(2));
            //if (audioSource.isPlaying)
            //{
            //    audioSource.Stop();
            //    AudioManager.Play(AudioClipName.lancePoke);
            //}
            //else
            //{
            //    AudioManager.Play(AudioClipName.lancePoke);
            //}
        }
    }

    /// <summary>
    /// Delay to reset cooldown on lance
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        playerMoving.setMoveSpeed(15);
        lanceCooldown = false;

    }
}
