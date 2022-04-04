using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[CreateAssetMenu]
public class Turret : ScriptableObject
{
    public int RockCost;
    public int WoodCost;
    public int IronCost;
    public int GoldCost;
    public string Range;
    public string ShotSpeed;
    public string Damage;
    public string FireSpeed;
    public GameObject prefab;
    public TMP_Text text;
}
