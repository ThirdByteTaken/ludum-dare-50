using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject go_Head;

    [SerializeField]
    float FireSpeed;

    [SerializeField]
    int Damage;

    [SerializeField]
    bool Homing;

    [SerializeField]
    Bullet bullet;

    Camera cam_Main;

    [HideInInspector]
    public GameObject go_Target;

    void Start()
    {
        cam_Main = Camera.main;
        Invoke("AttemptFire", FireSpeed);
    }

    public void AimAtTarget(GameObject Target)
    {
        if (go_Target == null) go_Target = Target;
        Vector2 direction = ((Vector2)go_Target.transform.position - (Vector2)go_Head.transform.position).normalized;
        go_Head.transform.up = direction;
    }

    public void AttemptFire()
    {
        if (go_Target != null)
        {
            var shot = Instantiate(bullet.gameObject);
            shot.SetActive(true);
            shot.GetComponent<Bullet>().Homing = Homing;
            shot.GetComponent<Bullet>().go_Target = go_Target;
            shot.transform.position = go_Head.transform.position;
        }

        Invoke("AttemptFire", FireSpeed);
    }

}
