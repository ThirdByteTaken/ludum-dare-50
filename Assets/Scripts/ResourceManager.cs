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
    int gold, iron, wood, rock;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
        }
    }
    public int Iron
    {
        get
        {
            return iron;
        }
        set
        {
            iron = value;
        }
    }
    public int Wood
    {
        get
        {
            return wood;
        }
        set
        {
            wood = value;
        }
    }
    public int Rock
    {
        get
        {
            return rock;
        }
        set
        {
            rock = value;
        }
    }

    public GameObject[] go_Resources;

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
        Money = 10; // TODO fix
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
        txt_ResourceGain.text = (deltaCurrency < 0) ? deltaCurrency.ToString() : "+" + deltaCurrency.ToString();
        anim_ResourceGain.SetTrigger((deltaCurrency < 0) ? "Lose" : "Gain");
    }

    public GameObject RandomResource() // Weighted odds
    {
        GameObject[] list = { go_Resources[0], go_Resources[0], go_Resources[1], go_Resources[1], go_Resources[2], go_Resources[2], go_Resources[3], }; // Last one is gold
        return list[Random.Range(0, list.Length)];

    }
}
