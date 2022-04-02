using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    ResourceManager resourceManager;
    // Start is called before the first frame update
    void Start()
    {
        resourceManager = ResourceManager.Instance;
    }

    public void BuyObject(Turret turret)
    {
        if (resourceManager.Money < turret.cost) return;
        GameObject.Instantiate(turret.prefab, position: Vector2.zero, rotation: new Quaternion(0, 0, 0, 0));
        resourceManager.Money -= turret.cost;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
