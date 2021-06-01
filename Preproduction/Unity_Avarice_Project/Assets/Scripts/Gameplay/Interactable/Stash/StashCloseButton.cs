using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A button to close the stash
/// </summary>
public class StashCloseButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();

        button.onClick.AddListener(delegate {GameObject.Find("InventoryManager(Clone)").GetComponent<InventoryManager>().
CloseStash();
        });
    }
}
