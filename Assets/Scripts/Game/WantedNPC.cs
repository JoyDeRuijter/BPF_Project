using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WantedNPC : BaseNPC
{
    [SerializeField] private float health;
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private int damage;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject bountyHead;
    [SerializeField] private GameObject npcWeapon;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private NavMeshAgent agent;
    private bool playerInSightRange;
    private bool playerInAttackRange;
    private bool alreadyAttacked;
    private bool isMoving;
    public bool wantedIsKilled { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (damage == 0) damage = 5;
        if (health == 0) health = 100f;
    }

    protected override void Update()
    {
        base.Update();
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        //wantedbehaviour
        //updateanimations
    }
}
