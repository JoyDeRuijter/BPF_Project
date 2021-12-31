using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [Header("Player Attributes")]
    public int currentHealth;
    public int currentXp;
    public int currentMoney;
    
    [HideInInspector]
    public int xpNeeded, currentLevel, maxHealth;
    [HideInInspector]
    public bool isLevelingUp;
    #endregion

    void Awake()
    {
        maxHealth = 100;
        currentHealth = 100;
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
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            currentHealth = 0;
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
        if (currentHealth + 25 <= maxHealth)
            currentHealth += 25;
        else
            currentHealth = maxHealth;
    }
}
