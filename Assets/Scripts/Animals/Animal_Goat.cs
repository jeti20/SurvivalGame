using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal_Goat : MonoBehaviour, IDamagable
{
    [Header("Stats")]
    public int health;
    public float walkSpeed;
    public float runSpeed;
    public ItemData[] dropOnDeath; // Przedmioty, które zwierzê upuszcza po œmierci

    [Header("AI")]
    public float safeDistance; // Odleg³oœæ, na jak¹ zwierzê chce siê oddaliæ od gracza
    public float detectDistance; // Odleg³oœæ wykrywania gracza

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    private NavMeshAgent agent;
    private Animator anim;

    private enum AIState { Idle, Wandering, Fleeing }
    private AIState aiState;

    private SkinnedMeshRenderer[] meshRenderers;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    void Start()
    {
        SetState(AIState.Wandering);
    }

    public float agentVelocityMagnitude;

    void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        anim.SetBool("Moving", aiState != AIState.Idle);

        switch (aiState)
        {
            case AIState.Idle:
            case AIState.Wandering:
                PassiveUpdate(playerDistance);
                break;
            case AIState.Fleeing:
                FleeingUpdate(playerDistance);
                break;
        }

        agentVelocityMagnitude = agent.velocity.magnitude;
    }

    void PassiveUpdate(float playerDistance)
    {
        if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }

        if (playerDistance < detectDistance)
        {
            SetState(AIState.Fleeing);
            agent.SetDestination(GetFleeLocation());
        }
    }

    void FleeingUpdate(float playerDistance)
    {
        if (playerDistance < safeDistance && agent.remainingDistance < 0.1f)
        {
            agent.SetDestination(GetFleeLocation());
        }
        else if (playerDistance > safeDistance)
        {
            SetState(AIState.Wandering);
        }
    }

    void SetState(AIState newState)
    {
        aiState = newState;

        switch (aiState)
        {
            case AIState.Idle:
                agent.speed = walkSpeed;
                agent.isStopped = true;
                break;
            case AIState.Wandering:
                agent.speed = walkSpeed;
                agent.isStopped = false;
                WanderToNewLocation();
                break;
            case AIState.Fleeing:
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
        }
    }

    void WanderToNewLocation()
    {
        if (aiState != AIState.Idle)
            return;

        SetState(AIState.Wandering);
        agent.SetDestination(GetRandomLocation(minWanderDistance, maxWanderDistance));
    }

    Vector3 GetFleeLocation()
    {
        Vector3 fleeDirection = (transform.position - PlayerController.instance.transform.position).normalized;
        Vector3 fleePosition = transform.position + fleeDirection * safeDistance;

        NavMeshHit hit;
        NavMesh.SamplePosition(fleePosition, out hit, safeDistance, NavMesh.AllAreas);

        return hit.position;
    }

    Vector3 GetRandomLocation(float minDistance, float maxDistance)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minDistance, maxDistance)), out hit, maxDistance, NavMesh.AllAreas);
        return hit.position;
    }

    public AudioClip damageSound;
    private AudioSource audioSource;
    public void TakePhysicalDamage(int damageAmount)
    {
        health -= damageAmount;
        audioSource.PlayOneShot(damageSound);
        if (health <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(DamageFlash());
        SetState(AIState.Fleeing);
    }

    void Die()
    {
        for (int i = 0; i < dropOnDeath.Length; i++)
        {
            Instantiate(dropOnDeath[i].dropPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    IEnumerator DamageFlash()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = new Color(1.0f, 0.6f, 0.6f);
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = Color.white;
        }
    }
}
