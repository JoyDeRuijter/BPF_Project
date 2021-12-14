using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class XpDisplay : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Text levelCounter, xpCounter;
    private GameObject levelUp, xpCounterGO;

    void Start()
    {
        levelCounter = GameObject.FindGameObjectWithTag("LevelCounter").GetComponent<Text>();
        xpCounterGO = GameObject.FindGameObjectWithTag("XpCounter");
        xpCounter = xpCounterGO.GetComponent<Text>();
        levelUp = GameObject.FindGameObjectWithTag("LevelUp");
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

    private IEnumerator ShowLevelUp()
    {
        xpCounterGO.SetActive(false);
        levelUp.SetActive(true);
        yield return new WaitForSeconds(2);
        levelUp.SetActive(false);
        xpCounterGO.SetActive(true);
    }

    public void XpPopUp()
    {
        StartCoroutine(ShowLevelUp());
    }
}
