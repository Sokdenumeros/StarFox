using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float speed_constant;

    void Start()
    {
     
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        Vector3 constant = new Vector3(0.0f, 0.0f, 1);

        movementController(movement, constant);
        RotationController();

        

    }

    void ClampPosition()
    {

        /*Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);*/

    }

    void RotationController()
    {

 

    }

    void movementController(Vector3 movement, Vector3 constant)
    {

        transform.localPosition += movement * speed * Time.deltaTime;
        transform.localPosition += constant * speed_constant * Time.deltaTime;
        ClampPosition();

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Pilar"))
        {
            other.gameObject.SetActive(false);
        }

    }


}