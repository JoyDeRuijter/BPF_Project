using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    public GameObject questButton;
    public Button buttonComponent;
    public RawImage icon;
    public Text eventName;
    public Sprite currentImage, waitingImage, doneImage;
    public QuestEvent thisEvent;
    public CompassController compassController;

    QuestEvent.EventStatus status;

    private void Awake()
    {
        CompassFocus();
        compassController = GameObject.Find("Compass").GetComponent<CompassController>();
    }

    public void Setup(QuestEvent e, GameObject scrollList)
    {
        thisEvent = e;
        buttonComponent.transform.SetParent(scrollList.transform, false);
        eventName.text = "<b>" + thisEvent.name + "</b>\n" + thisEvent.description;
        status = thisEvent.status;
        icon.texture = waitingImage.texture;
        buttonComponent.interactable = false;
    }

    public void UpdateButton(QuestEvent.EventStatus s)
    {
        status = s;
        if (status == QuestEvent.EventStatus.DONE)
        {
            icon.texture = doneImage.texture;
            buttonComponent.interactable = false;
            StartCoroutine(UpdateMissionLog(1.5f));
            ScaleMissionLog();
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

        CompassFocus();
    }

    public void CompassFocus()
    {
        //Set compass controller to point toward the location of this event
        if (status == QuestEvent.EventStatus.CURRENT)
            compassController.target = thisEvent.location;
    }

    private IEnumerator UpdateMissionLog(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        questButton.SetActive(false);
    }

    private void ScaleMissionLog()
    {

    }
}
