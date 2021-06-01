using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Resets the scene with a given button
/// </summary>
public class SceneReset : MonoBehaviour
{
    public KeyCode ResetKey;       // Key used to reset the scene

    // Update is called once per frame
    void Update()
    {
        // Reset the scene on key press
        if (Input.GetKeyDown(ResetKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
