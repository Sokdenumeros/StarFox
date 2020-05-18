using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject player;
    public GameObject Projectile1;
    public GameObject Projectile2;
    public GameObject Projectile3;
    private bool estatic;
    private float temps;
    private bool primer = true;
    private float tipusprojectil;
    // Start is called before the first frame update
    void Start()
    {
        estatic = false;
        temps = 0;
    }

    // Update is called once per frame
    void Update()
    {

        tipusprojectil = Random.Range(0.0f,3.1f);

        if (player.transform.position.z >= 550)
        {
            temps += Time.deltaTime;
            if (primer)
            {
                primer = false;
                transform.localPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 50);
            }


            Vector3 position = new Vector3(Random.Range(-1.0f,1.1f), Random.Range(-1.0f, 1.1f), 0);
            transform.localPosition += new Vector3(0,0,1) * 3 * Time.deltaTime;

            transform.localPosition += position * 10 * Time.deltaTime;


            if (temps >= 7)
            {

                if (tipusprojectil <= 1) creaProjectil(Projectile1, "blueproj", 40);
                else if (tipusprojectil > 1 && tipusprojectil <= 2) creaProjectil(Projectile2, "greenproj", 20);
                else if (tipusprojectil > 2 && tipusprojectil <= 3) creaProjectil(Projectile3, "purpleproj", 40);
              
                temps = 0;
            }
        }
    }

    void creaProjectil(GameObject Projectile, string tag, int sp)
    {
        Quaternion q = Quaternion.identity;
        q.SetLookRotation(new Vector3(0, 1, 0), player.transform.position - transform.position);
        GameObject p = Instantiate(Projectile, transform.position + new Vector3(0.0f, 0.0f, 1.0f), q);


        ProjectileScript pps = (ProjectileScript)p.GetComponent(typeof(ProjectileScript));

        //p.transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);

        pps.speed = sp;
        pps.movement = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z).normalized;
        pps.tago = tag;
        pps.player = player;
        pps.enemy = gameObject;
    }

          
}
