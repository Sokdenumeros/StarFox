using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 vect = player.transform.position + offset - transform.position;
            float spd = vect.magnitude;
            spd *= spd*spd;
            transform.localPosition += Vector3.Normalize(vect)*vect.magnitude;
        }
    }
}
