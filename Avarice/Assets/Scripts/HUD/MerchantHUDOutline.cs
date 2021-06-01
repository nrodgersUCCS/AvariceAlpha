using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This class handles making the merchantHUD outlines handle having a HUDIcon being dropped onto it
/// </summary>
public class MerchantHUDOutline : MonoBehaviour, IDropHandler
{
    private HUDUpdater theHUD;

    /// <summary>
    /// To initialize vars
    /// </summary>
    private void Awake()
    {
        theHUD = transform.root.GetComponent<HUDUpdater>();
    }

    /// <summary>
    /// This method detects when a weapon icon gets dropped onto the outline and snaps it into position
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.SetParent(this.transform.parent, false);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            theHUD.MerchantHUDOutlinesFilled++;
            WeaponUpForTrade aWeapon = new WeaponUpForTrade(
                eventData.pointerDrag.GetComponent<DraggableHUDIcon>().IndexInStack, 
                eventData.pointerDrag.GetComponent<DraggableHUDIcon>().ItemName);
            theHUD.WeaponsUpForTrade.Add(aWeapon);
        }
    }
}
