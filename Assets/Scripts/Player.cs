using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private GameObject healthbar;

    private float maxWidth, width, onePercent;

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

    void scaleHealthbar() //TODO move this to a new class for the healthbar
    {
        onePercent = maxWidth / 1000;

        if (width > health * 3.5)
            width -= onePercent;

        if (width <= 0)
            width = 0;

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
