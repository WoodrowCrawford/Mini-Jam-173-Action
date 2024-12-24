using System.Collections;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.Attack();
    }

    public override IEnumerator EnumeratorState(EnemyStateManager enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (!enemy.playerInSight && !enemy.playerInAttackRange)
        {
            enemy.SwitchState(enemy.wanderState);
        }

        if (enemy.playerInSight && !enemy.playerInAttackRange)
        {
            enemy.Chase();
        }

        if (enemy.playerInSight && enemy.playerInAttackRange)
        {
            enemy.SwitchState(enemy.attackState);
        }
    }
}
