using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject player, standardItem, replaceItem;

    void Start()
    {
    }

    void Update()
    {
        if (player.GetComponent<Player>().isLevelingUp)
        {
            Debug.Log("if statement does something");
            StartCoroutine(ShowPopUp());
            player.GetComponent<Player>().isLevelingUp = false;
        }
    }

    private IEnumerator ShowPopUp()
    {
        Debug.Log("Enumerator does something");
        standardItem.SetActive(false);
        replaceItem.SetActive(true);
        yield return new WaitForSeconds(2);
        replaceItem.SetActive(false);
        standardItem.SetActive(true);
    }
}
