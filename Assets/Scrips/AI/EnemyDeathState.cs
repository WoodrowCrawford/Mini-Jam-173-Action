using System.Collections;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.animator.SetTrigger("Die");
        enemy.isDead = true;
        enemy.agent.isStopped = true;
        enemy.canBeDamaged = false;
    }

    public override IEnumerator EnumeratorState(EnemyStateManager enemy)
    {

        yield return null;
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        return;
        //throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        return;
        //throw new System.NotImplementedException();
    }
}
