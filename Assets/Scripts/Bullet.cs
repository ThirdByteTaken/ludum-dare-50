using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public bool Homing = false; // TODO Add bullet homing depending on turret
    [HideInInspector]
    public float damage;

    [HideInInspector]
    public GameObject go_Target;
    Asteroid ast_Target;

    Vector3 targetPos;
    [HideInInspector]
    public float Bullet_Speed = 64f;
    const float Precision = 0.1f; // The lower, the more precise

    float timeToHit; // Amount of time between shooting and bullet hitting target
    float secondsPassed;
    bool sortingOrderAdjusted;

    void Start()
    {
        ast_Target = go_Target.GetComponent<Asteroid>();
        if (!Homing)
        {
            Vector2 predictedPosition = go_Target.transform.position;
            float distance; // distance between turret and target

            for (float i = 0; i < 60; i += Precision)
            {
                predictedPosition += ast_Target.FallVector * Precision;
                distance = Vector2.Distance(transform.position, predictedPosition);
                if (Bullet_Speed * i >= distance)
                {
                    break;
                }
            }

            targetPos = predictedPosition;
        }
        ast_Target.predictedHealth -= damage;
        Vector2 direction = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        transform.up = direction;
    }

    void Update()
    {
        if (go_Target == null)
        {
            Destroy(gameObject);
            return;
        }
        if (Homing)
        {
            transform.position = Vector3.MoveTowards(transform.position, go_Target.transform.position, Bullet_Speed * Time.deltaTime);
            Vector2 direction = ((Vector2)go_Target.transform.position - (Vector2)transform.position).normalized;
            transform.up = direction;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Bullet_Speed * Time.deltaTime);
        }
        if (!sortingOrderAdjusted)
        {
            secondsPassed += Time.deltaTime;
            if (secondsPassed * Bullet_Speed > 16)
            {
                SpriteRenderer sr_bullet = GetComponent<SpriteRenderer>();
                sr_bullet.sortingLayerName = "Foreground";
                sr_bullet.sortingOrder = 0;
                sortingOrderAdjusted = true;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Asteroid"))
        {
            Asteroid ast_Collider = collider.GetComponent<Asteroid>();
            if (!collider.gameObject.Equals(go_Target)) // if bullet intercepted by another asteroid
            {
                ast_Target.predictedHealth += damage; // add back subtracted damage
            }

            ast_Collider.health -= damage;
            ast_Collider.HitSound.Play();
            Destroy(gameObject);
        }
    }
}
