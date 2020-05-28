using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2Script : MonoBehaviour
{
    public GameObject player;
    public GameObject Basic;
    public GameObject Projectile1;
    public GameObject Projectile2;
    private float temps;
    public int bosslifes;
    private bool primer = true;
    private float tipusprojectil;
    private float tipusmoviment;
    public GameObject Explosion;
    private Vector3 position;
    private bool stopmov;
    private bool prime;
    private bool quiet;
    private bool first;
    private float tempsmov;
    private float tempsproj;
    private float tempsenemics;
    private float tempsb;
    public AudioSource dispar;
    public AudioSource dead;
    public PB HP;
    public EndgameUI EUI;
    // Start is called before the first frame update
    void Start()
    {
        HP.max = bosslifes;
        temps = 0;
        tempsb = 0;
        tipusmoviment = Random.Range(0.0f, 0.81f);
        stopmov = false;
        prime = true;
        tempsmov = 0;
        tempsproj = 0;
        quiet = false;
        tempsenemics = 0;
        first = true;
    }

    // Update is called once per frame
    void Update()
    {

        

        tipusprojectil = Random.Range(0.0f, 2.1f);


        if (player != null && player.transform.position.z >= 1391.5)
        {
            if(first)
            {
                tempsb += Time.deltaTime;
                transform.localPosition += new Vector3(player.transform.position.x, player.transform.position.y + 50, -player.transform.position.z).normalized * 60 * Time.deltaTime;

                if (tempsb >= 2)
                {
                    tempsb = 0;
                    first = false;
                    SetBossText();
                }
            }

           
            if (primer)
            {


                tempsenemics += Time.deltaTime;
                if (tempsenemics >= 1)
                {
                    enemicbasic();
                    tempsenemics = 0;
                }

                temps += Time.deltaTime;
                if (temps >= 50)
                {

                    tempsb += Time.deltaTime;
                    transform.localPosition += new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z).normalized * 60 * Time.deltaTime;

                    if (tempsb >= 1)
                    {
                        tempsb = 0;
                        temps = 0;
                        primer = false;
                    }
                    
                    
                    
                }

                tempsproj += Time.deltaTime;
                if (tempsproj >= 7)
                {

                    if (tipusprojectil <= 1) creaProjectil(Projectile1, "blueproj", 40);
                    else if (tipusprojectil > 1 && tipusprojectil <= 2) creaProjectil(Projectile2, "purpleproj", 40);

                    tempsproj = 0;
                }
            }

           

            else
            {

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

            }

            transform.localPosition += new Vector3(0, 0, 1) * 30 * Time.deltaTime;
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
        pps.dispar = dispar;
    }

    void SetBossText()
    {
        HP.gameObject.SetActive(true);
        HP.setCurrent(bosslifes);
        if (bosslifes == 0)
        {
            Quaternion q = Quaternion.identity;
            GameObject p = Instantiate(Explosion, transform.position, q);
            p.transform.localScale = new Vector3(20.0f, 20.0f, 20.0f);
            dead.Play();
            bossdeath();
        }

    }

    void bossdeath()
    {
        EUI.Victory();
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
            if (prime) transform.localPosition += pos * 20 * Time.deltaTime;
            else if (!prime) transform.localPosition -= pos * 20 * Time.deltaTime;
            if (tempsmov >= 3)
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

    void enemicbasic()
    {
        Vector3 aux = transform.position;
        GameObject e = Instantiate(Basic, aux, Quaternion.identity);
        CarController ec = (CarController)e.GetComponent(typeof(CarController));
        ec.movement = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 100);

        GameObject e1 = Instantiate(Basic, aux, Quaternion.identity);
        CarController ec1 = (CarController)e1.GetComponent(typeof(CarController));
        ec1.movement = new Vector3(player.transform.position.x - 10, player.transform.position.y, player.transform.position.z + 100);

        GameObject e2 = Instantiate(Basic, aux, Quaternion.identity);
        CarController ec2 = (CarController)e2.GetComponent(typeof(CarController));
        ec2.movement = new Vector3(player.transform.position.x + 10, player.transform.position.y, player.transform.position.z + 100);

        GameObject e3 = Instantiate(Basic, aux, Quaternion.identity);
        CarController ec3 = (CarController)e3.GetComponent(typeof(CarController));
        ec3.movement = new Vector3(player.transform.position.x, player.transform.position.y + 10, player.transform.position.z + 100);

        GameObject e4 = Instantiate(Basic, aux, Quaternion.identity);
        CarController ec4 = (CarController)e4.GetComponent(typeof(CarController));
        ec4.movement = new Vector3(player.transform.position.x, player.transform.position.y - 10, player.transform.position.z + 100);

        GameObject e5 = Instantiate(Basic, aux, Quaternion.identity);
        CarController ec5 = (CarController)e5.GetComponent(typeof(CarController));
        ec5.movement = new Vector3(player.transform.position.x - 10, player.transform.position.y - 10, player.transform.position.z + 100);

        GameObject e6 = Instantiate(Basic, aux, Quaternion.identity);
        CarController ec6 = (CarController)e6.GetComponent(typeof(CarController));
        ec6.movement = new Vector3(player.transform.position.x + 10, player.transform.position.y + 10, player.transform.position.z + 100);

        GameObject e7 = Instantiate(Basic, aux, Quaternion.identity);
        CarController ec7 = (CarController)e7.GetComponent(typeof(CarController));
        ec7.movement = new Vector3(player.transform.position.x + 10, player.transform.position.y - 10, player.transform.position.z + 100);

        GameObject e8 = Instantiate(Basic, aux, Quaternion.identity);
        CarController ec8 = (CarController)e8.GetComponent(typeof(CarController));
        ec8.movement = new Vector3(player.transform.position.x - 10, player.transform.position.y + 10, player.transform.position.z + 100);

        /*GameObject e1 = Instantiate(Basic, aux + new Vector3(6, 0, 0), q);
        EnemyController ec1 = (EnemyController)e1.GetComponent(typeof(EnemyController));
        ec1.player = player;


        GameObject e2 = Instantiate(Basic, aux - new Vector3(6, 0, 0), q);
        EnemyController ec2 = (EnemyController)e2.GetComponent(typeof(EnemyController));
        ec2.player = player;

        GameObject e3 = Instantiate(Basic, aux + new Vector3(0, 6, 0), q);
        EnemyController ec3 = (EnemyController)e3.GetComponent(typeof(EnemyController));
        ec3.player = player;


        GameObject e4 = Instantiate(Basic, aux - new Vector3(0, 6, 0), q);
        EnemyController ec4 = (EnemyController)e4.GetComponent(typeof(EnemyController));
        ec4.player = player;*/


    }
}
