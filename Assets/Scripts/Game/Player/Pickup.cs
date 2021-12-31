using UnityEngine;

public class Pickup : MonoBehaviour
{
    #region Variables
    [Header("Is the health a quest?")]
    [SerializeField] private bool isHealthQuest;

    private enum PickupTypes
    { 
        Health,
        Ammo,
        Weapon,
        Bounty
    };

    [Header ("Pickuptypes")]
    [Space(10)]
    [SerializeField] private PickupTypes pickupType;

    [Header("References")]
    [Space(10)]
    [SerializeField] private ParticleSystem playerEffect;
    #endregion

    void Awake()
    {
        if (pickupType == PickupTypes.Bounty)
            playerEffect = GameObject.FindGameObjectWithTag("BountyEffect").GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject player)
    {
        //If the player collides with a pickup, give the effect to the player and destroy the pickup
        playerEffect.Play();

        if (pickupType == PickupTypes.Health)
        { 
            player.GetComponent<Player>().currentHealth += 15;
            if(isHealthQuest)
                GameObject.FindGameObjectWithTag("PickupManager").GetComponent<QuestPickup>().isPickedUp = true;
        }


        if (pickupType == PickupTypes.Bounty)
        {
            GameObject.FindGameObjectWithTag("PickupManager").GetComponent<QuestPickup>().isPickedUp = true;
            player.GetComponent<Player>().currentXp += 50;
        }
        Destroy(gameObject);
    }
}
