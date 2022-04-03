using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool Homing = false; // TODO Add bullet homing depending on turret

    public GameObject go_Target;

    Asteroid asteroid;

    Vector3 targetPos;

    const float Bullet_Speed = 64f;
    const float Precision = 0.1f; // The lower, the more precise

    float timeToHit; // Amount of time between shooting and bullet hitting target

    void Start()
    {
        asteroid = go_Target.GetComponent<Asteroid>();
        if (!Homing)
        {
            Vector2 predictedPosition = go_Target.transform.position;
            float distance; // distance between turret and target

            for (float i = 0; i < 60; i += Precision)
            {
                predictedPosition += asteroid.FallVector * Precision;
                distance = Vector2.Distance(transform.position, predictedPosition);
                if (Bullet_Speed * i >= distance)
                {
                    break;
                }
            }

            targetPos = predictedPosition;
        }
    }

    void Update()
    {
        if (Homing)
        {
            transform.position = Vector3.MoveTowards(transform.position, go_Target.transform.position, Bullet_Speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Bullet_Speed * Time.deltaTime);
        }
    }
}
