using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public int damage;
    public int speed;
    public GameObject player;
    public Vector3 movement;
    public string tago;


    // Update is called once per frame
    void Update()
    {
        transform.gameObject.tag = tago;
        transform.localPosition += movement * speed * Time.deltaTime;
         if ((player.transform.position - gameObject.transform.position).magnitude > 300) Destroy(gameObject);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pilar"))
        {

            
            Destroy(gameObject);
        }

        else if(tago == "Projectile" && other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
