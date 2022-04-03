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
    void Start()
    {
        SpawnAsteroid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnAsteroid()
    {
        GameObject newAsteroid = Instantiate(go_AsteroidPrefab);
        Vector2 newFallVector = new Vector2(Random.Range(-moveSpeed, moveSpeed), Random.Range(-moveSpeed, moveSpeed));
        Vector2 newLandingPosition = new Vector2(Random.Range(-landingPositionsRadius / 2, landingPositionsRadius / 2), Random.Range(-landingPositionsRadius / 2, landingPositionsRadius / 2));
        float newHealth = Random.Range(minHealth, maxHealth);
        float newSecondsToFall = Random.Range(minFallTime, maxFallTime);
        newAsteroid.GetComponent<Asteroid>().SetUpAsteroid(newFallVector, newLandingPosition, newHealth, newSecondsToFall);
        Invoke("SpawnAsteroid", 1);
    }
}
