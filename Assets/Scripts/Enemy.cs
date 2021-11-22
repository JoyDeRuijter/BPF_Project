using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    
    void Start()
    {
        health = 100f;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
