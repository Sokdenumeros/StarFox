using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nucleocontroller : MonoBehaviour
{
    public int win;
    private int count;
    public GameObject terreny;
    public GameObject Explosion;
    public GameObject greenfire;
    public GameObject purplefire;
    public GameObject nau1;
    public GameObject nau2;
    public PB nuclivida;
    // Start is called before the first frame update
    void Start()
    {
        nuclivida.max = win;
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
            nuclivida.setCurrent(win - count);
            if (count >= win)
            {
                Destroy(gameObject);
                
                Quaternion q = Quaternion.identity;
                Instantiate(Explosion, transform.position, q);
                Destroy(terreny);
                Destroy(nau1);
                Destroy(nau2);
                Destroy(greenfire);
                Destroy(purplefire);
            }
        }

    }
}
