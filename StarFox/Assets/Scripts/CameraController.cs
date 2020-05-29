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
        if (player != null)
        {
            float moveVertical = Input.GetAxis("Vertical");
            float moveHorizontal = Input.GetAxis("Horizontal");

            Vector3 vect = player.transform.position + offset - transform.position;
            float spd = vect.magnitude;
            spd *= spd*spd;
            transform.localPosition += Vector3.Normalize(vect)*spd*Time.deltaTime;
        
            Vector3 target = new Vector3(-moveVertical * 5, moveHorizontal * 5, -moveHorizontal * 5);
            Vector3 rot = target - transform.localEulerAngles;
            if (rot.x > 180) rot.x -= 360;
            if (rot.y > 180) rot.y -= 360;
            if (rot.z > 180) rot.z -= 360;
            if (rot.x < -180) rot.x += 360;
            if (rot.y < -180) rot.y += 360;
            if (rot.z < -180) rot.z += 360;
            transform.localEulerAngles += Vector3.Normalize(rot) * rot.magnitude/5;
        }
    }
}
