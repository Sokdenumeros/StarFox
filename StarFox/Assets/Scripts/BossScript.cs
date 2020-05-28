using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public GameObject player;
    public GameObject Basic;
    public GameObject Normal;
    public GameObject movimente;
    public GameObject Projectile1;
    public GameObject Projectile2;
    public GameObject Projectile3;
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
    private float tempsmov;
    private int counte;
    private float tempsenemics;
    public AudioSource basica;
    public AudioSource normala;
    public AudioSource movimenta;
    public AudioSource dispar;
    public AudioSource collect;
    public AudioSource dead;
    public AudioSource bosssound;
    public AudioSource musicstop;
    public AudioSource victory;
    public PB HP;
    public EndgameUI EUI;

    // Start is called before the first frame update
    void Start()
    {
        HP.max = bosslifes;
        temps = 0;
        tipusmoviment = Random.Range(0.0f, 0.81f);
        stopmov = false;
        prime = true;
        tempsmov = 0;
        quiet = false;
        tempsenemics = 0;
        counte = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            tipusprojectil = Random.Range(0.0f, 3.1f);


            if (player.transform.position.z >= 889.5529)
            {
                tempsenemics += Time.deltaTime;
                if (tempsenemics >= 5)
                {
                    if (counte == 0) enemicbasic();
                    else if (counte == 1) enemicnormal();
                    else if (counte == 2) enemicmoviment();
                    ++counte;
                    if (counte == 3) counte = 0;
                    tempsenemics = 0;
                }

                temps += Time.deltaTime;
                if (primer)
                {
                    bosssound.Play();
                    musicstop.Stop();
                    SetBossText();
                    primer = false;
                    transform.localPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 150);
                }



                transform.localPosition += new Vector3(0, 0, 1) * 30 * Time.deltaTime;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, player.transform.position.z + 150);

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
        pps.collect = collect;
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
        victory.Play();
        Time.timeScale = 0;
        Destroy(gameObject);
        EUI.Victory();
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
        Quaternion q = Quaternion.identity;
        GameObject e = Instantiate(Basic, aux, q);
        EnemyController ec = (EnemyController)e.GetComponent(typeof(EnemyController));
        ec.player = player;
        ec.dispar = basica;

        GameObject e1 = Instantiate(Basic, aux + new Vector3(6, 0, 0), q);
        EnemyController ec1 = (EnemyController)e1.GetComponent(typeof(EnemyController));
        ec1.player = player;
        ec1.dispar = basica;


        GameObject e2 = Instantiate(Basic, aux - new Vector3(6, 0, 0), q);
        EnemyController ec2 = (EnemyController)e2.GetComponent(typeof(EnemyController));
        ec2.player = player;
        ec2.dispar = basica;

        GameObject e3 = Instantiate(Basic, aux + new Vector3(0, 6, 0), q);
        EnemyController ec3 = (EnemyController)e3.GetComponent(typeof(EnemyController));
        ec3.player = player;
        ec3.dispar = basica;


        GameObject e4 = Instantiate(Basic, aux - new Vector3(0, 6, 0), q);
        EnemyController ec4 = (EnemyController)e4.GetComponent(typeof(EnemyController));
        ec4.player = player;
        ec4.dispar = basica;


    }
    void enemicnormal() {


        Vector3 aux1 = transform.position;
        Quaternion qn = Quaternion.identity;
        GameObject en = Instantiate(Normal, aux1, qn);
        FloorEnemyController ecn = (FloorEnemyController)en.GetComponent(typeof(FloorEnemyController));
        ecn.player = player;
        ecn.dispar = normala;


        GameObject en1 = Instantiate(Normal, aux1 - new Vector3(0,  15, 0), qn);
        FloorEnemyController ecn1 = (FloorEnemyController)en1.GetComponent(typeof(FloorEnemyController));
        ecn1.player = player;
        ecn1.dispar = normala;

        GameObject en2 = Instantiate(Normal, aux1 + new Vector3(15, 0, 0), qn);
        FloorEnemyController ecn2 = (FloorEnemyController)en2.GetComponent(typeof(FloorEnemyController));
        ecn2.player = player;
        ecn2.dispar = normala;
    }
    void enemicmoviment() {

        Vector3 aux2 = transform.position;
        Quaternion qm = Quaternion.identity;
        GameObject em = Instantiate(movimente, aux2, qm);
        EnemyController ema = (EnemyController)em.GetComponent(typeof(EnemyController));
        ema.player = player;
        ema.dispar = movimenta;
        ema.distdis = 200;

        MovingEnemyController emb = (MovingEnemyController)em.GetComponent(typeof(MovingEnemyController));
        emb.player = player;
        emb.distdisp = player.transform.position.z + 100;
        emb.speed = 30;
        emb.speed_constant = 30;

    }
}
