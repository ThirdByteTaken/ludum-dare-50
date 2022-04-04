using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    private GameObject go_PlaceableObject;
    private Camera cam_Main;
    public bool is_Placing = true;
    public bool is_Placeable = true;
    int colliders;
    AudioSource PlaceSound;

    // Start is called before the first frame update
    void Start()
    {
        go_PlaceableObject = this.gameObject;
        cam_Main = Camera.main;
        PlaceSound = GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (is_Placing)
        {
            Vector2 mousePosition = DevTools.RoundVector3(cam_Main.ScreenToWorldPoint(Input.mousePosition), 32);
            go_PlaceableObject.transform.localPosition = mousePosition;
            if (Input.GetMouseButtonDown(0) && is_Placeable)
            {
                is_Placing = false;
                PlaceSound.Play();
                BuildManager.Instance.StopPlacing();

            }
            if (Input.GetMouseButtonDown(1))
            {
                BuildManager.Instance.CancelPlacing();
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if ((collider.CompareTag("Turret") || collider.CompareTag("Player")) && is_Placing)
        {
            colliders++;
            go_PlaceableObject.GetComponent<SpriteRenderer>().color = Color.red;
            go_PlaceableObject.transform.Find("Head").GetComponent<SpriteRenderer>().color = Color.red;
            is_Placeable = false;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if ((collider.CompareTag("Turret") || collider.CompareTag("Player")) && is_Placing)
        {
            colliders--;
            if (colliders <= 0)
            {
                go_PlaceableObject.GetComponent<SpriteRenderer>().color = Color.white;
                go_PlaceableObject.transform.Find("Head").GetComponent<SpriteRenderer>().color = Color.white;
                is_Placeable = true;
            }
        }
    }
}
