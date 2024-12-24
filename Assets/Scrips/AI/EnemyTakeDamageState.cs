using System.Collections;
using UnityEngine;

public class EnemyTakeDamageState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        //check to see if the enemy can be damaged
        if (enemy.canBeDamaged)
        {
            enemy.canBeDamaged = false;

            enemy.animator.SetTrigger("TakeDamage");
            enemy.health -= WeaponBehavior.damage;

            enemy.StartCoroutine(EnumeratorState(enemy));
        }

        return;
       
        
    }

    public override IEnumerator EnumeratorState(EnemyStateManager enemy)
    {
        //

        Debug.Log("Starting....");
        yield return new WaitForSecondsRealtime(enemy.damageTakenCooldown);
        Debug.Log("enemy can be damaged again");
        enemy.canBeDamaged=true;

        if(!enemy.isDead)
        {
            enemy.SwitchState(enemy.wanderState);
        }
        

        yield return null;
    }

    public override void ExitState()
    {
        Debug.Log("exit");
        //throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        return;
        //throw new System.NotImplementedException();
    }
}
