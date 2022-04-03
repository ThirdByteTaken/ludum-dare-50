using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDetector : MonoBehaviour
{
    Gun gun;

    int targets;

    void Start()
    {
        gun = transform.parent.GetComponent<Gun>();
        targets = 0;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Asteroid"))
        {
            targets++;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Asteroid"))
        {
            targets--;
            gun.go_Target = null;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Asteroid"))
        {
            gun.AimAtTarget(collider.gameObject);
        }
    }

}
