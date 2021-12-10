using UnityEngine.UI;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    private Text moneyCounter;
    public int currentMoney;

    void Start()
    {
        moneyCounter = GameObject.FindGameObjectWithTag("MoneyCounter").GetComponent<Text>();
        currentMoney = 0;
    }

    void Update()
    {
        ShowMoney();
    }

    private void ShowMoney()
    {
        moneyCounter.text = "" + currentMoney;
    }
}
