using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPickup : BaseQuest
{
    public bool isPickedUp;

    void Update()
    {
        if (isPickedUp)
            BountyIsPickedUp();       
    }

    private void BountyIsPickedUp()
    {
        if (qEvent.status != QuestEvent.EventStatus.CURRENT)
            return;

        qEvent.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
        qButton.UpdateButton(QuestEvent.EventStatus.DONE);
        qManager.UpdateQuestOnCompletion(qEvent);
    }
}
