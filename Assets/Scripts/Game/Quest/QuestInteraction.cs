using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInteraction : BaseQuest
{
    public bool isInteracting;

    void Update()
    {
        if (isInteracting)
            InteractedWithNPC();
    }

    private void InteractedWithNPC()
    {
        if (qEvent.status != QuestEvent.EventStatus.CURRENT)
            return;

        qEvent.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
        qButton.UpdateButton(QuestEvent.EventStatus.DONE);
        qManager.UpdateQuestOnCompletion(qEvent);
    }
}
