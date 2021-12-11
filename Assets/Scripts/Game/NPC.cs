using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] private float health, sightRange, attackRange, timeBetweenAttacks;
    [SerializeField] private int damage;
    [SerializeField] private GameObject player, impactEffect, bountyHead;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private NavMeshAgent agent;

    private bool playerInSightRange, playerInAttackRange, alreadyAttacked;
    private RaycastHit hit;
    private Ray ray;

    private enum NPCtype 
    { 
        Friendly,
        Hostile,
        Wanted
    };

    [SerializeField] private NPCtype npcType;

    void Start()
    {
        damage = 5;
        health = 100f;
        alreadyAttacked = false;
    }

    void Update()
    {
        ray = cam.ScreenPointToRay(player.transform.position);

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(playerInSightRange && !playerInAttackRange) FollowPlayer();
        if(playerInSightRange && playerInAttackRange) ShootPlayer();
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

    private void Die()
    {
        if(npcType == NPCtype.Wanted)
            SpawnBounty();

        Destroy(gameObject);
    }
}
