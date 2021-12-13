using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest quest = new Quest();
    public GameObject questPrintBox, buttonPrefab;

    public GameObject A, B, C;

    void Start()
    {
        //create each event
        QuestEvent a = quest.AddQuestEvent("test1", "description 1");
        QuestEvent b = quest.AddQuestEvent("test2", "description 2");
        QuestEvent c = quest.AddQuestEvent("test3", "description 3");

        //define the paths between the events - e.g. the order they must be completed
        quest.AddPath(a.GetId(), b.GetId());
        quest.AddPath(b.GetId(), c.GetId());
        quest.AddPath(a.GetId(), c.GetId());

        quest.BFS(a.GetId());

        QuestButton button = CreateButton(a).GetComponent<QuestButton>();
        A.GetComponent<QuestLocation>().Setup(this, a, button);
        button = CreateButton(b).GetComponent<QuestButton>();
        B.GetComponent<QuestLocation>().Setup(this, b, button);
        button = CreateButton(c).GetComponent<QuestButton>();
        C.GetComponent<QuestLocation>().Setup(this, c, button);

        quest.PrintPath();
    }

    GameObject CreateButton(QuestEvent e)
    {
        GameObject b = Instantiate(buttonPrefab);
        b.GetComponent<QuestButton>().Setup(e, questPrintBox);
        if (e.order == 1)
        {
            b.GetComponent<QuestButton>().UpdateButton(QuestEvent.EventStatus.CURRENT);
            e.status = QuestEvent.EventStatus.CURRENT;
        }
        return b;
    }

    public void UpdateQuestOnCompletion(QuestEvent e)
    {
        foreach (QuestEvent n in quest.questEvents)
        {
            //if this event is next in order
            if (n.order == (e.order + 1))
            {
                //make the next in line available for completion
                n.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
            }
        }
    }

}
