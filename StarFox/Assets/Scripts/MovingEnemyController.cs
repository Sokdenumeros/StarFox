using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyController : MonoBehaviour
{
    public int speed;
    public int speedkamikaze;
    public int videsenemic;
    private int count;
    private bool kamikaze;
    public int speed_constant;
    private Vector3 movement_constant;
    private Vector3 movement;
    public GameObject player;
    public float distdisp;
    // Start is called before the first frame update
    void Start()
    {
        kamikaze = false;
        count = 0;
        movement_constant = new Vector3(0, 0, 1);
        movement = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player != null && player.transform.position.z >= distdisp)
            {
                if (kamikaze)
                {
                    transform.localPosition += movement_constant * speedkamikaze * Time.deltaTime;
                }


                else if (count > videsenemic)
                {
                    if (transform.localPosition.x > player.transform.position.x) movement = new Vector3(-1, 0, 0);
                    else if (transform.localPosition.x < player.transform.position.x) movement = new Vector3(1, 0, 0);
                    kamikaze = true;
                    movement_constant = new Vector3(0, 0, -1);

                    transform.localPosition += movement * speed * Time.deltaTime;
                    transform.localPosition += movement_constant * speed_constant * Time.deltaTime;

                }
                else
                {
                    transform.localPosition += movement * speed * Time.deltaTime;
                    if (transform.localPosition.x > player.transform.position.x + 20) movement = new Vector3(-1, 0, 0);
                    else if (transform.localPosition.x < player.transform.position.x - 20) movement = new Vector3(1, 0, 0);
                    transform.localPosition += movement_constant * speed_constant * Time.deltaTime;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.CompareTag("Projectile"))
            {
            count = count + 1;
            }
        
    }
}
