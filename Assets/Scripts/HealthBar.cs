using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthbar;
    [SerializeField] private GameObject player;
    
    private int health;
    private float maxWidth, width, onePermille;

    void Start()
    {
        health = player.GetComponent<Player>().health;
        maxWidth = healthbar.GetComponent<RectTransform>().localScale.x;
        width = maxWidth;
    }

    void Update()
    {
        health = player.GetComponent<Player>().health;
        scaleHealthbar();
    }

    void scaleHealthbar() 
    {
        onePermille = maxWidth / 1000;

        if (width > health * 3.5)
            width -= onePermille;

        if (health * 3.5 > width)
            width += onePermille;

        if (width <= 0)
            width = 0;

        if (width >= maxWidth)
            width = maxWidth;

        healthbar.GetComponent<RectTransform>().localScale = new Vector3(width, 50, 1);
    }
}
