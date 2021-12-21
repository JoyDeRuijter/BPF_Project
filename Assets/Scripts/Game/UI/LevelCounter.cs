using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : Counter
{
    void Awake()
    {
        counter = GameObject.FindGameObjectWithTag("LevelCounter").GetComponent<Text>();
    }

    protected override void Update()
    {
        base.Update();
        value = player.GetComponent<Player>().currentLevel;
    }
}
