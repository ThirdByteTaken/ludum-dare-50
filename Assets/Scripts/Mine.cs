using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public static Mine Instance;

    // Start is called before the first frame update
    public int SpawnTime;
    public float MinSpeed;
    public float MaxSpeed;
    void Start()
    {
        Instance = this;
    }

    public void StartSpawning()
    {
        Invoke("SpawnRock", SpawnTime);
    }

    // Update is called once per frame
    void SpawnRock()
    {
        GameObject newRock = Instantiate(ResourceManager.Instance.go_Resources[0]);
        newRock.transform.position = new Vector2(transform.position.x - 16, transform.position.y + 12);
        newRock.GetComponent<Resource>().StartMoving(new Vector2(1, -1), Random.Range(MinSpeed, MaxSpeed));
        Invoke("SpawnRock", SpawnTime);
    }
}
