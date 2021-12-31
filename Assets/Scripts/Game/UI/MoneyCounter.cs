using UnityEngine.UI;
using UnityEngine;

public class MoneyCounter : Counter
{
    void Awake()
    {
        counter = GameObject.FindGameObjectWithTag("MoneyCounter").GetComponent<Text>();
    }

    protected override void Update()
    {
        base.Update();
        value = player.GetComponent<Player>().currentMoney;
    }
}
