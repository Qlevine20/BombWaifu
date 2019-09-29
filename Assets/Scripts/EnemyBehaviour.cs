using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    EnemyManager em;
    Animator anim;
    public float deathTime;
    private float deathTimer = 0;
    private bool dying;
    // Start is called before the first frame update
    void Start()
    {
        em = EnemyManager.instance;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = agent.speed + (agent.speed * em.speedMod);
        anim.speed = anim.speed + (anim.speed * em.speedMod);
    }

    // Update is called once per frame
    void Update()
    {
        if (dying)
        {
            if(deathTimer >= deathTime)
            {
                deathTimer = 0;
                dying = false;
                agent.enabled = true;
                EnemyPool.Instance.ReturnToPool(this);
            }
            deathTimer += Time.deltaTime;
        }
        if(agent && em)
        {
            agent.SetDestination(em.player.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            if(!em.collided)
                em.CollidedWithPlayer();
        }
    }

    public void Die()
    {
        agent.enabled = false;
        dying = true;
    }
}
