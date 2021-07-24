using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject creditsMenu;

    public bool creditsOpen = false;  

    public void Play()
    {
        GameManager.instance.healthBar.SetActive(true);
        SceneManager.LoadScene("BasicDungeon");
    }

    public void Credits()
    {
        if(creditsOpen == false)
        {
            startMenu.SetActive(false);
            creditsMenu.SetActive(true);
            StartCoroutine(Wait1());
        }
        if (creditsOpen == true)
        {
            startMenu.SetActive(true);
            creditsMenu.SetActive(false);
            StartCoroutine(Wait2());
        }
    }

    public void Quit()
    {
        Application.Quit();
        print("quit");
    }

    IEnumerator Wait1()
    {
        yield return new WaitForSeconds(1f);
        creditsOpen = true;
    }

    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(1f);
        creditsOpen = false;
    }
}
