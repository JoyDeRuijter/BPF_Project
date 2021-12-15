using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] private float health, sightRange, attackRange, timeBetweenAttacks;
    [SerializeField] private int damage;
    [SerializeField] private GameObject player, impactEffect, bountyHead, npc, npcWeapon;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private NavMeshAgent agent;

    private bool playerInSightRange, playerInAttackRange, alreadyAttacked, isMoving, isHolstered;
    private RaycastHit hit;
    private Ray ray;
    private Animator anim;
    public bool wantedIsKilled { get; private set; }

    private enum NPCtype 
    { 
        Friendly,
        Hostile,
        Wanted
    };

    [SerializeField] private NPCtype npcType;

    void Start()
    {
        wantedIsKilled = false;
        damage = 5;
        health = 100f;
        alreadyAttacked = false;
        anim = npc.GetComponent<Animator>();
        isHolstered = player.GetComponentInChildren<WeaponSwitching>().isHolstered;
    }

    void Update()
    {
        isHolstered = player.GetComponentInChildren<WeaponSwitching>().isHolstered;
        ray = cam.ScreenPointToRay(player.transform.position);

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        switch (npcType) 
        {
            case NPCtype.Wanted:
                WantedBehaviour();
                break;
            case NPCtype.Hostile:
                HostileBehaviour();
                break;
            case NPCtype.Friendly:
                FriendlyBehaviour();
                break;
        }

        StartCoroutine(IsMoving());
        UpdateAnimations();
    }

    private void FollowPlayer()
    {
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

    //TODO
    private void Patrol()
    { 
        // Add an NPC patrol
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
        Vector3 bountyPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        GameObject bountyGO = Instantiate(bountyHead, bountyPosition, Quaternion.LookRotation(hit.normal));
        bountyGO.GetComponent<ParticleSystem>().Play();
    }

    private IEnumerator IsMoving()
    {
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
        if (isMoving)
            anim.SetBool("IsRunning", true);
        else
            anim.SetBool("IsRunning", false);
    }

    private void Die()
    {
        player.GetComponent<Player>().currentXp += 10;

        if (npcType == NPCtype.Wanted)
        {
            GameObject.FindGameObjectWithTag("KillManager").GetComponent<QuestKill>().isKilled = true;
            SpawnBounty();
        }
        Destroy(gameObject);
    }

    private void WantedBehaviour()
    {
        Patrol();
        if (playerInSightRange && !playerInAttackRange)
            FollowPlayer();
        if (playerInSightRange && playerInAttackRange)
            ShootPlayer();
    }

    private void HostileBehaviour()
    {
        if (isHolstered)
        {
            Patrol();
            npcWeapon.SetActive(false);
        }
        else if (!isHolstered)
        {
            Patrol();
            npcWeapon.SetActive(true);
            if (playerInSightRange && !playerInAttackRange)
                FollowPlayer();
            if (playerInSightRange && playerInAttackRange)
                ShootPlayer();
        }
    }

    private void FriendlyBehaviour()
    { 
        //Insert friendly behaviour after friendly NPC 
    }
}
