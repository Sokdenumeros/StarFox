﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectController : MonoBehaviour
{
    public GameObject player;
    public GameObject Projectile;
    private bool estatic;
    private float temps;
    private bool primer = true;
    public float distdisp;
    public bool comença;
    public AudioSource risa;

    // Start is called before the first frame update
    void Start()
    {
        estatic = false;
        temps = 0;
        comença = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.transform.position.z >= distdisp) comença = true;

            if (comença)
            {
                if (estatic)
                {
                    temps += Time.deltaTime;
                    if (primer)
                    {
                        primer = false;
                        transform.localPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 50);
                        Quaternion q = Quaternion.identity;
                        q.SetLookRotation(new Vector3(0, 1, 0), player.transform.position - transform.position);
                        GameObject p = Instantiate(Projectile, transform.position + new Vector3(0.0f, 0.0f, 1.0f), q);


                        ProjectileScript pps = (ProjectileScript)p.GetComponent(typeof(ProjectileScript));

                        //p.transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);

                        pps.speed = 40;
                        pps.movement = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z).normalized;
                        pps.tago = "Enemy_Laser";
                        pps.player = player;
                        pps.enemy = gameObject;
                        pps.risa = risa;
                    }
                    if (temps >= 7)
                    {
                        primer = true;
                        temps = 0;
                        estatic = false;
                    }
                }

                else
                {
                    temps += Time.deltaTime;
                    if (primer)
                    {
                        primer = false;
                        transform.localPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 20);
                    }
                    if (temps >= 7)
                    {
                        primer = true;
                        temps = 0;
                        estatic = true;
                    }
                }

            }
        }
    }
}
