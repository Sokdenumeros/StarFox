using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject credits;

    public void loadScene1() {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void loadScene2()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void loadMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void Quit() {
        Application.Quit();
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void Credits() {
        credits.SetActive(true);
        menu.SetActive(false);
    }

    public void BackFromCredits() {
        credits.SetActive(false);
        menu.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        credits.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
