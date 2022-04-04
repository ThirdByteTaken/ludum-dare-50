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
            gun.ast_PossibleTargets.Add(collider.GetComponent<Asteroid>());
            Asteroid ast_gunBestTarget = gun.BestTarget();
            if (ast_gunBestTarget != null)
            {
                gun.AimAtTarget(gun.BestTarget().gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Asteroid"))
        {
            targets--;
            gun.ast_PossibleTargets.Remove(collider.GetComponent<Asteroid>());
            Asteroid ast_gunBestTarget = gun.BestTarget();
            if (ast_gunBestTarget != null)
            {
                gun.AimAtTarget(gun.BestTarget().gameObject);
            }
        }
    }

    /*void OnTriggerStay2D(Collider2D collider)
    {

        if (collider.CompareTag("Asteroid"))
        {
            if (collider.GetComponent<Asteroid>().predictedHealth > 0)
            {
                gun.AimAtTarget(collider.gameObject);
            }
        }
    }*/

}
