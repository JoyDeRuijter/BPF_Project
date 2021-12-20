using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HostileNPC : BaseNPC
{
    #region Variables
    [Header ("Is NPC Wanted?")]
    [SerializeField] private bool isWanted;

    [Header ("NPC Properties")]
    [Space(10)]
    [SerializeField] private float health;
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private int damage;

    [Header ("References")]
    [Space(10)]
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject bountyHead;
    [SerializeField] private GameObject npcWeapon;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private NavMeshAgent agent;

    private bool playerInSightRange;
    private bool playerInAttackRange;
    private bool alreadyAttacked;
    private bool isMoving;
    private bool isHolstered;
    public bool wantedIsKilled { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        if (damage == 0) damage = 5;
        if (health == 0) health = 100f;
        if (!isWanted) bountyHead = null;
    }

    protected override void Update()
    {
        base.Update();
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        isHolstered = player.GetComponentInChildren<WeaponSwitching>().isHolstered;
        if (isWanted) WantedBehaviour();
        else if (!isWanted) HostileBehaviour();
        UpdateAnimations();
    }

    private void FollowPlayer()
    {
        //Move the agent towards the location of the player
        if (Physics.Raycast(ray, out hit))
        {
            transform.LookAt(player.transform.position);
            agent.SetDestination(hit.point);
        }
    }

    private void ShootPlayer()
    {
        transform.LookAt(player.transform.position);
        RaycastHit shootHit;
        if (Physics.Raycast(transform.position, transform.forward, out shootHit, attackRange) && !alreadyAttacked)
        {
            muzzleFlash.Play();
            player.GetComponent<Player>().TakeDamage(damage);

            GameObject impactGO = Instantiate(impactEffect, shootHit.point, Quaternion.LookRotation(shootHit.normal));
            impactGO.GetComponent<ParticleSystem>().Play();
            Destroy(impactGO, 2f);
            alreadyAttacked = true;
            StartCoroutine(ResetAttack());
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        alreadyAttacked = false;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
            Die();
    }

    private void SpawnBounty()
    {
        //Spawn a bountyhead gameobject on the position of the NPC
        Vector3 bountyPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        GameObject bountyGO = Instantiate(bountyHead, bountyPosition, Quaternion.LookRotation(hit.normal));
        bountyGO.GetComponent<ParticleSystem>().Play();
    }

    private IEnumerator IsMoving()
    {
        //Check whether the NPC is currently moving or not
        Vector3 prevPos = transform.position;
        yield return new WaitForSeconds(0.15f);
        Vector3 actualPos = transform.position;

        if (prevPos == actualPos)
            isMoving = false;
        else if (prevPos != actualPos)
            isMoving = true;
    }

    private void UpdateAnimations()
    {
        //Update the current animation depending on if the NPC is moving or not
        if (isMoving)
            anim.SetBool("IsRunning", true);
        else
            anim.SetBool("IsRunning", false);
    }

    private void Die()
    {
        player.GetComponent<Player>().currentXp += 10;

        if (isWanted)
        {
            GameObject.FindGameObjectWithTag("KillManager").GetComponent<QuestKill>().isKilled = true;
            SpawnBounty();
        }
        Destroy(gameObject);
    }

    private void WantedBehaviour()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        //If the NPC is a WantedNPC, they will always follow and shoot the player no matter what
        if (playerInSightRange && !playerInAttackRange)
            FollowPlayer();
        if (playerInSightRange && playerInAttackRange)
            ShootPlayer();
        
        StartCoroutine(IsMoving());
    }

    private void HostileBehaviour()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        //If the NPC is not a WantedNPC, they will only follow, attack and draw their weapon if the player has their weapon unholstered.
        if (isHolstered)
            npcWeapon.SetActive(false);

        else if (!isHolstered)
        {
            npcWeapon.SetActive(true);
            if (playerInSightRange && !playerInAttackRange)
                FollowPlayer();
            if (playerInSightRange && playerInAttackRange)
                ShootPlayer();
        }
        StartCoroutine(IsMoving());
    }   
}
