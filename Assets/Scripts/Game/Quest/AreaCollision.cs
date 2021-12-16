using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollision : MonoBehaviour
{
    public bool isColliding;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Player")
            return;

        isColliding = true; 
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag != "Player")
            return;

        isColliding = false;
    }
}
