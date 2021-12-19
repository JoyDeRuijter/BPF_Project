using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questPrintBox, buttonPrefab, finalPopup, player;
    [SerializeField] private GameObject A, B, C, D, E;
    private QuestEvent final;
    public Quest quest = new Quest();

    public List<BaseQuest> quests;
    public Dictionary<string, BaseQuest> questDictionary = new Dictionary<string, BaseQuest>();

    void Start()
    {
        List<QuestEvent> questEvents = new List<QuestEvent>();

        foreach (var item in quests)
        {
            questDictionary.Add(item.questName, item);
            QuestEvent questEvent = quest.AddQuestEvent(item.questName, item.questDescription, item.gameObject);
            questEvents.Add(questEvent);
        }

        //define the paths between the events - e.g. the order they must be completed
        quest.AddPath(questEvents[0].GetId(), questEvents[1].GetId());
        quest.AddPath(questEvents[0].GetId(), questEvents[2].GetId());
        quest.AddPath(questEvents[1].GetId(), questEvents[2].GetId());
        quest.AddPath(questEvents[2].GetId(), questEvents[3].GetId());
        quest.AddPath(questEvents[3].GetId(), questEvents[4].GetId());

        quest.BFS(questEvents[0].GetId());

        QuestButton button = CreateButton(questEvents[0]).GetComponent<QuestButton>();
        A.GetComponent<BaseQuest>().Setup(this, questEvents[0], button);
        button = CreateButton(questEvents[1]).GetComponent<QuestButton>();
        B.GetComponent<QuestLocation>().Setup(this, questEvents[1], button);
        button = CreateButton(questEvents[2]).GetComponent<QuestButton>();
        C.GetComponent<QuestKill>().Setup(this, questEvents[2], button);
        button = CreateButton(questEvents[3]).GetComponent<QuestButton>();
        D.GetComponent<QuestPickup>().Setup(this, questEvents[3], button);
        button = CreateButton(questEvents[4]).GetComponent<QuestButton>();
        E.GetComponent<QuestLocation>().Setup(this, questEvents[4], button);

        final = questEvents[4];

        //BaseQuest quest = A.GetComponent<BaseQuest>();
        if (quest.GetType() == typeof(QuestLocation))
        { 
            
        }
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
            StartCoroutine(DisplayVictoryPopup(3));
            player.GetComponent<Player>().currentMoney += 500;
            player.GetComponent<Player>().currentXp += 100;
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
        finalPopup.SetActive(true);
        yield return new WaitForSeconds(seconds);
        finalPopup.SetActive(false);
    }

}
