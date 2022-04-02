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
    TMP_Text txt_Money, txt_ResourceGain;

    [SerializeField]
    Animator anim_ResourceGain;

    void Start()
    {
        Money = 0;
        Invoke("GainCurrency", Money_Gain_Time);
    }

    void GainCurrency()
    {
        Money += Money_Gain_Amount;
        txt_Money.text = "Money-" + Money;
        txt_ResourceGain.text = "+" + Money_Gain_Amount;
        anim_ResourceGain.SetTrigger("Start");
        Invoke("GainCurrency", Money_Gain_Time);
    }

}
