using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class to handle pausing the game
/// </summary>
public class PauseManager : MonoBehaviour
{
    private static GameObject instance;                          // Singleton object
    private AudioSource audioSource;                             // Pause manager's audio source
    private GameObject pauseMenuPrefab;                          // pause menue prefab
    private GameObject myPauseMenu;                              // Pause menu game object
    private bool tutorialPageExitTimerRunning;                   // Prevents accidentaly closing tutorial page as soon as it opens
    private GameObject tutorialPagePrefab;                       // Tutorial page prefab
    private GameObject myTutorialPage;                           // Tutorial page game object
   
    public bool IsPaused { get; protected set; }                 // Determines if game is paused or not
    public bool TutorialPageVisable { get; protected set; }      // Determines if a tutorial page is visable or not

    /// <summary>
    /// Creates the singleton pause manager
    /// </summary>
    public static PauseManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("PauseManager");
                instance.AddComponent<PauseManager>();
                DontDestroyOnLoad(instance);
            }
            return instance.GetComponent<PauseManager>();
        }
    }


    /// <summary>
    /// Pauses the game and brings up the pause menu
    /// </summary>
    public void Pause(bool openMenu = true)
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        IsPaused = true;
        MusicManager.Instance.ChangeVolume(0.5f);
    }

    /// <summary>
    /// Unpauses the game
    /// </summary>
    public void Unpause()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        IsPaused = false;
        if (Instance.myPauseMenu != null)
            Destroy(Instance.myPauseMenu);
        if (Instance.myTutorialPage != null)
            Destroy(Instance.myTutorialPage);
        MusicManager.Instance.ChangeVolume(1f);
    }

    #region Create Tutorial Page
    /// <summary>
    /// Creates a tutorial window for a throwable item
    /// </summary>
    /// <param name="item"></param>
    public void CreateTutorialWindow(ThrowableItem item)
    {
        if(myTutorialPage == null)
        {
            Pause();                                                                                // Pauses the game
            StartCoroutine(TutorialPageExitTimer(0.1f));                                            // Starts the tutorial page exit timer
            TutorialPageVisable = true;
            Instance.myTutorialPage = Instantiate<GameObject>(tutorialPagePrefab);                  // Instantiates the tutorial page
            AudioManager.Play(AudioClipName.PAUSEMENU_POPUP, Instance.GetComponent<AudioSource>()); // Plays the menu pop-up sound
            TextMeshProUGUI pageText = Instance.myTutorialPage.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>(); // Gets the page's text component

            // Sets dagger text
            if (item.Name == ItemName.DAGGER)
                pageText.text = "My Successor," + "\n" + "Though weak individually, throwing daggers in quick succession " + 
                    "can be powerful." + "\n" + "- The Adventurer";

            // Sets spear text
            if (item.Name == ItemName.SPEAR)
                pageText.text = "My Successor," + "\n" + 
                    "Spears can pierce through numerous enemies, but you must line up your shot before throwing. " + 
                    "Time your attacks carefully." + "\n" + "- The Adventurer";

            // Sets warhammer text
            if (item.Name == ItemName.WARHAMMER)
                pageText.text = "My Successor," + "\n" + "It takes a while to build up enough momentum to throw warhammers, " + 
                    "but they make good melee weapons while you do." + "\n" + "- The Adventurer";

            // Sets environmental weapon text
            if (item.Name == ItemName.BARREL || item.Name == ItemName.BIGCRATE || item.Name == ItemName.CANDELHOLDER
                 || item.Name == ItemName.CHAIR || item.Name == ItemName.FORK || item.Name == ItemName.IRONMAIDEN
                 || item.Name == ItemName.KNIFE || item.Name == ItemName.PLATE || item.Name == ItemName.SMALLCRATE
                 || item.Name == ItemName.TABLE)
                pageText.text = "My Successor," + "\n" + "Weapons might not always be within reach. " + 
                    "When they aren't, the junk littering this castle is surprisingly deadly when thrown." + "\n" + 
                    "- The Adventurer";
        }
    }

    /// <summary>
    /// Creates a tutorial page for armor
    /// </summary>
    /// <param name="armor"></param>
    public void CreateTutorialWindow(Armor armor)
    {
        if (myTutorialPage == null)
        {
            Pause();                                                                                // Pauses the game
            StartCoroutine(TutorialPageExitTimer(0.1f));                                            // Starts the tutorial page exit timer
            TutorialPageVisable = true;
            Instance.myTutorialPage = Instantiate<GameObject>(tutorialPagePrefab);                  // Instantiates the tutorial page
            AudioManager.Play(AudioClipName.PAUSEMENU_POPUP, Instance.GetComponent<AudioSource>()); // Plays the menu pop-up sound
            TextMeshProUGUI pageText = Instance.myTutorialPage.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>(); // Gets the page's text component

            // Sets armor text
            pageText.GetComponent<TextMeshProUGUI>().text = "My Successor," + "\n" + "It's absolutely vital you " + 
                "wear armor at all times. The demons in this castle are fierce and you won't last long without it." + "\n" +
                "- The Adventurer";
        }
    }

    /// <summary>
    /// Creates a custom tutorial page
    /// </summary>
    /// <param name="title"></param>
    /// <param name="description"></param>
    public void CreateTutorialWindow(string text)
    {
        if (myTutorialPage == null)
        {
            Pause();                                                                                // Pauses the game
            StartCoroutine(TutorialPageExitTimer(0.1f));                                            // Starts the tutorial page exit timer
            TutorialPageVisable = true;
            Instance.myTutorialPage = Instantiate<GameObject>(tutorialPagePrefab);                  // Instantiates the tutorial page
            AudioManager.Play(AudioClipName.PAUSEMENU_POPUP, Instance.GetComponent<AudioSource>()); // Plays the menu pop-up sound
            TextMeshProUGUI pageText = Instance.myTutorialPage.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>(); // Gets the page's text component

            // Sets custom text
            pageText.text = text;

        }
    }

    /// <summary>
    /// Creates the instructions page on the main menu
    /// </summary>
    public void CreateInstructionsPage()
    {
        if (Instance.myTutorialPage == null)
        {
            // Creates the tutorial window
            Instance.CreateTutorialWindow("Click Left Mouse to pick up room items & throw weapons" + "\n" + "\n" +
                "Hold Left Mouse down to spin warhammer" + "\n" + "\n" + "Use the Mouse to aim weapons" +
                "\n" + "\n" + "Use WASD to move around");

            // Sets the backround image
            Image bgImage = Instance.myTutorialPage.AddComponent<Image>();
            bgImage.sprite = Resources.Load<Sprite>("Sprites/UI/sp_MainMenuBackground");

            // Disables the main menu
            Canvas menu = GameObject.Find("MainMenu").GetComponent<Canvas>();
            menu.enabled = false;
        }
    }
    #endregion

    /// <summary>
    /// Creates a pause menu
    /// </summary>
    private void CreatePauseMenu()
    {
        if (Instance.myPauseMenu == null)
        {
            Instance.myPauseMenu = Instantiate<GameObject>(pauseMenuPrefab);
            Instance.myPauseMenu.name = "PauseMenu";
            AudioManager.Play(AudioClipName.PAUSEMENU_POPUP, Instance.GetComponent<AudioSource>());
        }
    }

    /// <summary>
    /// Awake is called before start
    /// </summary>
    private void Awake()
    {
        GetComponent<PauseManager>().enabled = true;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.ignoreListenerPause = true;

        // Loads the pause menu prefab
        pauseMenuPrefab = Resources.Load<GameObject>("Prefabs/HUD/PauseMenu");

        // Loads the tutorial page prefab
        tutorialPagePrefab = Resources.Load<GameObject>("Prefabs/HUD/TutorialPage");
    }

    /// <summary>
    /// Update is called every frame
    /// </summary>
    private void Update()
    {
        // Checks if escape was pressed & if player exists
        if (Input.GetKeyDown(KeyCode.Escape) && FindObjectOfType<PlayerMovement>() != null && !TutorialPageVisable)
        {
            // Unpauses the game
            if (Instance.IsPaused)
            {
                Instance.Unpause();
            }

            // Pauses the game & opens the pause menu
            else
            {
                Instance.Pause();
                Instance.CreatePauseMenu();
            }
        }

        // Closes the open page if left mouse is pressed
        if (Input.GetMouseButtonDown(0) && TutorialPageVisable && !tutorialPageExitTimerRunning)
        {
            // Closes the tutorial page in gameplay
            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                Instance.Unpause();
                TutorialPageVisable = false;
                SaveManager.Instance.SaveData(false, false);
            }
            // Closes the instructions tutorial page
            else
            {
                // Re-enables the main menu
                Canvas menu = GameObject.Find("MainMenu").GetComponent<Canvas>();
                menu.enabled = true;
                Instance.Unpause();
            }
        }

        // Displays the first tutorial page if it hasn't yet been seen
        if(FindObjectOfType<PlayerMovement>() != null && !SaveManager.Instance.FirstMessageSeen)
        {
            Instance.CreateTutorialWindow("My Successor" + "\n" + "This castle is a twisted, corrupted husk of what it once was. The Demon King " +
                "has claimed it and the surrounding land for himself, filling them both with demons that hunt down and slaughter the innocent. But, " +
                "there's a way to fix this mess. If I slay the Demon King, I can send him and his minions back to Hell!" + "\n" + "- The Adventurer");
            SaveManager.Instance.FirstMessageSeen = true;
        }
    }

    /// <summary>
    /// A timer used to prevent closing the tutorial page as soon as it opens
    /// </summary>
    /// <param name="timeToWait">How long to wait</param>
    /// <returns></returns>
    IEnumerator TutorialPageExitTimer(float timeToWait)
    {
        tutorialPageExitTimerRunning = true;
        yield return new WaitForSecondsRealtime(timeToWait);
        tutorialPageExitTimerRunning = false;
    }
}
