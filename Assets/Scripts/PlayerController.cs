using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject go_Player;
    public float moveSpeed;
    public float camSpeed;
    public int moveAxis = -1;// -1: stationary, 0: Vertical, 1: Horizontal    
    void Start()
    {
        go_Player = this.gameObject;
    }

    [SerializeField]
    CameraController cameraController;

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        float xMovement = horizontalInput * moveSpeed * Time.deltaTime;
        float yMovement = verticalInput * moveSpeed * Time.deltaTime;
        switch (moveAxis)
        {
            case -1: // if no movement on previous frame
                if (verticalInput != 0) // If there is input on vertical axis
                {
                    moveAxis = 0;
                    xMovement = 0; // cancel horizontal movement
                }
                else if (horizontalInput != 0) // If no input on vertical but input on horizontal
                {
                    moveAxis = 1;
                    yMovement = 0; // cancel vert movement
                }
                // otherwise moveAxis stays at -1
                break;
            case 0: // if vertical movement on previous frame
                if (verticalInput != 0) // if there is still movement on vert
                {
                    xMovement = 0;
                }
                else if (horizontalInput != 0) // no vert input but hor input
                {
                    moveAxis = 1;
                    yMovement = 0;
                }
                else // no input
                {
                    xMovement = 0;
                    yMovement = 0;
                    moveAxis = -1;
                }
                break;
            case 1:// if horizontal movemont on previous frame
                if (horizontalInput != 0) // if there is still movement on hor
                {
                    yMovement = 0;
                }
                else if (verticalInput != 0) // no hor input but vert input
                {
                    moveAxis = 0;
                    xMovement = 0;
                }
                else // no input
                {
                    xMovement = 0;
                    yMovement = 0;
                    moveAxis = -1;
                }
                break;
        }
        go_Player.transform.localPosition = new Vector2(Mathf.Clamp(go_Player.transform.localPosition.x + xMovement, -228, 228), Mathf.Clamp(go_Player.transform.localPosition.y + yMovement, -228, 228));
        cameraController.UpdatePosition();
    }
}
