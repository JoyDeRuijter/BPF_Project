using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestKill : BaseQuest
{
    public bool isKilled;

    void Update()
    {
        if (isKilled)
            WantedNPCIsKilled();
    }

    private void WantedNPCIsKilled()
    {
        if (qEvent.status != QuestEvent.EventStatus.CURRENT)
            return;

        qEvent.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
        qButton.UpdateButton(QuestEvent.EventStatus.DONE);
        qManager.UpdateQuestOnCompletion(qEvent);
    }
}
