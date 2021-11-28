using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private int damage;
    [SerializeField] private float impactForce;
    public GameObject player;
    public Camera cam;
    public LayerMask whatIsGround, whatIsPlayer;

    public GameObject impactEffect;
    public ParticleSystem muzzleFlash;
    public NavMeshAgent agent;

    private RaycastHit hit;
    private Ray ray;
    private Vector3 distanceToPlayer;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    void Start()
    {
        damage = 5;
        health = 100f;
        alreadyAttacked = false;
    }

    void Update()
    {
        ray = cam.ScreenPointToRay(player.transform.position);
        distanceToPlayer = transform.position - player.transform.position;

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
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
