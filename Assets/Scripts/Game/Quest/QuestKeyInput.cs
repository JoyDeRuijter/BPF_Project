using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestKeyInput : BaseQuest
{
    [SerializeField] private string key;
    KeyCode keyCode;

    private void Awake()
    {
        keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), key);
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode))
            KeyIsPressed();
    }

    private void KeyIsPressed()
    {
        if (qEvent.status != QuestEvent.EventStatus.CURRENT)
            return;

        qEvent.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
        qButton.UpdateButton(QuestEvent.EventStatus.DONE);
        qManager.UpdateQuestOnCompletion(qEvent);
    }
}
