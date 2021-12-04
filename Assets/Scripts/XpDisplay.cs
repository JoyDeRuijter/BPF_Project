using UnityEngine;
using UnityEngine.UI;

public class XpDisplay : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Text xpCounter;

    void Start()
    {
        xpCounter = GameObject.FindGameObjectWithTag("XpCounter").GetComponent<Text>();
    }

    void Update()
    {
        ShowXpLevel();
    }

    private void ShowXpLevel()
    {
        xpCounter.text = "" + player.GetComponent<Player>().currentLevel;
    }
}
