using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    ResourceManager resourceManager;
    public GameObject go_BuildMenu;
    public List<Turret> turretTypes;
    public GameObject go_BuildButton;
    List<GameObject> go_placedTurrets = new List<GameObject>();
    GameObject go_currentPlacingTurret;
    public GameObject go_Tutorial;
    int placingRock;
    int placingWood;
    int placingIron;
    int placingGold;
    bool is_tutorialOver;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        resourceManager = ResourceManager.Instance;
        turretTypes.ForEach(x => x.Start());
    }

    Animator anim_CurrentButton;
    public void SetAnimator(Animator anim) // Button turns red if you can't afford
    {
        anim_CurrentButton = anim;
    }

    public void BuyObject(Turret turret)
    {
        if (!is_tutorialOver) go_Tutorial.transform.GetChild(1).gameObject.SetActive(true);
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
        go_BuildMenu.SetActive(false);
        placingRock = turret.RockCost;
        placingWood = turret.WoodCost;
        placingIron = turret.IronCost;
        placingGold = turret.GoldCost;
    }
    public void StopPlacing()
    {
        if (!is_tutorialOver)
        {
            go_Tutorial.SetActive(false);
            Mine.Instance.StartSpawning();
        }
        // Update Pricing        
        var turret = go_currentPlacingTurret.GetComponent<Gun>().turret;
        if (turret.RockCost > 0) turret.RockCost = Mathf.Clamp(turret.RockCost + 5, 0, 99);
        if (turret.WoodCost > 0) turret.WoodCost = Mathf.Clamp(turret.WoodCost + 5, 0, 99);
        if (turret.IronCost > 0) turret.IronCost = Mathf.Clamp(turret.IronCost + 5, 0, 99);
        if (turret.GoldCost > 0) turret.GoldCost = Mathf.Clamp(turret.GoldCost + 3, 0, 99);

        go_BuildMenu.transform.Find(turret.name).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "<u><i>" + turret.name + "</i></u>\n" + turret.RockCost + " rock\t " + string.Format("{0:00}", turret.WoodCost) + " wood\n" + string.Format("{0:00}", turret.IronCost) + " iron\t " + string.Format("{0:00}", turret.GoldCost) + " gold\nrange  -\t" + turret.Range + "\t\tshot speed -\t" + turret.ShotSpeed + "\ndamage -\t" + turret.Damage + "\t\tfire speed -\t" + turret.FireSpeed;
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
        ShowBuildMenu();
    }

    public void ShowBuildMenu()
    {
        if (!is_tutorialOver) go_Tutorial.transform.GetChild(0).gameObject.SetActive(false);
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
