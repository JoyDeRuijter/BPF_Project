using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseQuest : MonoBehaviour
{
    public QuestManager qManager;
    public QuestEvent qEvent;
    public QuestButton qButton;
    [SerializeField] public string questName;
    [SerializeField] public string questDescription;

    public virtual void Setup(QuestManager qm, QuestEvent qe, QuestButton qb)
    {
        qManager = qm;
        qEvent = qe;
        qButton = qb;
        qe.button = qButton;
    }
}
