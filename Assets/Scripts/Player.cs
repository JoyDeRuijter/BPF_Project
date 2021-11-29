using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;

    void Start()
    {
        health = 100;
    }

    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
    }
}
