using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private ParticleSystem playerEffect;

    private enum PickupTypes
    { 
        Health,
        Ammo,
        Weapon
    };

    [SerializeField] private PickupTypes pickupType;

    void Start()
    {
        
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
