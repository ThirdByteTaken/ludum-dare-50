using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject go_Head;

    [SerializeField]
    float FireSpeed;

    [SerializeField]
    int Damage;
    [SerializeField]
    float Bullet_Speed;

    [SerializeField]
    bool Homing;

    [SerializeField]
    Bullet bullet;

    public Turret turret;

    Camera cam_Main;

    [HideInInspector]
    public List<Asteroid> ast_PossibleTargets = new List<Asteroid>();
    public Asteroid ast_Target;
    AudioSource ShootSound;
    void Start()
    {
        ShootSound = GetComponent<AudioSource>();
        cam_Main = Camera.main;
        Invoke("AttemptFire", FireSpeed);
    }
    public Asteroid BestTarget()
    {
        if (ast_PossibleTargets.Count > 0)
        {
            IEnumerable<Asteroid> ast_notDead = ast_PossibleTargets.Where(x => x.predictedHealth > 0);
            if (ast_notDead.Count() > 0)
            {
                return ast_notDead.OrderBy(x => x.height).First();
            }
        }
        return null;

    }
    public void AimAtTarget(GameObject Target)
    {
        Vector2 direction = ((Vector2)Target.transform.position - (Vector2)go_Head.transform.position).normalized;
        go_Head.transform.up = direction;
    }

    public void AttemptFire()
    {
        Asteroid ast_Target = BestTarget();
        if (ast_Target != null)
        {
            var shot = Instantiate(bullet.gameObject);
            shot.SetActive(true);
            Bullet shotBullet = shot.GetComponent<Bullet>();
            shotBullet.Homing = Homing;
            shotBullet.go_Target = ast_Target.gameObject;
            shotBullet.damage = Damage;
            shotBullet.Bullet_Speed = Bullet_Speed;
            shot.transform.position = go_Head.transform.position;
            ShootSound.Play();
        }
        Invoke("AttemptFire", FireSpeed);
    }

}
