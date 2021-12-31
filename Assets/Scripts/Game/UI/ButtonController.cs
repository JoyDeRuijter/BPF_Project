using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private AudioSource sound;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    public void QuitGame()
    {
        StartCoroutine(WaitForScene(0.5f));
        Debug.Log("Quit game");
        Application.Quit();
    }

    public void PlayScene(string scene)
    {
        StartCoroutine(WaitForScene(0.5f));
        SceneManager.LoadScene(scene);
    }

    private IEnumerator WaitForScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void PlayClickSound()
    {
        sound.Play();
    }
}   

