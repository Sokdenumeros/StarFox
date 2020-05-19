using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotator : MonoBehaviour
{
    public float speed;
    private Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
        rotation = new Vector3(0, speed, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.eulerAngles += rotation * Time.deltaTime;
    }
}
