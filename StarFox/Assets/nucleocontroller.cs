using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nucleocontroller : MonoBehaviour
{
    public int win;
    private int count;
    public GameObject terreny;
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Projectile"))
        {
            ++count;

            if (count >= win)
            {
                Destroy(gameObject);
                
                Quaternion q = Quaternion.identity;
                Instantiate(Explosion, transform.position, q);
                Destroy(terreny);
            }
        }

    }
}
