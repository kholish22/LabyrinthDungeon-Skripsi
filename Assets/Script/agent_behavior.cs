using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class agent_behavior : MonoBehaviour
{
    public UnityEvent OnAttack;
    public UnityEvent<Vector2> OnMovementInput;

    public Transform player;
    private NavMeshAgent agent;
    //Animator animator;


    //States (chaseRadius = sightRange, attackRadius = attackRange)
    public float chaseRadius;
    public float attackRadius;

    [SerializeField]
    private float attackDelay = 1;
    private float passedTime = 1;
    

    
    // Start is called before the first frame update
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        
    }

    //Chase AI

    void CheckDistance()
    {
        if(player == null)
            return;

        float distance = Vector2.Distance(player.position, transform.position);
        if(distance < chaseRadius)
        {
            if(distance <= attackRadius)
            {
                //attack
                OnMovementInput?.Invoke(Vector2.zero);
                if(passedTime >= attackDelay)
                {
                    passedTime = 0;
                    OnAttack?.Invoke();
                }
            }
            else
            {
                //chase
                agent.SetDestination(player.position);
                //animator.SetTrigger("Move");
            }
        }

        //idle
        if(passedTime <attackDelay)
        {
            passedTime += Time.deltaTime;
        }

        
    }

    
}

