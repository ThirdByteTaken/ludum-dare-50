using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField]
    GameObject go_Explosion;

    public static ExplosionManager Instance;

    void Start()
    {
        Instance = this;
    }

    public void Explode(Vector3 position)
    {
        Instantiate(go_Explosion, position, Quaternion.Euler(0, 0, 0));
    }
}
