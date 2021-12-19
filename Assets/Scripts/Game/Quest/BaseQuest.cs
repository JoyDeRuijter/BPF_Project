using UnityEngine;

public class BaseQuest : MonoBehaviour
{
    #region Variables
    [Header ("Quest references")]
    public QuestManager qManager;
    public QuestEvent qEvent;
    public QuestButton qButton;

    [Header("Quest properties")]
    [Space(10)]
    [SerializeField] public string questName;
    [SerializeField] public string questDescription;
    #endregion

    public virtual void Setup(QuestManager qm, QuestEvent qe, QuestButton qb)
    {
        qManager = qm;
        qEvent = qe;
        qButton = qb;
        qe.button = qButton;
    }

    public virtual void UpdateStatus()
    {
        if (qEvent.status != QuestEvent.EventStatus.CURRENT)
            return;

        qEvent.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
        qButton.UpdateButton(QuestEvent.EventStatus.DONE);
        qManager.UpdateQuestOnCompletion(qEvent);
    }
}
