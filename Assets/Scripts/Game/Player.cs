using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private XpDisplay xp;
    public int health, maxHealth, currentXp, currentLevel, xpNeeded;
    public bool isLevelingUp;

    void Start()
    {
        maxHealth = 100;
        health = 100;
        currentXp = 0;
        xpNeeded = 100;
        currentLevel = 1;
        isLevelingUp = false;
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
        //Debug.Log("Player died");
    }

    private void SetLevel()
    {
        if (currentXp >= xpNeeded)
        {
            isLevelingUp = true;
            currentLevel++;
            xpNeeded *= currentLevel;
            currentXp = 0;
            maxHealth += 20;

            if (health + 20 <= maxHealth)
                health += 20;
            else
                health = maxHealth;
        }
    }
}
