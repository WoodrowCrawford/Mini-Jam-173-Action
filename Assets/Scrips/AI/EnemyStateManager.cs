using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
  

    //Enemy states
    EnemyBaseState currentState;

    public EnemyWanderState wanderState = new EnemyWanderState();
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemyAttackState attackState = new EnemyAttackState();
    public EnemyTakeDamageState takeDamageState = new EnemyTakeDamageState();
    public EnemyDeathState deathState = new EnemyDeathState();



    [Header("Enemy Values")]
    public float health;
    public float damageTakenCooldown;

    public bool isDead;
    public bool canBeDamaged;

    public EnemyWeaponBehavior enemyWeapon;


    [Header("Nav Mesh Settings")]
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Animator animator;
    [SerializeField] public LayerMask _groundLayer;
    [SerializeField] public LayerMask _playerLayer;


    [Header("Patrolling")]
    public Vector3 destPoint;
    public bool walkPointSet;
    [SerializeField] float range;


    [Header("Chasing")]
    [SerializeField] public float sightRange;
    [SerializeField] public float attackRange;
    [SerializeField] public bool playerInSight;
    [SerializeField] public bool playerInAttackRange;
    public GameObject player;

    


    


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyWeapon = GameObject.FindGameObjectWithTag("EnemyWeapon").GetComponent<EnemyWeaponBehavior>();
    }

    private void OnEnable()
    {
        WeaponBehavior.OnPlayerAttackHit += () => SwitchState(takeDamageState);

       
    }

    private void OnDisable()
    {
        WeaponBehavior.OnPlayerAttackHit -= () => SwitchState(takeDamageState);
    }


    private void Start()
    {
        currentState = wanderState;
        currentState.EnterState(this);
    }


    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, _playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, _playerLayer);

        animator.SetFloat("Speed", agent.velocity.magnitude);
        animator.SetBool("PlayerInSight", playerInSight);
       

        currentState.UpdateState(this);


        if(health <= 0)
        {

           SwitchState(deathState);
        }
    }

     public IEnumerator Enumerator()
    {
        StartCoroutine(currentState.EnumeratorState(this));
        yield return null;
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
        EnemyBaseState.onSwitchState?.Invoke();
    }







    //WANDER
    public void Patrol()
    {
        if (!walkPointSet)
        {
            SearchForDest();
        }

        if (walkPointSet)
        {
            agent.SetDestination(destPoint);
        }

        if (Vector3.Distance(transform.position, destPoint) < 10)
        {
            walkPointSet = false;
        }


    }

    public void SearchForDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if(Physics.Raycast(destPoint, Vector3.down, _groundLayer))
        {
            walkPointSet = true;
        }
    }



    //CHASE
    public void Chase()
    {
        agent.SetDestination(player.transform.position);
    }


    public void Attack()
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01_SwordAndShiled"))
        {
            
            animator.SetTrigger("Attack");


        }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            Debug.Log("enemy is done attacking");

           

            SwitchState(wanderState);



        }


    }


    public void EnableAttack()
    {
        enemyWeapon.damageBox.enabled = true;
    }

    public void DisableAttack()
    {
        enemyWeapon.damageBox.enabled = false;
    }
}
