using System.Collections;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        if (enemy.isDead)
        {
            return;
        }


        enemy.StartCoroutine(enemy.Attack());
        
        
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
