using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TraderDialogButtonEvents : MonoBehaviour
{

    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();

        if (name == "Buy")
        {
            button.onClick.AddListener(delegate { GameObject.Find("Trader(Clone)").GetComponent<Shop>().SetText(false); });
        }
        else if (name == "Sell")
        {
            button.onClick.AddListener(delegate { GameObject.Find("Trader(Clone)").GetComponent<Shop>().SetText(true); });
        }
    }

}
