using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PB : MonoBehaviour
{

    public float max;
    public Image Msk;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setCurrent(float current)
    {
        Msk.fillAmount = (float)current / (float)max;
    }

    public void setColor (Color32 C) { Msk.color = C; }
}
