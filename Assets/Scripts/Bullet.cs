using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool Homing = false; // TODO Add bullet homing depending on turret

    public GameObject go_Target;

    Vector3 targetPos;

    const float Bullet_Speed = 64f;

    void Start()
    {
        if (!Homing) targetPos = go_Target.transform.position;
    }

    void Update()
    {
        if (!Homing)
        {
            transform.position = Vector3.MoveTowards(transform.position, go_Target.transform.position, Bullet_Speed * Time.deltaTime);
        }
    }
}
