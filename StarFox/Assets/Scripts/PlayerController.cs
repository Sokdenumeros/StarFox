using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float speed_constant;
    private int count;
    public Text countText;
    public Text winText;

    void Start()
    {

        count = 2;
        SetCountText();
        winText.text = "";

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        Vector3 constant = new Vector3(0.0f, 0.0f, 1);

        transform.eulerAngles = new Vector3(-moveVertical * 20, 0.0f,-moveHorizontal*20);


        movementController(movement, constant);
        RotationController();

        

    }

    void ClampPosition() //funcio per a fer que hi hagin limits invisibles a les vores
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

    void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.collider.gameObject);
        --count;
        SetCountText();
        
    }

    void SetCountText()
    {
        countText.text = "Lifes: " + count.ToString();
        if (count == 0)
        {
            Destroy(gameObject);
            winText.text = "YOU LOST";
        }
        
    }


}