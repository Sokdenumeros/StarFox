using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerController : MonoBehaviour
{
    private float timeshot;
    public GameObject Projectile;
    public AudioSource shotsound;
    public GameObject Explosion;
    public AudioSource enemykill;
    private bool barrelroll;
    private int rotation;
    private int brInc;
    // Start is called before the first frame update
    void Start()
    {
        barrelroll = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) shoot(Projectile, "Projectile", true, 250);
        if (Input.GetKey(KeyCode.Space)) if (timeshot <= 0.0f) shoot(Projectile, "Projectile", true, 250);
        if (Input.GetKeyDown(KeyCode.B)) {
            if (!barrelroll && Input.GetAxis("Horizontal") != 0) barrelroll = true;
            if (barrelroll && Input.GetAxis("Horizontal") > 0) brInc = 2;
            else if (barrelroll && Input.GetAxis("Horizontal") < 0) brInc = -2;
        }
        if (barrelroll) {
            rotation += brInc;
            if (rotation == brInc*180) { barrelroll = false; rotation = 0; }
            else transform.localEulerAngles = new Vector3(0.0f, 0.0f, -rotation);
        }
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        transform.localEulerAngles = new Vector3(-moveVertical * 45, moveHorizontal * 45, -moveHorizontal * 45);
    }

    void shoot(GameObject Project, string tag, bool escalat, int vel)
    {
        timeshot = 0.3f;
        shotsound.Play();
        GameObject p = Instantiate(Project, transform.position + new Vector3(0.0f, 0.0f, 1.0f), Quaternion.Euler(transform.localEulerAngles));
        ProjectileScript pps = (ProjectileScript)p.GetComponent(typeof(ProjectileScript));
        p.transform.Rotate(new Vector3(90, 0, 0));
        if (escalat) p.transform.localScale = new Vector3(0.38035f, 2.96248f, 0.32064f);
        else p.transform.localScale = new Vector3(5, 5, 5);
        pps.speed = vel;
        pps.tago = tag;
        pps.movement = Quaternion.Euler(transform.localEulerAngles) * (new Vector3(0.0f, 0.0f, 1.0f));
        pps.player = gameObject;
        pps.enemykill = enemykill;
        pps.Explosion = Explosion;
    }
}
