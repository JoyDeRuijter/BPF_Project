using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private int playerHealth;

    void Start()
    {
        playerHealth = player.GetComponent<Player>().currentHealth;
    }

    void Update()
    {
        playerHealth = player.GetComponent<Player>().currentHealth;
        if (playerHealth == 0)
            SceneManager.LoadScene("Death");
    }
}
