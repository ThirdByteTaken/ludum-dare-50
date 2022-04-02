using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    private GameObject go_PlaceableObject;
    private Camera cam_Main;
    public bool isPlacing;

    // Start is called before the first frame update
    void Start()
    {
        go_PlaceableObject = this.gameObject;
        cam_Main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacing)
        {
            Vector2 mousePosition = DevTools.RoundVector3(cam_Main.ScreenToWorldPoint(Input.mousePosition), 32);
            go_PlaceableObject.transform.localPosition = mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                isPlacing = false;
            }
        }
    }
}
