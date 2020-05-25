using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject player;
    private int rotation;
    private float temps;
    // Start is called before the first frame update
    void Start()
    {
        rotation = 0;
        temps = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {

            Vector3 targetDir = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) - transform.position;
            float step = 20 * Time.deltaTime;
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

        /*transform.localPosition += movement * 100 * Time.deltaTime;
        transform.localEulerAngles = new Vector3(-rotation, -rotation, -rotation);
        rotation = rotation + 5;*/
    }
}
