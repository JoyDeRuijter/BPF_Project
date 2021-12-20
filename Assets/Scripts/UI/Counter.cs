using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    #region Variables
    [Header ("References")]
    public GameObject player;
    public Text counter;
    [HideInInspector]
    public int value;
    #endregion

    protected virtual void Update()
    {
        DisplayCounter();
    }

    public virtual void DisplayCounter()
    {
        counter.text = "" + value;
    }
}
