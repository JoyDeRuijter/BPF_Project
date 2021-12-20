using UnityEngine;
using UnityEngine.UI;

public class XpCounter : Counter
{
    private int secondValue;

    void Awake()
    {
        counter = GameObject.FindGameObjectWithTag("XpCounter").GetComponent<Text>();
    }

    void Update()
    {
        value = player.GetComponent<Player>().currentXp;
        secondValue = player.GetComponent<Player>().xpNeeded;
    }

    public override void DisplayCounter()
    {
        counter.text = "" + value + "/" + secondValue + " XP";
    }
}
