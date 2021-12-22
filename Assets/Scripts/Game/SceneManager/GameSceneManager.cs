using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    #region Variables
    [Header ("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject finalPopup;

    [Header ("What is the name of the next scene?")]
    [Space(10)]
    [SerializeField] private string nextScene;

    private int playerHealth;
    #endregion

    void Start()
    {
        playerHealth = player.GetComponent<Player>().currentHealth;
    }

    void Update()
    {
        playerHealth = player.GetComponent<Player>().currentHealth;
        if (playerHealth == 0)
            SceneManager.LoadScene("Death");

        if (finalPopup.activeSelf)
            StartCoroutine(LoadScene(3));
    }

    private IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(nextScene);
    }
}
