using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneboy : MonoBehaviour
{
    // Start is called before the first frame update

    public void ToMainMenu()
    {
        Debug.Log("To Main Menu");
        SceneManager.LoadScene("Menu");
    }

    public void ToGameScreen()
    {
        Debug.Log("To Game Screen");
        SceneManager.LoadScene("SampleScene");
    }

    public void ToAboutScreen()
    {
        Debug.Log("To About Screen");
        SceneManager.LoadScene("About");
    }

    public void ToCreditsScene()
    {
        Debug.Log("To Credits Screen");
        SceneManager.LoadScene("Credits");
    }

    public void ToAboutTwo()
    {
        Debug.Log("To About 2");
        SceneManager.LoadScene("About 2");
    }
    public void ToAboutThree()
    {
        Debug.Log("To About 3");
        SceneManager.LoadScene("About 3");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
