using UnityEngine;

public class AreaCollision : MonoBehaviour
{
    [HideInInspector]
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
