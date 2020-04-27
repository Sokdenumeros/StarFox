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

    void Start()
    {
        health = 100;
        SetCountText();
        winText.text = "";
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) shoot();
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
        RotationController();
    }

    void ClampPosition() //funcio per a fer que hi hagin limits invisibles a les vores
    {
        /*Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);*/
    }

    void RotationController()
    {
        
    }

    void movementController(Vector3 movement, Vector3 constant)
    {
        transform.localPosition += movement * speed * Time.deltaTime;
        transform.localPosition += constant * speed_constant * Time.deltaTime;
        ClampPosition();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy_Projectile")) {
            damage(10);
            Destroy(collision.collider.gameObject);
            SetCountText();
        }
        else kill();
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
        pps.player = gameObject;
    }
}