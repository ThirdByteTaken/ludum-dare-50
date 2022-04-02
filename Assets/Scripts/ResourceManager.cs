using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    const int Money_Gain_Amount = 5;
    const int Money_Gain_Time = 5; // In Seconds

    public static ResourceManager Instance;
    public int Money;

    [SerializeField]
    TMP_Text txt_Money, txt_ResourceGain;

    [SerializeField]
    Animator anim_ResourceGain;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Money = 0;
        Invoke("GainCurrency", Money_Gain_Time);
    }

    void GainCurrency()
    {
        ChangeCurrency(Money_Gain_Amount);
        Invoke("GainCurrency", Money_Gain_Time);
    }
    public void ChangeCurrency(int deltaCurrency)
    {
        Money += deltaCurrency;
        txt_Money.text = "Money-" + Money;
        if (deltaCurrency < 0)
        {
            txt_ResourceGain.text = "-" + deltaCurrency.ToString();
            anim_ResourceGain.SetTrigger("Lose");
        }
        else
        {
            txt_ResourceGain.text = "+" + deltaCurrency.ToString();
            anim_ResourceGain.SetTrigger("Gain");
        }
    }
}
