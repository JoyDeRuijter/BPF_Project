using UnityEngine;
using UnityEngine.UI;

public class XpDisplay : Counter
{
    private Text levelCounter, xpCounter;

    void Start()
    {
        levelCounter = GameObject.FindGameObjectWithTag("LevelCounter").GetComponent<Text>();
        xpCounter = GameObject.FindGameObjectWithTag("XpCounter").GetComponent<Text>();
    }

    void Update()
    {
        ShowXpLevel();
        ShowXpCount();
    }

    private void ShowXpLevel()
    {
        levelCounter.text = "" + player.GetComponent<Player>().currentLevel;
    }

    private void ShowXpCount()
    {
        xpCounter.text = "" + player.GetComponent<Player>().currentXp + "/" + player.GetComponent<Player>().xpNeeded + " XP";
    }
}
