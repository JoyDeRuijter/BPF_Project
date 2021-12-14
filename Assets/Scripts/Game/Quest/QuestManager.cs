using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest quest = new Quest();
    public GameObject questPrintBox, buttonPrefab, victoryPopup;

    QuestEvent final;

    public GameObject A, B, C, D, E;

    void Start()
    {
        //create each event
        QuestEvent a = quest.AddQuestEvent("Sheriff's request", "Meet with the sheriff", A);
        QuestEvent b = quest.AddQuestEvent("On the loose", "Find the wanted criminal", B);
        QuestEvent c = quest.AddQuestEvent("Wanted", "Kill the wanted criminal", C);
        QuestEvent d = quest.AddQuestEvent("Collect the evidence", "Pick up the bounty skull", D);
        QuestEvent e = quest.AddQuestEvent("Collect your bounty", "Meet with the sheriff", E);

        //define the paths between the events - e.g. the order they must be completed
        quest.AddPath(a.GetId(), b.GetId());
        quest.AddPath(a.GetId(), c.GetId());
        quest.AddPath(b.GetId(), c.GetId());
        quest.AddPath(c.GetId(), d.GetId());
        quest.AddPath(d.GetId(), e.GetId());

        quest.BFS(a.GetId());

        QuestButton button = CreateButton(a).GetComponent<QuestButton>();
        A.GetComponent<QuestLocation>().Setup(this, a, button);
        button = CreateButton(b).GetComponent<QuestButton>();
        B.GetComponent<QuestLocation>().Setup(this, b, button);
        button = CreateButton(c).GetComponent<QuestButton>();
        C.GetComponent<QuestKill>().Setup(this, c, button);
        button = CreateButton(d).GetComponent<QuestButton>();
        D.GetComponent<QuestLocation>().Setup(this, d, button);
        button = CreateButton(e).GetComponent<QuestButton>();
        E.GetComponent<QuestLocation>().Setup(this, e, button);

        final = e;
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
        if (e == final)
        {
            Debug.Log("quest completed");
            StartCoroutine(DisplayVictoryPopup(3));
            return;
        }

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

    private IEnumerator DisplayVictoryPopup(float seconds)
    {
        victoryPopup.SetActive(true);
        yield return new WaitForSeconds(seconds);
        victoryPopup.SetActive(false);
    }

}
