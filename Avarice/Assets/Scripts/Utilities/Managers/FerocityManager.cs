using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// The ferocity manager    
/// </summary>
public class FerocityManager : MonoBehaviour
{
    private static GameObject instance;                     // The singelton object

    private string ferocityString;                          // UI text to show current ferocity

    // Constants
    private float OFFSETVALUE;                              // How many enemies need to be killed to offset weight of 1 item
    private float MAXFEROCITY;                              // Max amount of ferocity

    public float FerocityMovement { get; set; }             // How much player movement is increased
    public float CurrentKills { get; set; }                 // Current number of enemies killed
    public float CurrentStack { get; set; }                 // Current amount of ferocity

    // Start is called before the first frame update
    void Start()
    {
        // Gets constant values
        PlayerValues container = (PlayerValues)Constants.Values(ContainerType.PLAYER);
        OFFSETVALUE = container.OffsetValue;
        MAXFEROCITY = container.MaxFerocity;
    }

    /// <summary>
    /// Creates the singleton object
    /// </summary>
    public static FerocityManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("FerocityManager");
                instance.AddComponent<FerocityManager>();
                DontDestroyOnLoad(instance);
            }
            return instance.GetComponent<FerocityManager>();
        }
    }

    /// <summary>
    /// Increases the player's ferocity on enemy death
    /// </summary>
    public void IncreaseFerocity()
    {
        // Increases ferocity if not at max
        if (CurrentStack < MAXFEROCITY)
        {
            CurrentKills++;
            CurrentStack = (CurrentKills / OFFSETVALUE) / 10;
        }

        // Sets ferocity to max if over
        else if (CurrentStack > MAXFEROCITY)
            CurrentStack = MAXFEROCITY;

        // Set ferocity movement
        FerocityMovement = CurrentStack;

        // Set the Text UI
        SetUIText();
    }

    /// <summary>
    /// Resets the player's ferocity on death
    /// </summary>
    public void ResetFerocity()
    {
        CurrentKills = 0;
        CurrentStack = 0;
        FerocityMovement = 0;
    }

    /// <summary>
    /// Method for setting the UI text for the ferocity
    /// </summary>
    public void SetUIText()
    {
        // Set ferocity's UI text
        ferocityString = (Mathf.Round(CurrentStack * 100) / 100).ToString();
        TextMeshProUGUI ferocityUI = GameObject.Find("FerocityUI").transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        ferocityUI.text = ferocityString;
    }
}