using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private ParticleSystem playerEffect;

    private enum PickupTypes
    { 
        Health,
        Ammo,
        Weapon,
        Bounty
    };

    [SerializeField] private PickupTypes pickupType;

    void Start()
    {
        if (pickupType == PickupTypes.Bounty)
            playerEffect = GameObject.FindGameObjectWithTag("BountyEffect").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        
    }

    void OnParticleCollision(GameObject player)
    {
        Destroy(gameObject);
        playerEffect.Play();

        if(pickupType == PickupTypes.Health)
            player.GetComponent<Player>().health += 15;
    }
}
