using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
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
    public Text turboText;
    public Text powerText;
    private bool barrelroll;
    private int rotation;
    public AudioSource shotsound;
    public AudioSource enemykill;
    public AudioSource damaged;
    public AudioSource spin;
    public AudioSource nitro;
    public AudioSource notturbo;
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

    void Start()
    {
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


    }

    void Update()
    {

       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (countpower == greentopowerup)
            {
                shoot(Projectile_power, "Projectile_power", false, 27);
                countpower = 0;
                SetPowerText();
            }
            else shoot(Projectile, "Projectile", true, 100);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            turboacc();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            
            if (time == 0)
            {
                barrelroll = true;
                spin.Play();
                time += Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && inmortal)
        {
            if (hackfast == false)
            {
                speed_constant = 40;
                hackfast = true;
            }
            else
            {
                speed_constant = 15;
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
        }



        
        SetBarrelText();

        if (turbo)
        {
            turbotimer += Time.deltaTime;
            if(turbotimer >= 1)
            {
                speed_constant = 10;
                turbotimer = 0;
                turbo = false;
            }
        }

        if(barrelroll)
        {
            transform.localEulerAngles= new Vector3(0.0f, 0.0f, -rotation);
            rotation = rotation + 5;
            if (rotation == 360)
            {
                rotation = 0;
                barrelroll = false;
            }

        }

        if(relantitzat)
        {
            tempsrel += Time.deltaTime;
            speed = speed_rel;
            if (tempsrel >= 5)
            {
                tempsrel = 0;
                speed = 15;
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
           // Vector3 vect = transform.position + offset - camera.transform.position;
            //float spd = vect.magnitude;
            //spd *= spd * spd;
            //insect.transform.localPosition += Vector3.Normalize(vect) * spd * Time.deltaTime;
            // insect.transform.localPosition = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z - 4);

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
        transform.localEulerAngles = new Vector3(-moveVertical * 20 , moveHorizontal * 10, -moveHorizontal * 20);

    
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

    void ClampPosition() //funcio per a fer que hi hagin limits invisibles a les vores
    {
        //Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        //pos.x = Mathf.Clamp01(pos.x);
        //pos.y = Mathf.Clamp01(pos.y);
       // transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void movementController()
    {
        if(barrelroll)
        {
            rb.velocity = (movement * speedbarrel) + (new Vector3(0.0f, 0.0f, speed_constant));
        }
        else rb.velocity = (movement * speed) + (new Vector3(0.0f, 0.0f, speed_constant));
        ClampPosition();
        
    }

    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Enemy_Projectile"))
        {
            if (barrelroll == false)
            {
                damage(10);
                Destroy(other.gameObject);
                SetCountText();
                damaged.Play();
            }
        }

        else if (other.gameObject.CompareTag("Pilar") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Enemymover"))
        {
            kill();
        }

        if (other.gameObject.CompareTag("Enemy_Laser")) relantitzat = true;

        if (other.gameObject.CompareTag("purpleproj")) controlsinvertits = true;

        if(other.gameObject.CompareTag("blueproj")) visionula = true;

        if (other.gameObject.CompareTag("greenproj"))
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
        if (health == 0) kill();
        
    }

    void SetBarrelText()
    {
        if (time > 0)
        {
            time += Time.deltaTime;
            barrelR.setCurrent(time);
        }
        if (time >= 5)
        {
            barrelR.setCurrent(5);
            time = 0;
        }
    }

    void SetPowerText()
    {
        if (countpower == greentopowerup) powerText.text = "SUPER SHOT READY";
        else powerText.text = "Power to super shot: " + countpower.ToString() + "/" + greentopowerup;
    }

    void SetTurboText()
    {
        turboText.text = "TURBOS: " + turbocount.ToString();
    }

    void kill() {
        if (!inmortal)
        {
            Destroy(gameObject);
            winText.text = "YOU LOST";
        }
    }

    void shoot(GameObject Project, string tag, bool escalat, int vel) {
        shotsound.Play();
        GameObject p = Instantiate(Project, transform.position+ new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity);
        ProjectileScript pps = (ProjectileScript)p.GetComponent(typeof(ProjectileScript));
        p.transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
        if(escalat) p.transform.localScale = new Vector3(0.38035f, 2.96248f, 0.32064f);
        else p.transform.localScale = new Vector3(5, 5, 5);
        pps.speed = vel;
        pps.tago = tag;
        pps.movement = new Vector3(0.0f, 0.0f, 1.0f);
        pps.player = gameObject;
        pps.enemykill = enemykill;
    }

    void turboacc()
    {
        if (turbocount > 0)
        {
            nitro.Play();
            --turbocount;
            turbo = true;
            speed_constant = 27;
            SetTurboText();
        }

        else
        {
            notturbo.Play();
        }

    }
}