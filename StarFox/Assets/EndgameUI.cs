using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndgameUI : MonoBehaviour
{

    public Image W;
    public Image L;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() {
        this.gameObject.SetActive(true);
        L.gameObject.SetActive(true);
    }

    public void Victory() {
        this.gameObject.SetActive(true);
        W.gameObject.SetActive(true);
    }

    public void Menu() {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
