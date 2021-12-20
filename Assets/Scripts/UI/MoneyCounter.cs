using UnityEngine.UI;
using UnityEngine;

public class MoneyCounter : Counter
{
    void Awake()
    {
        counter = GameObject.FindGameObjectWithTag("MoneyCounter").GetComponent<Text>();
    }

    void Update()
    {
        value = player.GetComponent<Player>().currentMoney;
    }
}
