using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region Variables
    [Header("Gameobject References")]
    [SerializeField] private GameObject healthbar;
    [SerializeField] private GameObject healthCross;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject maxHealthDisplay;
    
    private int health, maxHealth;
    private float maxWidth, width, height, fourPermille;
    private Color green, yellow, orange, red;
    #endregion

    void Awake()
    {
        health = player.GetComponent<Player>().currentHealth;
        maxHealth = player.GetComponent<Player>().currentHealth;
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
        health = player.GetComponent<Player>().currentHealth;
        maxHealth = player.GetComponent<Player>().maxHealth;
        ScaleHealthbar();
        ColorHealthbar();
        DisplayMaxHealth();
    }

    private void ScaleHealthbar() 
    {
        //Check if the health is different from the width that it's supposed to be 
        //If it is different, slowly change the width by adding or substracting
        fourPermille = maxWidth / 250;

        if (width > health * (maxWidth/100))
            width -= fourPermille;

        if (health * (maxWidth / 100) > width)
            width += fourPermille;

        if (width <= 0)
            width = 0;

        if (width >= maxWidth)
            width = maxWidth;

        healthbar.GetComponent<RectTransform>().localScale = new Vector3(width, height, 1);
    }

    private void ColorHealthbar()
    {
        //Change the healthbar's color depending on the percentage of health left
        if (health > (maxHealth / 2))
        {
            healthbar.GetComponent<Image>().color = green;
            healthCross.GetComponent<RawImage>().color = green;
        }
        else if (health > (maxHealth / 4) && health <= (maxHealth / 2))
        {
            healthbar.GetComponent<Image>().color = yellow;
            healthCross.GetComponent<RawImage>().color = yellow;
        }
        else if (health <= (maxHealth / 4) && health > (maxHealth / 10))
        {
            healthbar.GetComponent<Image>().color = orange;
            healthCross.GetComponent<RawImage>().color = orange;
        }
        else if (health <= (maxHealth / 10))
        {
            healthbar.GetComponent<Image>().color = red;
            healthCross.GetComponent<RawImage>().color = red;
        }
    }

    private void DisplayMaxHealth()
    {
        maxHealthDisplay.GetComponent<Text>().text = "MAX: " + player.GetComponent<Player>().maxHealth;
    }
}
