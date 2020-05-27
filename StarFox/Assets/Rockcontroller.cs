using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockcontroller : MonoBehaviour
{
    private bool impacte;
    public AudioSource impact;
    // Start is called before the first frame update
    void Start()
    {
        impacte = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("terrain"))
        {
            if (impacte == false)
            {
                impact.Play();
                impacte = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (impacte == false)
        {
            if(impact != null) impact.Play();
            impacte = true;
        }
    }
}
