using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    GameObject go_Player;

    [SerializeField]
    PlayerController playerController;

    const int Tile_Size = 32;
    const int Horizontal_Tiles = 20;
    const int Vertical_Tiles = 20;

    const int Cam_Move_Threshold = 26; // How far the camera trails behind

    const float Cam_Speed = 3.0f;

    // Update is called once per frame
    public void UpdatePosition()
    {
        var pos = go_Player.transform.position;
        var maxX = Horizontal_Tiles * Tile_Size;
        var maxY = Vertical_Tiles * Tile_Size;
        var xDif = transform.position.x - go_Player.transform.position.x;
        var yDif = transform.position.x - go_Player.transform.position.x;


        transform.position = Vector3.Lerp(transform.position, new Vector2(transform.position.x, pos.y), Cam_Speed * Time.deltaTime); // Interpolates on y axis
        transform.position = Vector3.Lerp(transform.position, new Vector2(pos.x, transform.position.y), Cam_Speed * Time.deltaTime); // Interpolates on x axis
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -maxX, maxX), Mathf.Clamp(transform.position.y, -maxY, maxY), -10); // Clamps cam position
    }
}
