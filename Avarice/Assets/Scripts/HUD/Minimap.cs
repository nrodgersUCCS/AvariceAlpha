using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    GameObject minimapObject;                   // The minimap gameobject

    private Vector2 mapSize;                    // The current size of the minimap
    private Vector2 baseMapSize;                // The original size of the minimap
    private Vector2 startPos;                   // The minimap's original anchor position

    private Image smallBackground;              // The small minimap's frame
    private Image largeBackground;              // The large minimap's frame
    private RawImage mapImage;

    private GameObject playerMarker;            // The player's map icon
    private GameObject demonkingMarker;         // The demon king's map icon
    private Vector2 basePlayerMarkerSize;       // The base size of the player's map icon
    private Vector2 baseDemonKingMarkerSize;    // The base size of the demon king's map icon
    private float iconScaleMultiplier = 2.3f;   // How much to scale the map markers by

    // Start is called before the first frame update
    void Start()
    {
        // Saved for efficiency
        minimapObject = transform.GetChild(2).gameObject;
        smallBackground = transform.GetChild(0).gameObject.GetComponent<Image>();
        largeBackground = transform.GetChild(1).gameObject.GetComponent<Image>();
        mapImage = minimapObject.GetComponent<RawImage>();
        startPos = GetComponent<RectTransform>().anchoredPosition;
        mapSize = minimapObject.GetComponent<RectTransform>().sizeDelta;

        // Sets the map's base size
        baseMapSize = mapSize;

        // Finds the map markers
        playerMarker = FindObjectOfType<PlayerMovement>().transform.Find("MiniMap Icon").gameObject;
        demonkingMarker = FindObjectOfType<DemonKing>().transform.Find("MiniMap Icon").gameObject;

        // Sets the size of the map markers
        basePlayerMarkerSize = playerMarker.transform.localScale;
        baseDemonKingMarkerSize = demonkingMarker.transform.localScale;
        playerMarker.transform.localScale *= iconScaleMultiplier;
        demonkingMarker.transform.localScale *= iconScaleMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        // Changes the map's size when M is pressed
        if (Input.GetKeyDown(KeyCode.M))
            ResizeMinimap();
    }

    /// <summary>
    /// Changes the size of the minimap
    /// </summary>
    private void ResizeMinimap()
    {
        // The minimap gameobject's new position
        Vector2 newPos;

        // Checks minimap's current size
        if (mapSize != baseMapSize)
        {
            // Disables the large frame & enables the small frame
            largeBackground.enabled = false;
            smallBackground.enabled = true;

            // Sets the map back to its original size & position
            mapSize = baseMapSize;
            newPos = startPos;

            // Increases the size of the map markers when the map is small
            playerMarker.transform.localScale = basePlayerMarkerSize * iconScaleMultiplier;
            demonkingMarker.transform.localScale = baseDemonKingMarkerSize * iconScaleMultiplier;
        }
        else
        {
            // Disables the small frame & enables the large frame
            smallBackground.enabled = false;
            largeBackground.enabled = true;

            // Scales map's size & sets map's new position to center of screen
            mapSize = new Vector2(1150,1150);
            newPos = Vector2.zero;

            // Sets the map markers back to their original size when the map is big
            playerMarker.transform.localScale = basePlayerMarkerSize;
            demonkingMarker.transform.localScale = baseDemonKingMarkerSize;
        }

        // Sets size & position of map
        mapImage.rectTransform.sizeDelta = mapSize;
        gameObject.GetComponent<RectTransform>().anchoredPosition = newPos;
    }
}
