using UnityEngine;

public class QuestLocation : BaseQuest
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Player") 
            return;

        UpdateStatus();
    }
}
