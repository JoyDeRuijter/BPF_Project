using UnityEngine;

public class QuestKeyInput : BaseQuest
{
    [SerializeField] private string key;
    private KeyCode keyCode;

    private void Awake()
    {
        keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), key);
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode))
            UpdateStatus();
    }
}
