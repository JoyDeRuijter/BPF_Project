using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestKill : MonoBehaviour
{
    public QuestManager qManager;
    public QuestEvent qEvent;
    public QuestButton qButton;
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

    public void Setup(QuestManager qm, QuestEvent qe, QuestButton qb)
    {
        qManager = qm;
        qEvent = qe;
        qButton = qb;
        qe.button = qButton;
    }
}
