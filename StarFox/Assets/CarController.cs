using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject player;
    private int rotation;
    private float temps;
    public Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        rotation = 0;
        temps = 0;

    }

    // Update is called once per frame
    void Update()
    {

            Vector3 targetDir = movement - transform.position;
            float step = 40 * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);

            transform.position = Vector3.MoveTowards(transform.position, movement, step);

            temps += Time.deltaTime;

            if (temps >= 40)
            {
                temps = 0;
                Destroy(gameObject);
            }
        

        /*transform.localPosition += movement * 100 * Time.deltaTime; */

        transform.localEulerAngles = new Vector3(-rotation, -rotation, -rotation);
        rotation = rotation + 5;
    }
}
