using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float speed_constant;
    private bool dontcrossright;
    private bool dontcrosstop;
    private bool dontcrossbot;
    private bool dontcrossleft;
    public Text countText;
    public Text winText;
    public GameObject Projectile;
    private int health;
    private bool turbo;
    private bool hackfast;
    private float turbotimer;
    private float tempsrel;
    private int turbocount;
    public Text turboText;
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
    private Vector3 movement;
    private float moveHorizontal;
    private float moveVertical;
    public Rigidbody rb;

    void Start()
    {
        movement = new Vector3(0, 0, 0);
        health = 100;
        SetCountText();
        winText.text = "";
        turbo = false;
        barrelroll = false;
        rotation = 0;
        turbotimer = 0;
        turbocount = 3;
        tempsrel = 0;
        SetTurboText();
        inmortal = false;
        relantitzat = false;
        dontcrossright = false;
        dontcrosstop = false;
        dontcrossbot = false;
        dontcrossleft = false;


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            turboacc();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            spin.Play();
            barrelroll = true;
        }

        if (Input.GetKeyDown(KeyCode.F) && inmortal)
        {
            if (hackfast == false)
            {
                speed_constant = 30;
                hackfast = true;
            }
            else
            {
                speed_constant = 3;
                hackfast = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            inmortal = !inmortal;
            if(inmortal == true)
            {
                countText.text = "GOD MODE";
            }
            else
            {
                SetCountText();
            }
        }

        if (turbo)
        {
            turbotimer += Time.deltaTime;
            if(turbotimer >= 0.5)
            {
                speed_constant = 3;
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
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
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
        rb.velocity = (movement * speed) + (new Vector3(0.0f, 0.0f, speed_constant));
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
        countText.text = "HP: " + health.ToString();
        if (health == 0) kill();
        
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

    void shoot() {
        shotsound.Play();
        GameObject p = Instantiate(Projectile, transform.position+ new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity);
        ProjectileScript pps = (ProjectileScript)p.GetComponent(typeof(ProjectileScript));
        p.transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
        p.transform.localScale = new Vector3(0.38035f, 2.96248f, 0.32064f);
        pps.speed = 100;
        pps.tago = "Projectile";
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
            speed_constant = 20;
            SetTurboText();
        }

        else
        {
            notturbo.Play();
        }

    }
}