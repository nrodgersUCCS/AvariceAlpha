using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    // Quest goal settings
    public GoalType goalType;
    public int requiredAmount;
    public int currentAmount;

    public bool isReached()
    {
        return (currentAmount >= requiredAmount);
    }

    /// <summary>
    /// Add to current amount of collected items
    /// </summary>
    public void ItemCollected()
    {
        if(goalType == GoalType.collect)
        {
            currentAmount++;
        }
    }
}

/// <summary>
/// Goal types
/// </summary>
public enum GoalType
{
    collect,
    kill
}
