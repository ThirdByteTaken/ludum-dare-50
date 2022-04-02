using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    GameObject go_Player;

    const int Tile_Size = 32;
    const int Horizontal_Tiles = 20;
    const int Vertical_Tiles = 20;

    // Update is called once per frame
    public void UpdatePosition()
    {
        var pos = go_Player.transform.position;
        var maxX = Horizontal_Tiles * Tile_Size;
        var maxY = Vertical_Tiles * Tile_Size;

        pos = new Vector3(Mathf.Clamp(pos.x, -maxX, maxX), Mathf.Clamp(pos.y, -maxY, maxY), -10);
        transform.position = pos;
    }
}
