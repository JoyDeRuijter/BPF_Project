using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;
    private float maxWidth;
    private float width;
    private float onePercent;
    private float tenPercent;
    public GameObject healthbar;
   

    void Start()
    {
        health = 100;
        maxWidth = healthbar.GetComponent<RectTransform>().localScale.x;
        width = maxWidth;
    }

    void Update()
    {
        scaleHealthbar();
    }

    void scaleHealthbar()
    {
        onePercent = maxWidth / 1000;
        tenPercent = onePercent * 10;

        if (width > health * 3.5)
            width -= onePercent;

        if (width <= 0)
        {
            width = 0;
            //Debug.Log("health is 0");
        }
        healthbar.GetComponent<RectTransform>().localScale = new Vector3(width, 50, 1);
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
