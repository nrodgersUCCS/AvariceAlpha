using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays Dialog When talking to the trader
/// </summary>
public class Dialog : MonoBehaviour
{
    GameObject traderUI;
    GameObject traderDialog;
    GameObject playerInventory;
    GameObject traderInventory;
    Canvas canvas;
    AudioSource audioSource;
    bool isActive = false;

    GameObject interactIndicator;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        if (GameObject.FindGameObjectsWithTag("TraderUI") != null)
        {
            GetGameObjects();
            traderDialog.SetActive(true);
        }

        playerInventory = GameObject.Find("Player(Clone)").transform.Find("PlayerInventory").gameObject;

        canvas = FindObjectOfType<Canvas>();

    }

    // Update is called once per frame
    void Update()
    {
       if(isActive && Input.GetKeyDown(KeyCode.E) && !GameObject.Find("TraderInventory").GetComponent<Canvas>().isActiveAndEnabled)
        {
            AudioManager.Play(AudioClipName.vsTradeInteraction,audioSource);

            //Turns on the Player & Trader Dialog
            traderUI.GetComponent<Canvas>().enabled = true;
            traderDialog.GetComponent<Canvas>().enabled = true;
            playerInventory.GetComponent<Canvas>().enabled = true;
        }

    }

    void GetGameObjects()
    {
        traderUI = GameObject.FindGameObjectWithTag("TraderUI");
        traderInventory = traderUI.transform.Find("TraderInventory").gameObject;
        traderDialog = traderUI.transform.Find("Trader Dialog").gameObject;
  //      playerInventory = GameObject.Find("PlayerInventory").gameObject;
        traderInventory.GetComponent<Canvas>().enabled = false;
        traderDialog.SetActive(true);
        if(gameObject.name == "Trader(Clone)")
        {
            interactIndicator = gameObject.transform.Find("tempQuestIndicator").gameObject;
        }

    }


    public void PlayCloseSound()
    {
        AudioManager.Play(AudioClipName.vsTradeLeaving,audioSource);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!GameObject.Find("TraderInventory").GetComponent<Canvas>().isActiveAndEnabled)
        {
            interactIndicator.SetActive(true);

            if (other.tag == "Player")
            {
                isActive = true;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactIndicator.SetActive(false);
        isActive = false;
    }
}
