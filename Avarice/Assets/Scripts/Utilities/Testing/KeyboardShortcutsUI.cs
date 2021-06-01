using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// A class for showing testing keyboard shortcuts on the UI
/// </summary>
public class KeyboardShortcutsUI : MonoBehaviour
{
    [SerializeField]
    private bool active = true;        // Whether the UI is active or not

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        ToggleActive();

        // Get text template
        Text template = transform.GetChild(0).GetComponent<Text>();

        // Find reset key
        {
            SceneReset reset = FindObjectOfType<SceneReset>();

            // Create Text element in canvas
            Text newText = Instantiate(template, transform);
            Vector3 newPos = template.transform.position;
            newPos.y -= template.rectTransform.rect.height;
            newText.rectTransform.position = newPos;

            // Write reset key
            newText.text = "Reset Room - " + reset.ResetKey;
        }

        // Find all teleport key commands
        {
            TeleportPoint[] teleportPoints = FindObjectsOfType<TeleportPoint>().OrderBy(point => point.Key).ToArray();
            for (int i = 0; i < teleportPoints.Length; i++)
            {
                // Create Text elements in the canvas
                Text newText = Instantiate(template, transform);
                Vector3 newPos = template.transform.position;
                newPos.y -= template.rectTransform.rect.height * (i + 2);
                newText.rectTransform.position = newPos;

                // Write shortcut descriptions
                newText.text = "Teleport to " + teleportPoints[i].LocationDescription + " - " + teleportPoints[i].Key;
            }
        }
    }

    /// <summary>
    /// Called each frame
    /// </summary>
    private void Update()
    {
        // Toggle UI off and on with ~ key
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            active = !active;
            ToggleActive();
        }
    }

    /// <summary>
    /// Toggles the UI on and off
    /// </summary>
    private void ToggleActive()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(active);
        }
    }
}
