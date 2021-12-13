using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocation : MonoBehaviour
{
    //This code goes on a gameobject that represents the task to be performed
    //by the player at the location of the object. This code can contain any logic
    //as when the task is complete it injects the three statuses back into the quest
    //system (as per in the OnCollisionEnter) currently here.
    public QuestManager qManager;
    public QuestEvent qEvent;
    public QuestButton qButton;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Player") 
            return;

        //If we shouldn't be working on this event
        //then don't register it as completed
        if (qEvent.status != QuestEvent.EventStatus.CURRENT)
            return;

        //Inject these back into the Quest Manager to Update States
        qEvent.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
        qButton.UpdateButton(QuestEvent.EventStatus.DONE);
        qManager.UpdateQuestOnCompletion(qEvent);
    }

    public void Setup(QuestManager qm, QuestEvent qe, QuestButton qb)
    {
        qManager = qm;
        qEvent = qe;
        qButton = qb;
        //setup link between event and button
        qe.button = qButton;
    }
}
