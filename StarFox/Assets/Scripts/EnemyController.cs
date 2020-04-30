using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject Projectile;
    public GameObject player;
    public bool IsAttacking;
    public float distance;
    //public float shotsPerSeconds; impleemntacio dispars random
    // Start is called before the first frame update
    void Start()
    {
        //shotsPerSeconds = 1; impleemntacio dispars random
         InvokeRepeating("shoot", 1.0f, 1.0f); // implementacio dispars cada x segons
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(player.transform);
        distance = transform.position.z - player.transform.position.z;

       /* float probability = Time.deltaTime * shotsPerSeconds;
        if (Random.value < probability)
        {
            shoot();
        } */ //implementacio dispars random

        if (distance < 40)
            IsAttacking = true;
        else IsAttacking = false;

        
    }

    void shoot()
    {
        if (IsAttacking)
        {
            Quaternion q = Quaternion.identity;
            q.SetLookRotation(new Vector3(0, 1, 0), player.transform.position - transform.position);
            GameObject p = Instantiate(Projectile, transform.position + new Vector3(0.0f, 0.0f, 1.0f), q);
            
            ProjectileScript pps = (ProjectileScript)p.GetComponent(typeof(ProjectileScript));
            //p.transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
            p.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            pps.speed = 40;
            pps.movement = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y,  player.transform.position.z - transform.position.z).normalized;
            pps.tago = "Enemy_Projectile";
            pps.player = player;
        }
    }
}
