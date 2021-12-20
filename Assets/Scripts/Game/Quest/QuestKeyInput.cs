using UnityEngine;

public class QuestKeyInput : BaseQuest
{
    #region Variables
    [SerializeField] private string key;
    private KeyCode keyCode;
    #endregion

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
