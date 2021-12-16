using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] private float health, sightRange, attackRange, timeBetweenAttacks;
    [SerializeField] private int damage;
    [SerializeField] private GameObject player, impactEffect, bountyHead, npc, npcWeapon, questArea, missionPopup;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private NavMeshAgent agent;

    private bool playerInSightRange, playerInAttackRange, alreadyAttacked, isMoving, isHolstered, missionIsShowing;
    private RaycastHit hit;
    private Ray ray;
    private Animator anim;
    public bool wantedIsKilled { get; private set; }

    private enum NPCtype 
    { 
        Friendly,
        Hostile,
        Wanted,
        Interactive
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

        if (npcType != NPCtype.Interactive)
            questArea = null;

        missionIsShowing = false;
    }

    void Update()
    {
        isHolstered = player.GetComponentInChildren<WeaponSwitching>().isHolstered;
        ray = cam.ScreenPointToRay(player.transform.position);

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        checkType();
        UpdateAnimations();
    }

    private void checkType()
    {
        switch (npcType)
        {
            case NPCtype.Wanted: WantedBehaviour(); break;
            case NPCtype.Hostile: HostileBehaviour(); break;
            case NPCtype.Friendly: FriendlyBehaviour(); break;
            case NPCtype.Interactive: InteractiveBehaviour(); break;
        }
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
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        Patrol();
        if (playerInSightRange && !playerInAttackRange)
            FollowPlayer();
        if (playerInSightRange && playerInAttackRange)
            ShootPlayer();

        StartCoroutine(IsMoving());
    }

    private void HostileBehaviour()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
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

        StartCoroutine(IsMoving());
    }

    private void FriendlyBehaviour()
    { 
        //Insert friendly behaviour after friendly NPC 
    }

    private void InteractiveBehaviour()
    {
        transform.LookAt(player.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        if (questArea.GetComponent<AreaCollision>().isColliding && Input.GetKeyDown(KeyCode.E) && !missionIsShowing)
        {
            GameObject.FindGameObjectWithTag("InteractionManager").GetComponent<QuestInteraction>().isInteracting = true;
            missionPopup.SetActive(true);
            missionIsShowing = true;
        }
        else if (questArea.GetComponent<AreaCollision>().isColliding && Input.GetKeyDown(KeyCode.E) && missionIsShowing)
        {
            GameObject.FindGameObjectWithTag("InteractionManager").GetComponent<QuestInteraction>().isInteracting = false;
            missionPopup.SetActive(false);
            missionIsShowing = false;
        }

        //else
        // GameObject.FindGameObjectWithTag("InteractionManager").GetComponent<QuestInteraction>().isInteracting = false;

    }
}
