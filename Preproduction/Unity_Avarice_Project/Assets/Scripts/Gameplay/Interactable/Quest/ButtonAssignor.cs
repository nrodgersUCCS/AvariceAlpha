using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Assigns onClick event for specific buttons
/// </summary>
public class ButtonAssignor : MonoBehaviour
{

    Button button;
    bool wasButtonPressed;

    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        // Assigns the AcceptButton's onClick function
        if (gameObject.name == "AcceptButton" && !wasButtonPressed)
        {
            button.onClick.AddListener(delegate { Accept(); });
            wasButtonPressed = true;
        }

        // Assigns the DeclineButton's onClick function
        if (gameObject.name == "DeclineButton" && !wasButtonPressed)
        {
            button.onClick.AddListener(delegate { Decline(); });
            wasButtonPressed = true;
        }

        // Assigns the ContinueButton's onClick function
        if (gameObject.name == "ContinueButton" && !wasButtonPressed)
        {
            button.onClick.AddListener(delegate { Continue(); });
            wasButtonPressed = true;
        }
    }

    // Accepts  the given quest
    void Accept()
    {
        FindObjectOfType<QuestGiver>().AcceptQuest();
    }

    // Decliens the given quest
    void Decline()
    {
        FindObjectOfType<QuestGiver>().DeclineQuest();
    }

    // Closes reward popup
    void Continue()
    {
        FindObjectOfType<QuestManager>().CloseCompleteWindow();
    }
}
