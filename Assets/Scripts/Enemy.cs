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

    public GameObject impactEffect;
    public NavMeshAgent agent;

    void Start()
    {
        health = 100f;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(player.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            transform.LookAt(player.transform.position);
            agent.SetDestination(hit.point);
        }
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
