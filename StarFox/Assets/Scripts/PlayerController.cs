using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float speed_constant;
    public Text countText;
    public Text winText;
    public GameObject Projectile;
    private int health;
    private bool turbo;
    private float turbotimer;
    private int turbocount;
    public Text turboText;
    private bool barrelroll;
    private int rotation;


    void Start()
    {
        health = 100;
        SetCountText();
        winText.text = "";
        turbo = false;
        barrelroll = false;
        rotation = 0;
        turbotimer = 0;
        turbocount = 3;
        SetTurboText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) shoot();
        if (Input.GetKeyDown(KeyCode.N)) turboacc();
        if (Input.GetKeyDown(KeyCode.B)) barrelroll = true;

        if(turbo)
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
            

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float mhabs = moveHorizontal;
        if (mhabs < 0) mhabs = -mhabs;
        transform.localEulerAngles = new Vector3(-moveVertical * 20 -mhabs*20, 0.0f, -moveHorizontal * 20);

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        Vector3 constant = new Vector3(0.0f, 0.0f, 1);

        movementController(movement, constant);
    }

    void ClampPosition() //funcio per a fer que hi hagin limits invisibles a les vores
    {
        //Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        //pos.x = Mathf.Clamp01(pos.x);
        //pos.y = Mathf.Clamp01(pos.y);
       // transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void movementController(Vector3 movement, Vector3 constant)
    {
        transform.localPosition += movement * speed * Time.deltaTime;
        transform.localPosition += constant * speed_constant * Time.deltaTime;
        ClampPosition();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy_Projectile"))
        {

            damage(10);
            Destroy(other.gameObject);
            SetCountText();
        }

        if (other.gameObject.CompareTag("Pilar") || other.gameObject.CompareTag("Enemy"))
        {
            kill();
        }
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
        Destroy(gameObject);
        winText.text = "YOU LOST";
    }

    void shoot() {
        GameObject p = Instantiate(Projectile, transform.position+ new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity);
        ProjectileScript pps = (ProjectileScript)p.GetComponent(typeof(ProjectileScript));
        p.transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
        p.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        pps.speed = 100;
        pps.tago = "Projectile";
        pps.movement = new Vector3(0.0f, 0.0f, 1.0f);
        pps.player = gameObject;
    }

    void turboacc()
    {
        if (turbocount > 0)
        {
            --turbocount;
            turbo = true;
            speed_constant = 20;
            SetTurboText();
        }

    }
}