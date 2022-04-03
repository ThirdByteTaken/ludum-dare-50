using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    ResourceManager resourceManager;
    List<GameObject> placedTurrets = new List<GameObject>();
    GameObject go_currentPlacingTurret;
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
        placedTurrets.Add(go_currentPlacingTurret);
        placedTurrets.ForEach(x => x.transform.Find("Range").GetComponent<SpriteRenderer>().enabled = true);
        go_currentPlacingTurret.GetComponent<BoxCollider2D>().enabled = false;
        go_currentPlacingTurret.GetComponent<Gun>().enabled = false;
        resourceManager.ChangeCurrency(-turret.cost);
    }
    public void StopPlacing()
    {
        placedTurrets.ForEach(x => x.transform.Find("Range").GetComponent<SpriteRenderer>().enabled = false);
        go_currentPlacingTurret.GetComponent<BoxCollider2D>().enabled = true;
        go_currentPlacingTurret.GetComponent<Gun>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
