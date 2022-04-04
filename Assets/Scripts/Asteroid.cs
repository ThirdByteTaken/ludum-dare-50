using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public SpriteRenderer sr_Asteroid;
    public Vector2 FallVector;
    public float height;
    public float fallSpeed;

    public float secondsToFall;
    public float health;
    public float predictedHealth; // health after bullets currently in the air hit it

    float startingHealth;

    bool ready = false;

    public Sprite[] spr_Asteroids;

    public GameObject go_resource;

    public void SetUpAsteroid(Vector2 _FallVector, Vector2 LandingPosition, float _health, float _secondsToFall)
    {
        FallVector = _FallVector;
        sr_Asteroid = GetComponent<SpriteRenderer>();
        secondsToFall = _secondsToFall;
        fallSpeed = 100 / secondsToFall; // After seconds to fall time, height will be zero
        transform.position = LandingPosition - (FallVector * secondsToFall);// new Vector2(LandingPosition.x-(FallVector.x * secondsToFall), LandingPosition.y-(FallVector.y * secondsToFall));
        transform.Rotate(new Vector3(0, 0, Random.Range(0, 90)));
        health = _health;
        startingHealth = health;
        ready = true;
    }
    void Start()
    {
        predictedHealth = health;
    }
    // Update is called once per frame
    void Update()
    {
        if (!ready) return;
        Vector2 fallAmount = FallVector * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + fallAmount.x, transform.position.y + fallAmount.y);
        transform.localScale = new Vector2(50 - (height * .5f), 50 - (height * .5f)); // multiplied by .5 to change from scale of 100 to scale of 50
        height -= fallSpeed * Time.deltaTime;
        sr_Asteroid.color = new Color(1 - (height / 100f), sr_Asteroid.color.g, sr_Asteroid.color.b);
        sr_Asteroid.sortingOrder = (int)height;

        if (height <= 0)
        {
            ExplosionManager.Instance.Explode(transform.position);
            AsteroidManager.DestroyAsteroid(gameObject);
            print("you got blown up");
        }
        if (health <= 0)
        {
            var resource = Instantiate(go_resource);
            resource.GetComponent<Resource>().StartMoving();
            resource.transform.position = transform.position;
            AsteroidManager.DestroyAsteroid(gameObject);
            return;
        }
        sr_Asteroid.sprite = spr_Asteroids[Mathf.RoundToInt((health / startingHealth) * (spr_Asteroids.Length - 1))];
    }

}
