using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    EnemyManager em;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        em = EnemyManager.instance;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(em.player.position);
        agent.speed = agent.speed + (agent.speed * em.speedMod);
        anim.speed = anim.speed + (anim.speed * em.speedMod);
    }

    // Update is called once per frame
    void Update()
    {
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
}
