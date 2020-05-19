using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public GameObject player;
    public GameObject Projectile1;
    public GameObject Projectile2;
    public GameObject Projectile3;
    private float temps;
    public Text bossText;
    public int bosslifes;
    private bool primer = true;
    private float tipusprojectil;
    private float tipusmoviment;
    public GameObject Explosion;
    private Vector3 position;
    private bool stopmov;
    private bool prime;
    private bool quiet;
    private float tempsmov;
    // Start is called before the first frame update
    void Start()
    {
        temps = 0;
        bossText.text = "No Boss yet";
        tipusmoviment = Random.Range(0.0f, 0.81f);
        stopmov = false;
        prime = true;
        tempsmov = 0;
        quiet = false;
    }

    // Update is called once per frame
    void Update()
    {

        tipusprojectil = Random.Range(0.0f, 3.1f);
        

        if (player.transform.position.z >= 550)
        {

            temps += Time.deltaTime;
            if (primer)
            {
                SetBossText();
                primer = false;
                transform.localPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 50);
            }


            
            transform.localPosition += new Vector3(0, 0, 1) * 3 * Time.deltaTime;

            if (stopmov == false)
            {
                tipusmoviment = Random.Range(0.0f, 0.8f);
                if (tipusmoviment <= 0.1) position = new Vector3(1, 1, 0);
                else if (tipusmoviment <= 0.2) position = new Vector3(1, 0, 0);
                else if (tipusmoviment <= 0.3) position = new Vector3(0, 1, 0);
                else if (tipusmoviment <= 0.4) position = new Vector3(-1, 1, 0);
                else if (tipusmoviment <= 0.5) position = new Vector3(1, -1, 0);
                else if (tipusmoviment <= 0.6) position = new Vector3(0, -1, 0);
                else if (tipusmoviment <= 0.7) position = new Vector3(-1, -1, 0);
                else if (tipusmoviment <= 0.8) position = new Vector3(-1, 0, 0);
                
            }



           moviment(position);

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

    void SetBossText()
    {
        bossText.text = "BOSS HP: " + bosslifes;
        if (bosslifes == 0) bossdeath();

    }

    void bossdeath()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile_power"))
        {
            --bosslifes;
            SetBossText();
            Quaternion q = Quaternion.identity;
            Instantiate(Explosion, transform.position, q);
        }
    }

    void moviment(Vector3 pos)
    {
        stopmov = true;

        if (quiet)
        {
            tempsmov += Time.deltaTime;
            if (tempsmov >= 1)
            {
                stopmov = false;
                quiet = false;
                tempsmov = 0;
            }
        }

        else if (stopmov)
        {
            tempsmov += Time.deltaTime;
            if (prime) transform.localPosition += pos * 10 * Time.deltaTime;
            else if (!prime) transform.localPosition -= pos * 10 * Time.deltaTime;
            if (tempsmov >= 1)
            {
                if (prime)
                {
                    prime = false;
                }
                else
                {
                    prime = true;
                    quiet = true;
                }
                tempsmov = 0;
            }
        }
    }
}
