using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public GameObject go_RangeVisualizer;
    private static GameObject s_go_ExplosionPrefab;

    public static AsteroidManager Instace;

    public AnimationCurve SpawnCurve;
    public AnimationCurve RadiusCurve;

    public static int AsteroidsDestroyed;

    public TMP_Text txt_Score;

    public Sprite[] spr_Asteroids;
    bool isSpawning;

    void Start()
    {
        s_go_ExplosionPrefab = go_ExplosionPrefab;
        Instace = this;
        isSpawning = false;
        AsteroidsDestroyed = 0;
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            Invoke("SpawnAsteroid", 3);
        }
    }

    void SpawnAsteroid()
    {
        GameObject newAsteroid = Instantiate(go_AsteroidPrefab);
        newAsteroid.GetComponent<SpriteRenderer>().sprite = spr_Asteroids[Random.Range(0, spr_Asteroids.Length)];
        Vector2 newFallVector = new Vector2(Random.Range(-moveSpeed, moveSpeed), Random.Range(-moveSpeed, moveSpeed));
        landingPositionsRadius = RadiusCurve.Evaluate(AsteroidsDestroyed);
        go_RangeVisualizer.transform.localScale = Vector2.one * (1f / 32f) * (landingPositionsRadius / 32);
        Vector2 newLandingPosition = new Vector2(Random.Range(-landingPositionsRadius / 2, landingPositionsRadius / 2), Random.Range(-landingPositionsRadius / 2, landingPositionsRadius / 2));
        float newHealth = Random.Range(minHealth, maxHealth);
        float newSecondsToFall = Random.Range(minFallTime, maxFallTime);
        newAsteroid.GetComponent<Asteroid>().go_resource = ResourceManager.Instance.RandomResource();
        newAsteroid.GetComponent<Asteroid>().SetUpAsteroid(newFallVector, newLandingPosition, newHealth, newSecondsToFall);
        Invoke("SpawnAsteroid", 60 / SpawnCurve.Evaluate(AsteroidsDestroyed));
    }

    public void DestroyAsteroid(GameObject asteroid, bool is_gameOver)
    {
        GameObject explosion = Instantiate(s_go_ExplosionPrefab, asteroid.transform.position, asteroid.transform.rotation);
        AsteroidsDestroyed++;
        txt_Score.text = string.Format("{0:000000}", AsteroidsDestroyed);
        Destroy(asteroid);
        Destroy(explosion, 0.5f);
        if (is_gameOver) Invoke("EndGame", 2f);
    }
    void EndGame()
    {
        ScoreManager.Score = AsteroidsDestroyed;
        SceneLoader.PlayerDeath();
    }
}
