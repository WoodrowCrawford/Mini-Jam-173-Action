using System.Collections;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public delegate void EnemyDeathStateDelegate();

    public static event EnemyDeathStateDelegate onEnemyKilledWithTimeSlow;
    public static event EnemyDeathStateDelegate onEnemyDeath;

    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.isDead = true;
        EnemyDeathState.onEnemyDeath?.Invoke();

        //stop all corutines from running when the enemy dies
        enemy.StopAllCoroutines();

        //check if the enemy was killed in slow motion
        if (TimeManipulationBehavior.isTimeSlowed)
        {
           onEnemyKilledWithTimeSlow?.Invoke();
        }

        enemy.animator.SetTrigger("Die");

        enemy.agent.isStopped = true;
        enemy.canBeDamaged = false;
    }

    public override IEnumerator EnumeratorState(EnemyStateManager enemy)
    {
        yield return null;
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy is exiting the death state");
        //throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

        return;
     
    }
}
