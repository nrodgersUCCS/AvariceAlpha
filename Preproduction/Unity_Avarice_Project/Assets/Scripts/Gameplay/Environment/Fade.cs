using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    Image image;

    bool fadeingOut;
    bool timerSet;
    float tranisitionTime;
    AudioSource audioSource;

    /// <summary>
    /// Called before start
    /// </summary>
    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Saved for efficency
        image = GetComponent<Image>();
        fadeingOut = false;

        // Determines if scene should be faded into 
        if (SceneManager.GetActiveScene().name != "Hub")
            gameObject.transform.localScale = new Vector3(0, 0, 0);

        // Runs fade in effect
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        // Fades out if player is dead
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            if (!fadeingOut)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                FadeOut();
            }

            if (fadeingOut && !timerSet)
            {
                tranisitionTime = 5f;
                timerSet = true;
            }

            if (timerSet)
            {
                tranisitionTime -= Time.deltaTime;
                if (tranisitionTime <= 0)
                {
                    SceneManager.LoadScene("Hub");
                }
            }
        }
    }

    /// <summary>
    /// Makes screen fade from black
    /// </summary>
    public void FadeIn()
    {
        image.CrossFadeAlpha(0, 2, false);
        // fadeingOut = true;
        fadeingOut = false;
    }

    /// <summary>
    /// Makes screen fade to black
    /// </summary>
    public void FadeOut()
    {
        image.CrossFadeAlpha(2, 2, false);
        fadeingOut = true;
        audioSource.loop = false;
        audioSource.Stop();
        AudioManager.Play(AudioClipName.vsSongDeath, audioSource);
        // fadeingOut = false;
    }
}
