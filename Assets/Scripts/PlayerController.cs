using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject Player;
    public float moveSpeed;
    // Start is called before the first frame updat
    void Start()
    {
        Player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float yMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        Player.transform.localPosition = new Vector2(Player.transform.localPosition.x + xMovement, Player.transform.localPosition.y + yMovement);
    }
}
