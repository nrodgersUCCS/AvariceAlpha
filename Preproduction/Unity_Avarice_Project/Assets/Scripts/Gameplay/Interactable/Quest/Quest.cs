using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    // Checks if quest is active
    public bool isActive;

    // Quest info
    public string title;
    public string description;
    public int expReward;
    public int goldReward;
    public GameObject objectReward;
    public QuestGoal questGoal;

    // Complete's quest
    public void Complete()
    {
        isActive = false;
    }

    // Starts the passed along quest
    public void StartQuest(QuestName questName)
    {
        if(questName == QuestName.testQuest)
        {
            title = "Lost Object!";
            description = "Find the lost blue square";
            expReward = 10;
            goldReward = 300;
            questGoal.goalType = GoalType.collect;
            questGoal.requiredAmount = 1;
            objectReward = null;
        }
    }
}
