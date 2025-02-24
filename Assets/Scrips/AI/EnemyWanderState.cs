using System.Collections;
using UnityEngine;

public class EnemyWanderState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("wander");
        enemy.StartCoroutine(enemy.NewSearchForRandomDestination());
    }

    public override IEnumerator EnumeratorState(EnemyStateManager enemy)
    {
        yield return null;
        //throw new System.NotImplementedException();
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        Debug.Log("Ending wander state...");
        enemy.StopCoroutine(enemy.NewSearchForRandomDestination());
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
       

        if (!enemy.playerInSight && !enemy.playerInAttackRange)
        {
            //enemy.NewSearchForRandomDestination();
        }

        if(enemy.playerInSight && !enemy.playerInAttackRange)
        {
            enemy.SwitchState(enemy.chaseState);
        }

        if (enemy.playerInSight && enemy.playerInAttackRange)
        {
            enemy.SwitchState(enemy.attackState);
        }
    }
}
