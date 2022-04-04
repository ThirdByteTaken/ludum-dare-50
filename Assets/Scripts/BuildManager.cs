using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    ResourceManager resourceManager;
    public GameObject go_BuildMenu;
    public GameObject go_BuildButton;
    List<GameObject> go_placedTurrets = new List<GameObject>();
    GameObject go_currentPlacingTurret;
    int placingRock;
    int placingWood;
    int placingIron;
    int placingGold;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        resourceManager = ResourceManager.Instance;
    }

    Animator anim_CurrentButton;
    public void SetAnimator(Animator anim) // Button turns red if you can't afford
    {
        anim_CurrentButton = anim;
    }

    public void BuyObject(Turret turret)
    {
        if (resourceManager.Rock < turret.RockCost || resourceManager.Wood < turret.WoodCost || resourceManager.Iron < turret.IronCost || resourceManager.Gold < turret.GoldCost)
        {
            anim_CurrentButton.SetTrigger("CantBuild");
            return;
        }
        go_currentPlacingTurret = GameObject.Instantiate(turret.prefab, position: Vector2.zero, rotation: new Quaternion(0, 0, 0, 0));

        go_placedTurrets.ForEach(x => x.transform.Find("Range").GetComponent<SpriteRenderer>().enabled = true);
        go_currentPlacingTurret.transform.Find("Range").GetComponent<SpriteRenderer>().enabled = true;
        go_currentPlacingTurret.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        go_currentPlacingTurret.GetComponent<BoxCollider2D>().isTrigger = true;
        go_currentPlacingTurret.GetComponent<Gun>().enabled = false;
        placingRock = turret.RockCost;
        placingWood = turret.WoodCost;
        placingIron = turret.IronCost;
        placingGold = turret.GoldCost;
    }
    public void StopPlacing()
    {
        // Update Pricing        
        var turret = go_currentPlacingTurret.GetComponent<Gun>().turret;
        if (turret.RockCost > 0) turret.RockCost += 5;
        if (turret.WoodCost > 0) turret.WoodCost += 5;
        if (turret.IronCost > 0) turret.IronCost += 5;
        if (turret.GoldCost > 0) turret.GoldCost += 5;

        turret.text.text = "<u><i>" + turret.name + "</i></u> " + turret.RockCost + " rock\t " + turret.WoodCost + " wood " + turret.IronCost + " iron\t " + turret.GoldCost + " gold range  -\t" + turret.Range + "\t\tshot speed -\t" + turret.ShotSpeed + " damage -\t" + turret.Damage + "\t\tfire speed -\t" + turret.FireSpeed;
        go_placedTurrets.Add(go_currentPlacingTurret);
        go_placedTurrets.ForEach(x => x.transform.Find("Range").GetComponent<SpriteRenderer>().enabled = false);
        go_currentPlacingTurret.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        go_currentPlacingTurret.GetComponent<BoxCollider2D>().isTrigger = false;
        go_currentPlacingTurret.GetComponent<Gun>().enabled = true;

        //resourceManager.ChangeCurrency(-placingCost);
        if (placingRock != 0) ResourceManager.Instance.ChangeResource(-placingRock, 0, ref ResourceManager.Instance.Rock);
        if (placingWood != 0) ResourceManager.Instance.ChangeResource(-placingWood, 1, ref ResourceManager.Instance.Wood);
        if (placingIron != 0) ResourceManager.Instance.ChangeResource(-placingIron, 2, ref ResourceManager.Instance.Iron);
        if (placingGold != 0) ResourceManager.Instance.ChangeResource(-placingGold, 3, ref ResourceManager.Instance.Gold);
        AsteroidManager.Instace.StartSpawning();
        HideBuildMenu();
    }
    public void CancelPlacing()
    {
        go_placedTurrets.ForEach(x => x.transform.Find("Range").GetComponent<SpriteRenderer>().enabled = false);
        HideBuildMenu();
    }

    public void ShowBuildMenu()
    {
        go_BuildButton.SetActive(false);
        go_BuildMenu.SetActive(true);
    }
    public void HideBuildMenu()
    {
        go_BuildButton.SetActive(true);
        go_BuildMenu.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneLoader.TogglePause();
        }
    }
}
