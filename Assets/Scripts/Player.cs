using UnityEngine;

public class Player : MonoBehaviour
{
    public int health, currentXp, currentLevel;
    private int xpNeeded;

    void Start()
    {
        health = 100;
        currentXp = 0;
        xpNeeded = 100;
        currentLevel = 1;
    }

    void Update()
    {
        SetLevel();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
    }

    private void SetLevel()
    {
        if (currentXp >= xpNeeded)
        {
            currentLevel++;
            xpNeeded *= currentLevel;
            currentXp = 0;
        }
    }
}
