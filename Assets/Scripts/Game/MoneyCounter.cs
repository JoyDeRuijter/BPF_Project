using UnityEngine.UI;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Text moneyCounter;
    private int currentMoney;

    void Start()
    {
        currentMoney = player.GetComponent<Player>().currentMoney;
        moneyCounter = GameObject.FindGameObjectWithTag("MoneyCounter").GetComponent<Text>();
        currentMoney = 0;
    }

    void Update()
    {
        currentMoney = player.GetComponent<Player>().currentMoney;
        ShowMoney();
    }

    private void ShowMoney()
    {
        moneyCounter.text = "" + currentMoney;
    }
}
