using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Holds item details
/// </summary>
public class DefaultItem : MonoBehaviour
{
    string itemName = "Item";
    int price = 10;
    List<int> stats;


    public string GetName()
    {
        return itemName;
    }

    public void SetName(string newName)
    {
        name = newName;
    }
    public int GetPrice()
    {
        return price;
    }

    public void SetPrice(int newPrice)
    {
        price = newPrice;
    }
    public List<int> GetStats()
    {
        return stats;
    }

    public void SetStats(List<int> newStats)
    {
        stats = newStats;
    }

    public GameObject GetItemType()
    {
        return gameObject;
    }
}
