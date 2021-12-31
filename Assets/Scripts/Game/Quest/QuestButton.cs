using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    #region Variables
    [Header ("Questbutton components")]
    [SerializeField] private GameObject questButton;
    [SerializeField] private Button buttonComponent;
    [SerializeField] private RawImage icon;
    [SerializeField] private Text eventText;

    [Header("Status icons")]
    [Space(10)]
    [SerializeField] private Sprite currentImage;
    [SerializeField] private Sprite waitingImage;
    [SerializeField] private Sprite doneImage;

    private QuestEvent thisEvent;
    private CompassController compassController;
    private QuestEvent.EventStatus status;
    #endregion

    private void Awake()
    {
        compassController = GameObject.Find("Compass").GetComponent<CompassController>();
        CompassFocus();
    }

    public void Setup(QuestEvent e, GameObject scrollList)
    {
        //Set up all the values necessary for the questbutton
        thisEvent = e;
        buttonComponent.transform.SetParent(scrollList.transform, false);
        eventText.text = "<b>" + thisEvent.name + "</b>\n" + thisEvent.description;
        status = thisEvent.status;
        icon.texture = waitingImage.texture;
        buttonComponent.interactable = false;
    }

    public void UpdateButton(QuestEvent.EventStatus s)
    {
        //Visually update the questbutton depending on the eventstatus
        status = s;
        if (status == QuestEvent.EventStatus.DONE)
        {
            icon.texture = doneImage.texture;
            buttonComponent.interactable = false;
            StartCoroutine(UpdateMissionLog(1.5f));
        }
        else if (status == QuestEvent.EventStatus.WAITING)
        {
            icon.texture = waitingImage.texture;
            buttonComponent.interactable = false;
        }
        else if (status == QuestEvent.EventStatus.CURRENT)
        {
            icon.texture = currentImage.texture;
            buttonComponent.interactable = true;
        }

        //Update the compass focus at the same time
        CompassFocus();
    }

    private void CompassFocus()
    {
        //Set compass controller to point towards the location of the current event
        if (status == QuestEvent.EventStatus.CURRENT)
            compassController.target = thisEvent.location;
    }

    private IEnumerator UpdateMissionLog(float seconds)
    {
        //Deactivates the questbutton after seconds
        yield return new WaitForSeconds(seconds);
        questButton.SetActive(false);
    }
}
