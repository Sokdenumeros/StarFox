using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public int damage;
    public int speed;
    public GameObject player;


    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(0.0f, 0.0f, 1.0f);
        transform.localPosition += movement * speed * Time.deltaTime;
        if((player.transform.position - gameObject.transform.position).magnitude > 300) Destroy(gameObject);
    }
}
