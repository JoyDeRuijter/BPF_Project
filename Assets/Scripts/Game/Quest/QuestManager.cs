using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    #region Variables
    [Header("Gameobject references")]
    [SerializeField] private GameObject questPrintBox;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject finalPopup;
    [SerializeField] private GameObject player;
    public Dictionary<string, BaseQuest> questDictionary = new Dictionary<string, BaseQuest>();

    [Header("List of quests")]
    [Space(10)]
    public List<BaseQuest> quests;

    public Quest quest = new Quest();
    private QuestEvent final;
    #endregion

    void Awake()
    {
        DefineQuestEvents();
    }

    private void DefineQuestEvents()
    {
        List<QuestEvent> questEvents = new List<QuestEvent>();
        List<GameObject> questObjects = new List<GameObject>();

        //Add quests to dictionary, create the questevents, add questevents and questobjects to a list
        foreach (var item in quests)
        {
            questDictionary.Add(item.questName, item);
            QuestEvent questEvent = quest.AddQuestEvent(item.questName, item.questDescription, item.gameObject);
            questEvents.Add(questEvent);
            questObjects.Add(item.gameObject);
        }

        //Define the paths between the events
        quest.AddPath(questEvents[0].GetId(), questEvents[1].GetId());
        quest.AddPath(questEvents[0].GetId(), questEvents[2].GetId());
        quest.AddPath(questEvents[1].GetId(), questEvents[2].GetId());
        quest.AddPath(questEvents[2].GetId(), questEvents[3].GetId());
        quest.AddPath(questEvents[3].GetId(), questEvents[4].GetId());

        //Run breadth first search
        quest.BFS(questEvents[0].GetId());

        //Create the quest buttons for the events
        QuestButton button = CreateButton(questEvents[0]).GetComponent<QuestButton>();
        questObjects[0].GetComponent<BaseQuest>().Setup(this, questEvents[0], button);
        button = CreateButton(questEvents[1]).GetComponent<QuestButton>();
        questObjects[1].GetComponent<BaseQuest>().Setup(this, questEvents[1], button);
        button = CreateButton(questEvents[2]).GetComponent<QuestButton>();
        questObjects[2].GetComponent<BaseQuest>().Setup(this, questEvents[2], button);
        button = CreateButton(questEvents[3]).GetComponent<QuestButton>();
        questObjects[3].GetComponent<BaseQuest>().Setup(this, questEvents[3], button);
        button = CreateButton(questEvents[4]).GetComponent<QuestButton>();
        questObjects[4].GetComponent<BaseQuest>().Setup(this, questEvents[4], button);

        //Save the final quest
        final = questEvents[questEvents.Count - 1];
    }

    GameObject CreateButton(QuestEvent e)
    {
        //Instantiate the buttons and update their status
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
        //Execute some actions if the last quest is completed
        if (e == final)
        {
            StartCoroutine(DisplayPopup(3));
            player.GetComponent<Player>().currentMoney += 500;
            player.GetComponent<Player>().currentXp += 100;
            return;
        }

        foreach (QuestEvent n in quest.questEvents)
        {
            //If this event is next in order
            if (n.order == (e.order + 1))
            {
                //Make it available for completion
                n.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
            }
        }
    }

    private IEnumerator DisplayPopup(float seconds)
    {
        //Display the final popup and deactivate it after seconds
        finalPopup.SetActive(true);
        yield return new WaitForSeconds(seconds);
        finalPopup.SetActive(false);
    }

}
