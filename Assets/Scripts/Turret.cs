using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[CreateAssetMenu]
public class Turret : ScriptableObject
{
    public int rockCost, woodCost, ironCost, goldCost;

    [HideInInspector]
    public int RockCost, WoodCost, IronCost, GoldCost;
    public void Start()
    {
        Debug.Log("inspector rock cost " + rockCost);
        RockCost = rockCost;
        Debug.Log("set rock cost " + RockCost);
        WoodCost = woodCost;
        IronCost = ironCost;
        GoldCost = goldCost;
    }
    public string Range;
    public string ShotSpeed;
    public string Damage;
    public string FireSpeed;
    public GameObject prefab;
}
