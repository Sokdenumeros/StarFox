using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyController : MonoBehaviour
{
    public int speed;
    public int speed_constant;
    private Vector3 movement_constant;
    private Vector3 movement;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        movement_constant = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        transform.localPosition += movement * speed * Time.deltaTime;
        transform.localPosition += movement_constant * speed_constant * Time.deltaTime;
    }
}
