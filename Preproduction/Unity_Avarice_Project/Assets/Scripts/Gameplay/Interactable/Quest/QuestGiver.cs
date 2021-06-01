using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class QuestGiver : MonoBehaviour
{
    Quest quest;
    GameObject questWindow;
    Text titleText;
    Text descText;
    Text expText;
    Text goldText;
    Text objectRewardText;

    QuestManager questManager;

    AudioSource audioSource;

    private void Start()
    {
        // Saved for efficency
        quest = gameObject.GetComponent<Quest>();
        questWindow = GameObject.FindGameObjectWithTag("QuestWindow");
        titleText = questWindow.transform.GetChild(1).gameObject.GetComponent<Text>();
        descText = questWindow.transform.GetChild(2).gameObject.GetComponent<Text>();
        expText = questWindow.transform.GetChild(3).gameObject.GetComponent<Text>();
        goldText = questWindow.transform.GetChild(4).gameObject.GetComponent<Text>();
        objectRewardText = questWindow.transform.GetChild(5).gameObject.GetComponent<Text>();
        questWindow.SetActive(false);
        questManager = FindObjectOfType<QuestManager>();
        audioSource = GetComponent<AudioSource>();

        // Gets current scene & sets the quest to be given
        if(SceneManager.GetActiveScene().name == "QuestTestRoom")
        {
            quest.StartQuest(QuestName.testQuest);
        }
    }

    /// <summary>
    /// Opens the quest window & sets appropriate text boxes 
    /// </summary>
    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descText.text = quest.description;
        if (quest.expReward != 0)
            expText.text = "Experience: " + quest.expReward.ToString();
        if(quest.goldReward != 0)
            goldText.text = "Gold: " + quest.goldReward.ToString();
        if(quest.objectReward != null)
            objectRewardText.text = quest.objectReward.ToString();
    }

    /// <summary>
    /// Sets the active quest
    /// </summary>
    public void AcceptQuest()
    {
        AudioManager.Play(AudioClipName.vsQuestRecieved, audioSource);
        questWindow.SetActive(false);
        quest.isActive = true;
        questManager.quest = quest;
        FindObjectOfType<InteractManager>().DisableInteractIndicator();
    }

    /// <summary>
    /// Declines offered quest
    /// </summary>
    public void DeclineQuest()
    {
        questWindow.SetActive(false);
    }
}
