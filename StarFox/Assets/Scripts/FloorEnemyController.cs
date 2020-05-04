using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEnemyController : MonoBehaviour
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
        InvokeRepeating("shoot", 1.0f, 3.0f); // implementacio dispars cada x segons
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

        if (distance < 100)
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
            GameObject p1 = Instantiate(Projectile, transform.position + new Vector3(0.0f, 0.0f, 1.0f), q);
            GameObject p2 = Instantiate(Projectile, transform.position + new Vector3(0.0f, 0.0f, 1.0f), q);
            GameObject p3 = Instantiate(Projectile, transform.position + new Vector3(0.0f, 0.0f, 1.0f), q);
            GameObject p4 = Instantiate(Projectile, transform.position + new Vector3(0.0f, 0.0f, 1.0f), q);

            ProjectileScript pps = (ProjectileScript)p.GetComponent(typeof(ProjectileScript));
            ProjectileScript pps1 = (ProjectileScript)p1.GetComponent(typeof(ProjectileScript));
            ProjectileScript pps2 = (ProjectileScript)p2.GetComponent(typeof(ProjectileScript));
            ProjectileScript pps3 = (ProjectileScript)p3.GetComponent(typeof(ProjectileScript));
            ProjectileScript pps4 = (ProjectileScript)p4.GetComponent(typeof(ProjectileScript));
            //p.transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
            p.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            pps.speed = 40;
            pps.movement = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z).normalized;
            pps.tago = "Enemy_Projectile";
            pps.player = player;
            pps.enemy = gameObject;

            p1.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            pps1.speed = 40;
            pps1.movement = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y + 3, player.transform.position.z - transform.position.z).normalized;
            pps1.tago = "Enemy_Projectile";
            pps1.player = player;
            pps1.enemy = gameObject;

            p2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            pps2.speed = 40;
            pps2.movement = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y - 3, player.transform.position.z - transform.position.z).normalized;
            pps2.tago = "Enemy_Projectile";
            pps2.player = player;
            pps2.enemy = gameObject;

            p3.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            pps3.speed = 40;
            pps3.movement = new Vector3(player.transform.position.x - transform.position.x + 3, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z).normalized;
            pps3.tago = "Enemy_Projectile";
            pps3.player = player;
            pps3.enemy = gameObject;

            p4.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            pps4.speed = 40;
            pps4.movement = new Vector3(player.transform.position.x - transform.position.x - 3, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z).normalized;
            pps4.tago = "Enemy_Projectile";
            pps4.player = player;
            pps4.enemy = gameObject;
        }
    }
}