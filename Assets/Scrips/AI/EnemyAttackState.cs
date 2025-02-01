using System.Collections;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        //first check if the enemy is dead
        if (!enemy.isDead)
        {
            //then check to see if the player is able to take damage 
            if(!PlayerInputBehavior.playerIsInvulnerable)
            {
                enemy.StartCoroutine(enemy.Attack());
            }

            return;
        }



        return;
    }

    public override IEnumerator EnumeratorState(EnemyStateManager enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        enemy.StopCoroutine(enemy.Attack());
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (!enemy.playerInSight && !enemy.playerInAttackRange)
        {
            enemy.SwitchState(enemy.wanderState);
        }

        if (enemy.playerInSight && !enemy.playerInAttackRange)
        {
            enemy.SwitchState(enemy.chaseState);
        }

        if (enemy.playerInSight && enemy.playerInAttackRange && enemy.canAttackAgain && !enemy.isDead)
        {
            enemy.StartCoroutine(enemy.Attack());
        }
    }
}
