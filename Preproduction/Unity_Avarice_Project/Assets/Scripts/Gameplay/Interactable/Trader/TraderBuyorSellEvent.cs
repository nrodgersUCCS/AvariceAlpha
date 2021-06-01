using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TraderBuyorSellEvent : MonoBehaviour
{

    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }
    public void onClick()
    {
        if (gameObject.GetComponent<Text>().text == "Buy")
        {
            GameObject.Find("Trader(Clone)").GetComponent<Shop>().Buy();
        }
        else if (gameObject.GetComponent<Text>().text == "Sell")
        {
            GameObject.Find("Trader(Clone)").GetComponent<Shop>().Sell();
        }
    }
}
