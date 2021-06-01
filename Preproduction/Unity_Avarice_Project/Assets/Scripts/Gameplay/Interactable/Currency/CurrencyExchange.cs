using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyExchange : MonoBehaviour
{
    PlayerMoney playerMoney;
    GameObject trader;
    // Start is called before the first frame update
    void Start()
    {
        playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Checks if a is greater than b
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    bool CheckPrice(int a, int b)
    {
        if (a >= b)
        {
            return true;
        }
        else

            return false;
    }
    /// <summary>
    /// used to pass money to and from the trader
    /// </summary>
    /// <param name="price"></param>
    /// <param name="TransactionType"></param>
    /// <returns></returns>
    public int Transaction(int price, int TransactionType)
    {
        //if value is greater than 0 Buy code is run else Sell code is run
        if (TransactionType > 0)
        {
            playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>();

            if (CheckPrice(playerMoney.money, price))
            {
                playerMoney.SubtractMoney(price);
                return 1;
            }
            else
            {
                // show error message

                return -1;
            }
        }
        else
        {
            playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>();
            playerMoney.AddMoney(price);
            return 1;
        }
    }
}
