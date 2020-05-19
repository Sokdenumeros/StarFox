using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public void hoverAction() {
        transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
    }

    public void UnhoverAction()
    {
        transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
    }
}
