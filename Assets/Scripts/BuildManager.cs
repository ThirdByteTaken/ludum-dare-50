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
        if (resourceManager.Rock < turret.Rock || resourceManager.Wood < turret.Wood || resourceManager.Iron < turret.Iron || resourceManager.Gold < turret.Gold)
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
        placingRock = turret.Rock;
        placingWood = turret.Wood;
        placingIron = turret.Iron;
        placingGold = turret.Gold;
    }
    public void StopPlacing()
    {
        go_placedTurrets.Add(go_currentPlacingTurret);
        go_placedTurrets.ForEach(x => x.transform.Find("Range").GetComponent<SpriteRenderer>().enabled = false);
        go_currentPlacingTurret.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        go_currentPlacingTurret.GetComponent<BoxCollider2D>().isTrigger = false;
        go_currentPlacingTurret.GetComponent<Gun>().enabled = true;

        //resourceManager.ChangeCurrency(-placingCost);
        ResourceManager.Instance.ChangeResource(-placingRock, 0, ref ResourceManager.Instance.Rock);
        ResourceManager.Instance.ChangeResource(-placingWood, 1, ref ResourceManager.Instance.Wood);
        ResourceManager.Instance.ChangeResource(-placingIron, 2, ref ResourceManager.Instance.Iron);
        ResourceManager.Instance.ChangeResource(-placingGold, 3, ref ResourceManager.Instance.Gold);
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

        }
    }
}
