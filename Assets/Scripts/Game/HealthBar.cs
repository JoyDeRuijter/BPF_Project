using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthbar;
    [SerializeField] private GameObject player;
    
    private int health, maxHealth;
    private float maxWidth, width, height, twoPermille;
    Color green, yellow, orange, red;

    void Start()
    {
        health = player.GetComponent<Player>().health;
        maxHealth = player.GetComponent<Player>().health;
        maxWidth = healthbar.GetComponent<RectTransform>().localScale.x;
        height = healthbar.GetComponent<RectTransform>().localScale.y;
        width = maxWidth;
        green = healthbar.GetComponent<Image>().color;
        yellow = new Color32(255, 222, 1, 255);
        orange = new Color32(255, 120, 1, 255);
        red = new Color32(225, 7, 7, 255);
    }

    void Update()
    {
        health = player.GetComponent<Player>().health;
        maxHealth = player.GetComponent<Player>().health;
        scaleHealthbar();
        colorHealthbar();
    }

    void scaleHealthbar() 
    {
        twoPermille = maxWidth / 500;

        if (width > health * (maxWidth/100))
            width -= twoPermille;

        if (health * (maxWidth / 100) > width)
            width += twoPermille;

        if (width <= 0)
            width = 0;

        if (width >= maxWidth)
            width = maxWidth;

        healthbar.GetComponent<RectTransform>().localScale = new Vector3(width, height, 1);
    }

    void colorHealthbar()
    {
        if (health > (maxHealth / 2))
        {
            healthbar.GetComponent<Image>().color = green;
        }
        else if (health > (maxHealth / 4) && health <= (maxHealth / 2))
        {
            healthbar.GetComponent<Image>().color = yellow;
        }
        else if (health <= (maxHealth / 4) && health > (maxHealth / 10))
        {
            healthbar.GetComponent<Image>().color = orange;
        }
        else if (health <= (maxHealth / 10))
        {
            healthbar.GetComponent<Image>().color = red;
        }
    }
}
