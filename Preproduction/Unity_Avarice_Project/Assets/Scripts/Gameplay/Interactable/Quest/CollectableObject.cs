using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    QuestManager questManager;

    private void Start()
    {
        // Gets QuestManager component from player object
        questManager = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            // Checks if a collection quest is active
            if (questManager.quest.isActive && questManager.quest.questGoal.goalType == GoalType.collect)
            {
                questManager.CollectItem();
                Destroy(this.gameObject);
            }
        }
    }
}
