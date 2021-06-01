using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoney : MonoBehaviour
{
    private Canvas canvas;
    public Text moneyText;
    public int money;

    // Start is called before the first frame update
    void Start()
    {
        money = 100;
        moneyText.text = money.ToString();
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMoney(int moneyToAdd)
    {
        money += moneyToAdd;
        moneyText.text = money.ToString();
    }

    public void SubtractMoney(int moneyToSubtract)
    {
        if (money - moneyToSubtract < 0)
        {
            Debug.Log("You dont have enough money!");
        }
        else
        {
            money -= moneyToSubtract;
            moneyText.text = money.ToString();
        }
    }
}
