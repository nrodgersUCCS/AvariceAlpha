using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTestRoom_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject tempQuestPlayer;
    private GameObject questNPCPrefab;
    private GameObject questUIPrefab;
    private GameObject collectableItemPrefab;

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        ///////////////////////////////////////
        // LOAD EACH PREFAB RESOURCE ONLY ONCE
        ///////////////////////////////////////
        tempQuestPlayer = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Quest/tempQuestPlayer");
        questNPCPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Quest/questNPC");
        questUIPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Quest/Quest UI");
        collectableItemPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Quest/tempCollectableObject");

        ///////////////////////////////////////////
        // ADD IN OBJECTS
        // PERFORM ANY NECEESARY ACTIONS 
        // (ROTATIONS, SCALING, ADD SCRIPTS, ETC.)
        ///////////////////////////////////////////

        // Player
        GameObject player = Instantiate<GameObject>(tempQuestPlayer);

        // Quest NPC
        GameObject questNPC = Instantiate<GameObject>(questNPCPrefab);

        // Quest UI
        GameObject questUI = Instantiate<GameObject>(questUIPrefab);
        {
            // Set parent to quest NPC
            questUI.transform.SetParent(questNPC.transform, false);

            // Set continue button behavior
            Button continueButton = questUI.transform.Find("QuestCompleteWindow").Find("ContinueButton").GetComponent<Button>();
            continueButton.onClick.AddListener(delegate { player.GetComponent<QuestManager>().CloseCompleteWindow(); });
        }

        // Collectable object
        Instantiate<GameObject>(collectableItemPrefab);
    }
}
