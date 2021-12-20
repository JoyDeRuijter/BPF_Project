using System.Collections;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    #region
    [Header ("Gameobject references")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject standardItem; 
    [SerializeField] private GameObject replaceItem;
    #endregion

    void Update()
    {
        if (player.GetComponent<Player>().isLevelingUp)
        {
            StartCoroutine(ShowPopUp());
            player.GetComponent<Player>().isLevelingUp = false;
        }
    }

    private IEnumerator ShowPopUp()
    {
        standardItem.SetActive(false);
        replaceItem.SetActive(true);
        yield return new WaitForSeconds(2);
        replaceItem.SetActive(false);
        standardItem.SetActive(true);
    }
}
