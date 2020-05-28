using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostUI : MonoBehaviour
{
    public Image N3;
    public Image N2;
    public Image N1;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Decrease() {
        --count;
        if (count == 2) N3.gameObject.SetActive(false);
        else if (count == 1) N2.gameObject.SetActive(false);
        else N1.gameObject.SetActive(false);
    }
}
