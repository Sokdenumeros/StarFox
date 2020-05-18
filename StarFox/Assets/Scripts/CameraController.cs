using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject insecte;
    private Vector3 offset;
    private bool insbool;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        insbool = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            insbool = true;
        }

       

        if (player != null)
        {
            Vector3 vect = player.transform.position + offset - transform.position;
            float spd = vect.magnitude;
            spd *= spd*spd;
            transform.localPosition += Vector3.Normalize(vect)*spd*Time.deltaTime;
        
            if(insbool)insecte.transform.position += Vector3.Normalize(vect) * spd * Time.deltaTime;
        }
    }
}
