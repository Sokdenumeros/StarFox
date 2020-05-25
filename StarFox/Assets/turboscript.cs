using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turboscript : MonoBehaviour
{ 

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (player != null) transform.position = player.transform.position + new Vector3(0-0f,0.0f,0.9902f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) transform.position = player.transform.position + new Vector3(0 - 0f, 0.0f, 0.9902f);
    }
}
