using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TraderButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    int slot = 0;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
            
            if(name == "ClearCart")
            {
                button.onClick.AddListener(delegate { GameObject.Find("Trader(Clone)").GetComponent<Shop>().ClearItem(); });
                return;
            }
            else if (transform.parent.name.Length < 6)
            {
                slot = int.Parse(transform.parent.name.Substring(transform.parent.name.Length - 1, 1));
            }
            else slot = int.Parse(transform.parent.name.Substring(transform.parent.name.Length - 2, 2));


            if (GameObject.Find("Trader(Clone)") != null)
            {
                button.onClick.AddListener(delegate { GameObject.Find("Trader(Clone)").GetComponent<Shop>().MoveItemToCart(slot - 1); });
            }

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (name != "ClearCart")
        {
            if (name != "ShopText")
            {
                GameObject.Find("Trader(Clone)").GetComponent<Shop>().DisplayItemDetails(slot - 1);
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (name != "ClearCart")
        {
            if (name != "ShopText")
            {
                GameObject.Find("Trader(Clone)").GetComponent<Shop>().RemoveItemDetails(slot - 1);
            }
        }
    }
}
