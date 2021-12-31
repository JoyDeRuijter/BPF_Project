using UnityEngine;

public class QuestKeyInput : BaseQuest
{
    #region Variables
    [SerializeField] private string key;
    private KeyCode keyCode;
    private bool isScrolling;
    #endregion

    private void Awake()
    {
        if (key != "scrollWheel")  
            keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), key);
    }

    void Update()
    {
        if (key == "scrollWheel")
            CheckScrollWheel();
        if (Input.GetKeyDown(keyCode) || isScrolling)
            UpdateStatus();
    }

    private void CheckScrollWheel()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 || Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            isScrolling = true;
    }
}
