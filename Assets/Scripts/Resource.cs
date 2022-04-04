using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    Rigidbody2D rigidBody;
    const int Min_Speed = 45;
    const int Max_Speed = 90;

    public Resources resource;
    public Vector2 Force;

    public void StartMoving(Vector2 rotation, float Force)
    {
        rigidBody = GetComponent<Rigidbody2D>();
        transform.up = rotation;
        rigidBody.AddForce(Force * transform.up, ForceMode2D.Impulse);
        Invoke("StopMoving", 1f);
    }
    public void StartMoving()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        transform.up = new Vector2(Random.Range(-320, 320), Random.Range(-320, 320));
        rigidBody.AddForce(Random.Range(Min_Speed, Max_Speed) * transform.up, ForceMode2D.Impulse);
        Invoke("StopMoving", 1f);
    }
    void StopMoving()
    {
        rigidBody.velocity = Vector2.zero;
        Invoke("Despawn", 10f);
    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            switch (resource)
            {
                case Resources.Rock:
                    ResourceManager.Instance.ChangeResource(1, 0, ref ResourceManager.Instance.Rock);
                    break;
                case Resources.Wood:
                    ResourceManager.Instance.ChangeResource(1, 1, ref ResourceManager.Instance.Wood);
                    break;
                case Resources.Iron:
                    ResourceManager.Instance.ChangeResource(1, 2, ref ResourceManager.Instance.Iron);
                    break;
                case Resources.Gold:
                    ResourceManager.Instance.ChangeResource(1, 3, ref ResourceManager.Instance.Gold);
                    break;

            }
            Destroy(this.gameObject);
        }
    }

    void Despawn()
    {
        Destroy(gameObject);
    }
}

public enum Resources
{
    Rock,
    Wood,
    Iron,
    Gold

}