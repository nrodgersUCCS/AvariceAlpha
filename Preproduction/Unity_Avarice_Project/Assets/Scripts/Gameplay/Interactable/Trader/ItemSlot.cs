using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// used to Handle Drag and Drop
/// </summary>
public class ItemSlot : MonoBehaviour, IDropHandler
{

    GameObject item;
    //Gets mouse postion upon LMB up
    public void OnDrop(PointerEventData eventData)
    {

        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
        else eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition;
    }

    public GameObject GetItem()
    {
        return item;
    }

    public void SetItem(GameObject gameObject)
    {
        item = gameObject;
    }

    public void Destory()
    {
        Destroy(gameObject);
    }

    public Transform GetTransform()
    {
        return gameObject.transform;
    }

}
