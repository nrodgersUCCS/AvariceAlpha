using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droppedMoney : MonoBehaviour
{
    int money;
    int min = 5;
    int max = 15;
    /// <summary>
    /// returns random amount of many based on a min and max value
    /// </summary>
    /// <returns></returns>
    public int GetMoney()
    {
        money = Random.Range(min, max);

        return money;
    }

    /// <summary>
    /// Mutator for min value
    /// </summary>
    /// <param name="newMin"></param>
    public void SetMin(int newMin)
    {
        min = newMin;
        return;
    }
    /// <summary>
    /// mutator for max value
    /// </summary>
    /// <param name="newMax"></param>
    public void SetMax(int newMax)
    {
        max = newMax;
        return;
    }
}
