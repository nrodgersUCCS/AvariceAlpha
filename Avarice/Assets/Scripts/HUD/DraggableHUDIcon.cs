using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This class handles making the MerchantHUD weapon icons draggable
/// </summary>
public class DraggableHUDIcon : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;                      //The PlayerHUD canvas
    private RectTransform rectTransform;        //The RectTransform of the icon
    private CanvasGroup canvasGroup;            //The canvas group on the icon    
    private HUDUpdater hudUpdater;              //The HUD updater
    
    
    //Keeps track of the index in the stack it came from
    public int IndexInStack { get; set; }
    public ItemName ItemName { get; set; }

    //It's starting position on the merchant HUD
    public Vector3 StartPosition { private get; set; }
    
    // It's starting parent
    public Transform StartParent { private get; set; }

    /// <summary>
    /// To initialize the vars before anything else
    /// </summary>
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        hudUpdater = transform.root.GetComponent<HUDUpdater>();
    }

    /// <summary>
    /// This method is called when the player starts dragging an icon
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

        hudUpdater.RemoveItem(IndexInStack);
    }

    /// <summary>
    /// This method is called when the player is dragging an icon. 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    /// <summary>
    /// This method is called when the player stops dragging an icon
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        bool isInList = false;
        List<WeaponUpForTrade> weaponsUp = hudUpdater.WeaponsUpForTrade;
        for (int i = 0; i < weaponsUp.Count; ++i)
        {
            if (weaponsUp[i].indexInStack == IndexInStack)
            {
                isInList = true;
                break;
            }
        }

        if (!isInList)
        {
            eventData.pointerDrag.transform.SetParent(StartParent, false);
            eventData.pointerDrag.transform.localPosition = StartPosition;
            hudUpdater.MerchantHUDOutlinesFilled--;
        }
    }
}
