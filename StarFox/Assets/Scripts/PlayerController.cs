using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem turbollum;
    public GameObject Explosion;
    public float speed;
    public GameObject Focturbo;
    public int speedbarrel;
    public float speed_constant;
    private bool dontcrossright;
    private bool dontcrosstop;
    private bool dontcrossbot;
    private bool dontcrossleft;
    public Text winText;
    public GameObject Projectile;
    public GameObject Projectile_power;
    public GameObject insect;
    public GameObject camera;
    private int health;
    private bool turbo;
    private bool hackfast;
    private float time;
    private bool controlsinvertits;
    private float turbotimer;
    private float tempsrel;
    private int turbocount;
    public BoostUI boostUI;
    public Text powerText;
    private bool barrelroll;
    private int rotation;
    public AudioSource shotsound;
    public AudioSource enemykill;
    public AudioSource damaged;
    public AudioSource spin;
    public AudioSource nitro;
    public AudioSource notturbo;
    public AudioSource killexplo;
    public int speed_rel;
    private bool inmortal;
    private bool relantitzat;
    private float tempsinvertit;
    private bool visionula;
    private Vector3 movement;
    private float moveHorizontal;
    private float moveVertical;
    private float tempsvisionula;
    private bool esquerra;
    private int countpower;
    public int greentopowerup;
    public Rigidbody rb;
    public PB hp;
    public PB barrelR;
    private float graus;
    private bool baresqu;
    private bool bardreta;
    private float timeshot;
    public EndgameUI endUI;

    void Start()
    {

        baresqu = false;
        bardreta = false;
        movement = new Vector3(0, 0, 0);
        health = 100;
        hp.max = 100;
        barrelR.max = 5;
        SetCountText();
        SetPowerText();
        winText.text = "";
        turbo = false;
        barrelroll = false;
        rotation = 0;
        turbotimer = 0;
        turbocount = 3;
        tempsrel = 0;
        SetTurboText();
        SetBarrelText();
        inmortal = false;
        relantitzat = false;
        dontcrossright = false;
        dontcrosstop = false;
        dontcrossbot = false;
        dontcrossleft = false;
        controlsinvertits = false;
        visionula = false;
        tempsinvertit = 0;
        tempsvisionula = 0;
        esquerra = true;
        countpower = 0;
        time = 0;
        graus = 45;
        turbollum.Pause();

    }

    void Update()
    {

        timeshot -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (countpower == greentopowerup) {
                shoot(Projectile_power, "Projectile_power", false, 60);
                countpower = 0;
                SetPowerText();
            }
            else
            {
                shoot(Projectile, "Projectile", true, 250);
            }
        }
        if (Input.GetKey(KeyCode.Space)) if (timeshot <= 0.0f) shoot(Projectile, "Projectile", true, 250);
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        if (Input.GetKeyDown(KeyCode.N)){
            turboacc();
            }
            if (Input.GetKeyDown(KeyCode.B))
            {

                if (time == 0 && Input.GetAxis("Horizontal") != 0)
                {
                    barrelroll = true;
                    
                    time += Time.deltaTime;
                }
            }

            if (Input.GetKeyDown(KeyCode.F) && inmortal)
            {
                if (hackfast == false)
                {
                    speed_constant = 200;
                    hackfast = true;
                }
                else
                {
                    speed_constant = 30;
                    hackfast = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.P) && inmortal)
            {

                countpower = greentopowerup;
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                inmortal = !inmortal;
            if (inmortal) hp.setColor(new Color32(255, 255, 0, 255));
            else hp.setColor(new Color32(0, 255, 0, 255));
            health = 100;
            SetCountText();
        }




            SetBarrelText();

            if (turbo)
            {
                turbotimer += Time.deltaTime;
                if (turbotimer >= 1)
                {
                    speed_constant = 30;
                    turbotimer = 0;
                    turbo = false;
                    turbollum.Pause();
                }
            }

            if (barrelroll)
            {
                if (bardreta == false  && Input.GetAxis("Horizontal") > 0)
                {
                    bardreta = true;
                    spin.Play();
            }

                else if(baresqu == false  && Input.GetAxis("Horizontal") < 0 )
                {
                    baresqu = true;
                    spin.Play();
            }

                if (bardreta)
                {
                    transform.localEulerAngles = new Vector3(0.0f, 0.0f, -rotation);
                    rotation = rotation + 5;
                    if (rotation == 360)
                    {
                        rotation = 0;
                        barrelroll = false;
                        bardreta = false;
                    }

                }

                else if (baresqu)
                {

                    transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotation);
                    rotation = rotation + 5;
                    if (rotation == 360)
                    {
                        rotation = 0;
                        barrelroll = false;
                        baresqu = false;
                    }

                }
                

            }

            if (relantitzat)
            {
                tempsrel += Time.deltaTime;
                graus = 20;
                if (tempsrel >= 5)
                {
                    tempsrel = 0;
                graus = 45;
                    relantitzat = false;
                }
            }

            if (controlsinvertits)
            {
                tempsinvertit += Time.deltaTime;
                moveVertical = Input.GetAxis("Horizontal");
                moveHorizontal = Input.GetAxis("Vertical");
                movement = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0.0f);
                if (tempsinvertit >= 5)
                {
                    tempsinvertit = 0;
                    controlsinvertits = false;
                }
            }

            else
            {
                moveHorizontal = Input.GetAxis("Horizontal");
                moveVertical = Input.GetAxis("Vertical");
                movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            }

        if (visionula)
        {

            tempsvisionula += Time.deltaTime;

            insect.transform.localPosition = new Vector3(camera.transform.position.x, camera.transform.position.y - 3, camera.transform.position.z + 3);
            if (esquerra)
            {
                camera.transform.localPosition += new Vector3(1, 0, 0) * 5 * Time.deltaTime;
                esquerra = false;
            }
            else if (!esquerra)
            {
                camera.transform.localPosition += new Vector3(-1, 0, 0) * 5 * Time.deltaTime;
                esquerra = true;
            }
            if (tempsvisionula >= 5)
            {
                insect.transform.localPosition = new Vector3(-10, -10, -10);
                tempsvisionula = 0;
                visionula = false;
            }
        }
         

       
    }

    void FixedUpdate()
    {
        transform.localEulerAngles = new Vector3(-moveVertical * graus , moveHorizontal * graus, -moveHorizontal * graus);
    
        if ((dontcrossright && moveHorizontal > 0) || (dontcrossleft && moveHorizontal < 0)) movement = new Vector3(0.0f, moveVertical, 0.0f);
        else {
            dontcrossright = false;
            dontcrossleft = false;
        }

        if ((dontcrosstop && moveVertical > 0) || (dontcrossbot && moveVertical < 0)) movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        else
        {
            dontcrosstop = false;
            dontcrossbot = false;
        }



        movementController();
    }

  

    void movementController()
    {
        if(barrelroll && (bardreta || baresqu))
        {
            rb.velocity = (movement * speedbarrel) + (new Vector3(0.0f, 0.0f, speed_constant));
        }
        else rb.velocity = Quaternion.Euler(transform.localEulerAngles) * (new Vector3(0.0f, 0.0f, speed_constant));
        
    }

    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Enemy_Projectile"))
        {
            damage(10);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            SetCountText();
            damaged.Play();

        }

        else if (other.gameObject.CompareTag("barrera") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Enemymover"))
        {
            damage(10);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Instantiate(Explosion, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            SetCountText();
            damaged.Play();
        }

        else if (other.gameObject.CompareTag("obstacle") || other.gameObject.CompareTag("terrain"))

        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            killexplo.Play();
            kill();
        }

        if (other.gameObject.CompareTag("redvelcol"))
        {
            speed_constant = 30;
            speedbarrel = 37;
            
        }

        if (other.gameObject.CompareTag("Enemy_Laser")) relantitzat = true;

        if (other.gameObject.CompareTag("purpleproj")) controlsinvertits = true;

        if(other.gameObject.CompareTag("blueproj")) visionula = true;

        if (other.gameObject.CompareTag("greenproj") || other.gameObject.CompareTag("static_green"))
        {
            if (countpower < greentopowerup)
            {
                ++countpower;
                SetPowerText();
            }
        }
        if (other.gameObject.CompareTag("RightL")) dontcrossright = true;
        else if (other.gameObject.CompareTag("LeftL")) dontcrossleft = true;


        if (other.gameObject.CompareTag("TopL")) dontcrosstop = true;
        else if (other.gameObject.CompareTag("BotL")) dontcrossbot = true;

        


    }

    void damage(int damage) {
        health -= damage;
        SetCountText();
    }

    void SetCountText()
    {
        hp.setCurrent(health);
        if (health == 0)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            killexplo.Play();
            kill();
        }
        
    }

    void SetBarrelText()
    {
        if (time > 0)
        {
            time += Time.deltaTime;
            barrelR.setCurrent(time);
            barrelR.gameObject.SetActive(true);
        }
        if (time >= 5)
        {
            barrelR.setCurrent(5);
            barrelR.gameObject.SetActive(false);
            time = 0;
        }
    }

    void SetPowerText()
    {
        if (countpower == greentopowerup) powerText.text = "SUPER SHOT: READY";
        else powerText.text = "SUPER SHOT: " + countpower.ToString() + "/" + greentopowerup;
    }

    void SetTurboText()
    {
    }

    void kill() {
        if (!inmortal)
        {
            Time.timeScale = 0;
            Destroy(gameObject);
            endUI.GameOver();
        }
    }

    void shoot(GameObject Project, string tag, bool escalat, int vel) {
        timeshot = 0.3f;
        shotsound.Play();
        GameObject p = Instantiate(Project, transform.position + new Vector3(0.0f, 0.0f, 1.0f), Quaternion.Euler(transform.localEulerAngles));
        ProjectileScript pps = (ProjectileScript)p.GetComponent(typeof(ProjectileScript));
        p.transform.Rotate(new Vector3 (90,0,0));
        if(escalat) p.transform.localScale = new Vector3(0.38035f, 2.96248f, 0.32064f);
        else p.transform.localScale = new Vector3(5, 5, 5);
        pps.speed = vel;
        pps.tago = tag;
        pps.movement = Quaternion.Euler(transform.localEulerAngles)* (new Vector3(0.0f, 0.0f, 1.0f));
        pps.player = gameObject;
        pps.enemykill = enemykill;
        pps.Explosion = Explosion;
    }

    void turboacc()
    {
        if (turbocount > 0)
        {

            turbollum.Play();

            nitro.Play();
            --turbocount;
            turbo = true;
            speed_constant = 40;
            boostUI.Decrease();
            SetTurboText();
        }

        else
        {
            notturbo.Play();
        }

    }
}