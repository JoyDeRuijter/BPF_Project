using UnityEngine;

public class InteractiveNPC : BaseNPC
{
    #region Variables
    [Header ("References")]
    [SerializeField] private GameObject questArea;
    [SerializeField] private GameObject missionPopup;
    private bool missionIsShowing;
    #endregion

    protected override void Update()
    {
        base.Update();
        Behaviour();
    }

    private void Behaviour()
    {
        transform.LookAt(player.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        //If the player is in the area around the NPC and they press E while the popup is not showing yet, it will show the popup
        if (questArea.GetComponent<AreaCollision>().isColliding && Input.GetKeyDown(KeyCode.E) && !missionIsShowing)
        {
            GameObject.FindGameObjectWithTag("InteractionManager").GetComponent<QuestInteraction>().isInteracting = true;
            missionPopup.SetActive(true);
            missionIsShowing = true;
        }
        //If it is showing the popup, this will delete the popup from the screen again
        else if (questArea.GetComponent<AreaCollision>().isColliding && Input.GetKeyDown(KeyCode.E) && missionIsShowing)
        {
            GameObject.FindGameObjectWithTag("InteractionManager").GetComponent<QuestInteraction>().isInteracting = false;
            missionPopup.SetActive(false);
            missionIsShowing = false;
        }
    }
}
