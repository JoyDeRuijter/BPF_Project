using UnityEngine;

public class CompassController : MonoBehaviour
{
    #region Variables
    [Header ("References")]
    [SerializeField] private GameObject pointer;
    public GameObject target;
    [SerializeField] private GameObject player;
    [SerializeField] private RectTransform compassLine;

    private RectTransform rect;
    private Vector3 startPosition;
    #endregion

    void Awake()
    {
        rect = pointer.GetComponent<RectTransform>();
        startPosition = rect.localPosition;
    }

    private void Update()
    {
        UpdateCompassPos();
    }

    private void UpdateCompassPos()
    {
        Vector3[] v = new Vector3[4];
        compassLine.GetLocalCorners(v);
        float pointerScale = Vector3.Distance(v[1], v[2]); //Bottom corners of the compassbar

        //If there is a target, set compasspointer position in the direction of the target
        if (target != null)
        {
            Vector3 direction = target.transform.position - player.transform.position;
            float angleToTarget = Vector3.SignedAngle(player.transform.forward, direction, player.transform.up);
            angleToTarget = Mathf.Clamp(angleToTarget, -90, 90) / 180.0f * pointerScale;
            rect.localPosition = new Vector3(angleToTarget, rect.localPosition.y, rect.localPosition.z);
        }
        else
            rect.localPosition = startPosition;
    }
}
