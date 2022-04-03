using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    Rigidbody2D rigidBody;
    const int Min_Speed = 45;
    const int Max_Speed = 90;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        transform.up = new Vector2(Random.Range(-320, 320), Random.Range(-320, 320));
        rigidBody.AddForce(Random.Range(Min_Speed, Max_Speed) * transform.up, ForceMode2D.Impulse);
        Invoke("StopMoving", 1f);
    }

    void StopMoving()
    {
        rigidBody.velocity = Vector2.zero;
    }
}
