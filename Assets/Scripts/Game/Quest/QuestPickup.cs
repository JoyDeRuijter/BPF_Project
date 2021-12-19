using UnityEngine;

public class QuestPickup : BaseQuest
{
    [HideInInspector]
    public bool isPickedUp;

    void Update()
    {
        if (isPickedUp)
            UpdateStatus();       
    }
}
