using System.Collections;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Chase");
    }

    public override IEnumerator EnumeratorState(EnemyStateManager enemy)
    {
        yield break;
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        Debug.Log("Exiting chase state");
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
