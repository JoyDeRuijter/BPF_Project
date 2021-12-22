using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private GameObject player;

    public void TakeDamage(float amount)
    {
        Debug.Log("you hit the dummy");
        health -= amount;
        if (health <= 0f)
            Die();
    }

    private void Die()
    {
        player.GetComponent<Player>().currentXp += 100;
        GameObject.FindGameObjectWithTag("KillManager").GetComponent<QuestKill>().isKilled = true;
        Destroy(gameObject);
    }

}
