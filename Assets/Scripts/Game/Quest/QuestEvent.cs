using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvent
{ 
    public enum EventStatus
    {
        WAITING,
        CURRENT,
        DONE
    };
    //WAITING - not yet completed but can't be worked on cause there's a prerequisite event
    //CURRENT - the one the player should be trying to achieve
    //DONE - has been achieved

    public string name, description, id;
    public int order = -1;
    public EventStatus status;
    public QuestButton button;
    public GameObject location;

    public List<QuestPath> pathlist = new List<QuestPath>();

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
