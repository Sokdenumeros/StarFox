using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapcontroller : MonoBehaviour
{
    public GameObject player;
    public int trapdist;
    public AudioSource caiguda;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && player.transform.position.z >= trapdist) {
            Destroy(gameObject);
        }
    }
}
