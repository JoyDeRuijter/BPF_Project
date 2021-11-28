using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private int damage;
    [SerializeField] private float impactForce;

    public GameObject impactEffect;

    void Start()
    {
        health = 100f;
    }

    void Update()
    {
        
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
