using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController Instance;
    [SerializeField]
    GameObject go_Player;

    [SerializeField]
    PlayerController playerController;

    static int Max_Y = 203;
    static int Max_X = 112;

    const int Cam_Move_Threshold = 26; // How far the camera trails behind

    const float Cam_Speed = 3.0f;
    void Awake()
    {
        Instance = this;
    }
    public void SetTarget(GameObject target)
    {
        go_Player = target;
    }
    // Update is called once per frame
    public void UpdatePosition()
    {
        var pos = go_Player.transform.position;
        var xDif = transform.position.x - go_Player.transform.position.x;
        var yDif = transform.position.x - go_Player.transform.position.x;


        transform.position = Vector3.Lerp(transform.position, new Vector2(transform.position.x, pos.y), Cam_Speed * Time.deltaTime); // Interpolates on y axis
        transform.position = Vector3.Lerp(transform.position, new Vector2(pos.x, transform.position.y), Cam_Speed * Time.deltaTime); // Interpolates on x axis
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -Max_X, Max_X), Mathf.Clamp(transform.position.y, -Max_Y, Max_Y), -10); // Clamps cam position
    }
}
