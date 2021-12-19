using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocation : BaseQuest
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Player") 
            return;

        if (qEvent.status != QuestEvent.EventStatus.CURRENT)
            return;

        qEvent.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
        qButton.UpdateButton(QuestEvent.EventStatus.DONE);
        qManager.UpdateQuestOnCompletion(qEvent);
    }
}
