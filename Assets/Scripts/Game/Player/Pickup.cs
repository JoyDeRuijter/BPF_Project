using UnityEngine;

public class Pickup : MonoBehaviour
{
    #region Variables
    [SerializeField] private ParticleSystem playerEffect;

    private enum PickupTypes
    { 
        Health,
        Ammo,
        Weapon,
        Bounty
    };

    [SerializeField] private PickupTypes pickupType;
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

        if(pickupType == PickupTypes.Health)
            player.GetComponent<Player>().currentHealth += 15;

        if (pickupType == PickupTypes.Bounty)
        {
            GameObject.FindGameObjectWithTag("PickupManager").GetComponent<QuestPickup>().isPickedUp = true;
            player.GetComponent<Player>().currentXp += 50;
        }
        Destroy(gameObject);
    }
}
