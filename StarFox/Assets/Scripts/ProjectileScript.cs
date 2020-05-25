using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public int damage;
    public float temps;
    public int speed;
    public GameObject player;
    public GameObject enemy;
    public Vector3 movement;
    public string tago;
    public AudioSource enemykill;
    public GameObject Explosion;



    void Start()
    {
        temps = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tago != null)
        {
            transform.gameObject.tag = tago;

            if (tago == "Enemy_Laser")
            {
                //transform.localScale = new Vector3(Time.deltaTime*2, Time.deltaTime*2, Time.deltaTime*2);
                Vector3 targetDir = player.transform.position - transform.position;
                float step = speed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
                transform.rotation = Quaternion.LookRotation(newDir);

                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

                temps += Time.deltaTime;

                if (temps >= 5)
                {
                    temps = 0;
                    Destroy(gameObject);
                }
            }

            else if (tago == "purpleproj" || tago == "blueproj")
            {
                Vector3 targetDir = player.transform.position - transform.position;
                float step = speed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
                transform.rotation = Quaternion.LookRotation(newDir);

                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

                temps += Time.deltaTime;

                if (temps >= 8)
                {
                    temps = 0;
                    Destroy(gameObject);
                }

            }

            else
            {

                transform.gameObject.tag = tago;
                transform.localPosition += movement * speed * Time.deltaTime;
            }

            if (player != null && (player.transform.position - gameObject.transform.position).magnitude > 300) Destroy(gameObject);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("barrera"))
        {

            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
            Destroy(other.gameObject);
            
        }

        if (tago == "Projectile" && other.gameObject.CompareTag("Enemy"))
        {

            enemykill.Play();

            Instantiate(Explosion, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            
            Destroy(gameObject);

        }

        if (tago == "Projectile" && other.gameObject.CompareTag("Enemymover"))
        {


            Instantiate(Explosion, other.gameObject.transform.position, Quaternion.identity);

        }

        if (other.gameObject.CompareTag("obstacle"))
        {

            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }

    }
}
