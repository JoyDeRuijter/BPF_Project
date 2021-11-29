using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    private Text moneyCounter;

    public int currentMoney;

    // Start is called before the first frame update
    void Start()
    {
        moneyCounter = GameObject.FindGameObjectWithTag("MoneyCounter").GetComponent<Text>();
        currentMoney = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ShowMoney();
    }

    private void ShowMoney()
    {
        moneyCounter.text = "" + currentMoney;
    }
}
