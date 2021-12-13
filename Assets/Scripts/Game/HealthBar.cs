using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthbar;
    [SerializeField] private GameObject player;
    
    private int health;
    private float maxWidth, width, height, twoPermille;

    void Start()
    {
        health = player.GetComponent<Player>().health;
        maxWidth = healthbar.GetComponent<RectTransform>().localScale.x;
        height = healthbar.GetComponent<RectTransform>().localScale.y;
        width = maxWidth;
    }

    void Update()
    {
        health = player.GetComponent<Player>().health;
        scaleHealthbar();
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
}
