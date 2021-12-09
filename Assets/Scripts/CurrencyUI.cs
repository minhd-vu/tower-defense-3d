using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public Text currencyText;

    // Update is called once per frame
    void Update()
    {
        currencyText.text = "Currency\n$" + Player.instance.Currency.ToString();
    }
}
