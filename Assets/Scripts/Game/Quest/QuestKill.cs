using UnityEngine;

public class QuestKill : BaseQuest
{
    [HideInInspector]
    public bool isKilled;

    void Update()
    {
        if (isKilled)
           UpdateStatus();
    }
}
