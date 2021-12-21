using UnityEngine;

public class BaseNPC : MonoBehaviour
{
    #region Variables
    [Header ("References")]
    public GameObject player;
    public GameObject npc;
    public Camera cam;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    [HideInInspector]
    public RaycastHit hit;
    [HideInInspector]
    public Ray ray;
    [HideInInspector]
    public Animator anim;
    #endregion

    protected virtual void Awake()
    {
        anim = npc.GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        ray = cam.ScreenPointToRay(player.transform.position);
    }
}

