using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class QuestManager : MonoBehaviour
{
    public Quest quest;
    GameObject questCompleteWindow;
    Text titleText;
    Text expText;
    Text goldText;
    Text objectRewardText;

    AudioSource audioSource;

    private void Start()
    {
        // Saved for efficency
        quest = GameObject.FindGameObjectWithTag("QuestColl").GetComponent<Quest>();
        questCompleteWindow = GameObject.FindGameObjectWithTag("QuestCompleteWindow");
        titleText = questCompleteWindow.transform.GetChild(1).gameObject.GetComponent<Text>();
        expText = questCompleteWindow.transform.GetChild(3).gameObject.GetComponent<Text>();
        goldText = questCompleteWindow.transform.GetChild(4).gameObject.GetComponent<Text>();
        objectRewardText = questCompleteWindow.transform.GetChild(5).gameObject.GetComponent<Text>();
        questCompleteWindow.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Collect item
    /// </summary>
    public void CollectItem()
    {
        if(quest.isActive)
        {
            quest.questGoal.ItemCollected();
            if(quest.questGoal.isReached())
            {
                // increase exp
                // increase gold
                // give quest reward object

                AudioManager.Play(AudioClipName.vsQuestCompleted, audioSource);

                // Opens quest complete window
                questCompleteWindow.SetActive(true);
                titleText.text = quest.title;
                if(quest.expReward != 0)
                    expText.text = "Experience: " + quest.expReward.ToString();
                if(quest.goldReward != 0)
                    goldText.text = "Gold: " + quest.goldReward.ToString();
                if(quest.objectReward != null)
                    objectRewardText.text = quest.objectReward.ToString();

                quest.Complete();
            }
        }
    }

    /// <summary>
    /// Closes the quest complete window
    /// </summary>
    public void CloseCompleteWindow()
    {
        questCompleteWindow.SetActive(false);
    }
}
