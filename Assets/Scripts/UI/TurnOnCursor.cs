using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnCursor : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Update()
    {
        
    }
}
