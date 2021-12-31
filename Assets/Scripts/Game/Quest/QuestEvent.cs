using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvent
{
    #region Variables
    public enum EventStatus
    {
        WAITING, //Not yet completed but can't be worked on cause there's a prerequisite event
        CURRENT, //The one the player should be trying to complete at this moment
        DONE     //Has been completed
    };

    public string name, description, id;
    public int order = -1;
    public EventStatus status;
    public QuestButton button;
    public GameObject location;
    public List<QuestPath> pathlist = new List<QuestPath>();
    #endregion

    public QuestEvent(string n, string d, GameObject loc)
    {
        id = Guid.NewGuid().ToString();
        name = n;
        description = d;
        status = EventStatus.WAITING;
        location = loc;
    }

    public void UpdateQuestEvent(EventStatus es)
    {
        status = es;
        button.UpdateButton(es);
    }

    public string GetId()
    {
        return id;
    }
}
