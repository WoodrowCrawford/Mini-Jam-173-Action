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
    public float attackCoolDown;

    public bool isDead;
    public bool canBeDamaged;
    public bool isAttacking;
    public bool canAttackAgain = true;

    public EnemyWeaponBehavior enemyWeapon;


    [Header("Nav Mesh Settings")]
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Animator animator;
    [SerializeField] public LayerMask _groundLayer;
    [SerializeField] public LayerMask _playerLayer;

    public int areaMask;

    


    [Header("Patrolling")]
    public Vector3 randomPoint;
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
        enemyWeapon = GetComponentInChildren<EnemyWeaponBehavior>();
    }

    private void OnEnable()
    {
        WeaponBehavior.OnPlayerAttackHit += () => SwitchState(takeDamageState);

        EnemyBaseState.onSwitchState +=  () => wanderState.ExitState(this);
        EnemyBaseState.onSwitchState +=() =>  chaseState.ExitState(this);
        EnemyBaseState.onSwitchState += () => attackState.ExitState(this);
        EnemyBaseState.onSwitchState += () => takeDamageState.ExitState(this);
        EnemyBaseState.onSwitchState += () => deathState.ExitState(this);
       
    }

    private void OnDisable()
    {
        WeaponBehavior.OnPlayerAttackHit -= () => SwitchState(takeDamageState);

        EnemyBaseState.onSwitchState -= () => wanderState.ExitState(this);
        EnemyBaseState.onSwitchState -= () => chaseState.ExitState(this);
        EnemyBaseState.onSwitchState -= () => attackState.ExitState(this);
        EnemyBaseState.onSwitchState -= () => takeDamageState.ExitState(this);
        EnemyBaseState.onSwitchState -= () => deathState.ExitState(this);
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
        animator.SetBool("isDead", isDead);
       

        currentState.UpdateState(this);

  

       
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


    public void CheckWhatNavMeshAgentIsOn()
    {
        NavMeshHit hit;

        agent.SamplePathPosition(NavMesh.AllAreas, 3f, out hit);
        
        areaMask = hit.mask;

    
    }


    public IEnumerator NewSearchForRandomDestination()
    {
        CheckWhatNavMeshAgentIsOn();

        NavMeshHit hit;

         //calculate a random path
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        randomPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
       

    
      if(NavMesh.SamplePosition(randomPoint, out hit, 500f, agent.areaMask))
      {

        Vector3 finalDestPoint = hit.position;

        agent.SetDestination(finalDestPoint);


        yield return new WaitUntil(() => Vector3.Distance(agent.transform.position, finalDestPoint) < 3);

        StartCoroutine(NewSearchForRandomDestination());

          Debug.Log("destination reached");


        yield break;

      }

      else
      {
        Debug.Log("a point was not found");
        yield break;
      }
        
    }



    //CHASE
    public void Chase()
    {
        agent.SetDestination(player.transform.position);
    }


    public IEnumerator Attack()
    {
        //if the enemy is not attacking or is dead...
        if (!isAttacking && canAttackAgain || !isDead)
        {
            //set is attacking to true
             isAttacking = true;
            animator.SetTrigger("Attack");

            //set to false
            canAttackAgain = false;
            isAttacking = false;


            yield return new WaitForSecondsRealtime(attackCoolDown);

            //set to true
            canAttackAgain = true;

            yield break;

        }
        //



    }


    public void EnableAttack()
    {
        enemyWeapon.damageBox.enabled = true;
    }

    public void DisableAttack()
    {
        enemyWeapon.damageBox.enabled = false;
    }

    public void PlayFootstepLeftSound()
    {
        SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.instance.footstepLeftClip, this.transform, false, 1f);
    }

    public void PlayFootstepRightSound() 
    {
        SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.instance.footstepRightClip, this.transform, false, 1f);
    }
}
