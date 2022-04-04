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
    public int Gold, Iron, Wood, Rock;


    public GameObject[] go_Resources;

    [SerializeField]
    TMP_Text[] txt_Resources, txt_ResourceGains;

    [SerializeField]
    Animator[] anim_ResourceGains;

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
        print("pre " + Rock);
        ChangeResource(Money_Gain_Amount, 0, ref Rock);
        print("post " + Rock);
        Invoke("GainCurrency", Money_Gain_Time);
    }
    public void ChangeResource(int deltaResource, int resourceID, ref int resource)
    {
        resource += deltaResource;
        txt_Resources[resourceID].text = resource.ToString();
        txt_ResourceGains[resourceID].text = (deltaResource < 0) ? deltaResource.ToString() : "+" + deltaResource.ToString();
        anim_ResourceGains[resourceID].SetTrigger((deltaResource < 0) ? "Lose" : "Gain");
    }

    public GameObject RandomResource() // Weighted odds
    {
        GameObject[] list = { go_Resources[0], go_Resources[0], go_Resources[1], go_Resources[1], go_Resources[2], go_Resources[2], go_Resources[3], }; // Last one is gold
        return list[Random.Range(0, list.Length)];

    }
}
enum ResourceTypes
{
    Rock,
    Wood,
    Iron,
    Gold
}