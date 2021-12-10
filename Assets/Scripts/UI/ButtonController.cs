using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

    public void NextScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}   

