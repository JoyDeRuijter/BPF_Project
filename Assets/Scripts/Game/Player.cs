using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private XpDisplay xp;
    public int health, maxHealth, currentXp, currentLevel, xpNeeded, currentMoney;
    public bool isLevelingUp;

    void Start()
    {
        maxHealth = 100;
        health = 100;
        currentXp = 0;
        xpNeeded = 100;
        currentLevel = 1;
        currentMoney = 0;
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
            LevelUp();
        }
    }

    private void LevelUp()
    {
        isLevelingUp = true;
        currentLevel++;
        currentXp -= xpNeeded;
        xpNeeded *= currentLevel;
        
        maxHealth += 20;
        if (health + 25 <= maxHealth)
            health += 25;
        else
            health = maxHealth;
    }
}
