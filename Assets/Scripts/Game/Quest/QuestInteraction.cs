using UnityEngine;

public class QuestInteraction : BaseQuest
{
    [HideInInspector]
    public bool isInteracting;

    void Update()
    {
        if (isInteracting)
            UpdateStatus();
    }
}
