using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used as an alternative to the final -layer to avoid merger conflicts
/// </summary>
public class tempQuestPlayer : MonoBehaviour
{
    Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(mousePos.x, mousePos.y);
    }
}
