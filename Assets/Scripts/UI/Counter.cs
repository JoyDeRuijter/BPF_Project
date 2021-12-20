using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public GameObject player;
    public Text counter;
    public int value;

    private void Update()
    {
        DisplayCounter();
    }

    public virtual void DisplayCounter()
    {
        counter.text = "" + value;
    }

}
