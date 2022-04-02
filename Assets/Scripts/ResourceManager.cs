using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    const int Money_Gain_Amount = 5;
    const int Money_Gain_Time = 5; // In Seconds

    int Money;

    [SerializeField]
    TMP_Text txt_Money;

    void Start()
    {
        Money = 0;
        Invoke("GainCurrency", Money_Gain_Time);
    }

    void GainCurrency()
    {
        Money += Money_Gain_Amount;
        txt_Money.text = "Money-" + Money;
        Invoke("GainCurrency", Money_Gain_Time);
    }


}
