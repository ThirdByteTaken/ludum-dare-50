using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    ResourceManager resourceManager;
    List<GameObject> placedTurrets = new List<GameObject>();
    GameObject go_currentPlacingTurret;
    int placingCost;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        resourceManager = ResourceManager.Instance;
    }

    public void BuyObject(Turret turret)
    {
        if (resourceManager.Money < turret.cost) return;
        go_currentPlacingTurret = GameObject.Instantiate(turret.prefab, position: Vector2.zero, rotation: new Quaternion(0, 0, 0, 0));

        placedTurrets.ForEach(x => x.transform.Find("Range").GetComponent<SpriteRenderer>().enabled = true);
        go_currentPlacingTurret.transform.Find("Range").GetComponent<SpriteRenderer>().enabled = true;
        go_currentPlacingTurret.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        go_currentPlacingTurret.GetComponent<BoxCollider2D>().isTrigger = true;
        go_currentPlacingTurret.GetComponent<Gun>().enabled = false;
        placingCost = turret.cost;
    }
    public void StopPlacing(bool is_Canceled)
    {
        placedTurrets.Add(go_currentPlacingTurret);
        placedTurrets.ForEach(x => x.transform.Find("Range").GetComponent<SpriteRenderer>().enabled = false);
        go_currentPlacingTurret.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        go_currentPlacingTurret.GetComponent<BoxCollider2D>().isTrigger = false;
        go_currentPlacingTurret.GetComponent<Gun>().enabled = true;
        if (is_Canceled) return;

        resourceManager.ChangeCurrency(-placingCost);
    }
}
