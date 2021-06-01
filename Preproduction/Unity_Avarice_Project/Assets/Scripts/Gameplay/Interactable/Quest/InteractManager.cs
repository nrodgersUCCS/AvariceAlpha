using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    bool questColl;

    GameObject gObject;

    QuestManager questManager;

    GameObject interactIndicator;

    // Start is called before the first frame update
    void Start()
    {
        questManager = gameObject.GetComponent<QuestManager>();
        interactIndicator = gameObject.transform.GetChild(0).gameObject;

        questManager = GetComponent<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Opens the quest window
        if(questColl && Input.GetKeyDown(KeyCode.E) && !questManager.quest.isActive)
        {
            gObject.GetComponent<QuestGiver>().OpenQuestWindow();
        }
    }

    /// <summary>
    /// Disables the interact indicator
    /// </summary>
    public void DisableInteractIndicator()
    {
        interactIndicator.SetActive(false);
    }

    /// <summary>
    /// Checks collision object
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "QuestColl")
        {
            questColl = true;
            gObject = collision.gameObject;
            if(!questManager.quest.isActive)
                interactIndicator.SetActive(true);
        }
    }

    /// <summary>
    /// Disables the interact icon
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerExit2D(Collider2D collision)
    {
        questColl = false;
        gObject = null;
        interactIndicator.SetActive(false);
    }
}
