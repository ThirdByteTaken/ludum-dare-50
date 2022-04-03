using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public float maxFallTime;
    public float minFallTime;

    public float minHealth;
    public float maxHealth;

    public float landingPositionsRadius;
    public float moveSpeed;
    public GameObject go_AsteroidPrefab;
    public GameObject go_ExplosionPrefab;
    private static GameObject s_go_ExplosionPrefab;

    public Sprite[] spr_Asteroids;
    void Start()
    {
        s_go_ExplosionPrefab = go_ExplosionPrefab;
        SpawnAsteroid();

    }

    void SpawnAsteroid()
    {
        GameObject newAsteroid = Instantiate(go_AsteroidPrefab);
        newAsteroid.GetComponent<SpriteRenderer>().sprite = spr_Asteroids[Random.Range(0, spr_Asteroids.Length)];
        Vector2 newFallVector = new Vector2(Random.Range(-moveSpeed, moveSpeed), Random.Range(-moveSpeed, moveSpeed));
        Vector2 newLandingPosition = new Vector2(Random.Range(-landingPositionsRadius / 2, landingPositionsRadius / 2), Random.Range(-landingPositionsRadius / 2, landingPositionsRadius / 2));
        float newHealth = Random.Range(minHealth, maxHealth);
        float newSecondsToFall = Random.Range(minFallTime, maxFallTime);
        newAsteroid.GetComponent<Asteroid>().SetUpAsteroid(newFallVector, newLandingPosition, newHealth, newSecondsToFall);
        Invoke("SpawnAsteroid", 1);
    }

    public static void DestroyAsteroid(GameObject asteroid)
    {
        print("ah");
        GameObject explosion = Instantiate(s_go_ExplosionPrefab, asteroid.transform.position, asteroid.transform.rotation);
        Destroy(asteroid);
        Destroy(explosion, 0.5f);

    }
}
