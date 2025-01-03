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

            Debug.Log("enemy is taking damage");
            enemy.animator.SetTrigger("TakeDamage");
            enemy.health -= WeaponBehavior.damage;

            //play the take damage sound
            SoundFXManager.instance.PlaySoundFXClipAtSetVolume(SoundFXManager.instance.characterHitClip, enemy.transform, false, 1f, 0.5f);

            if(enemy.health <= 0f)
            {
                enemy.SwitchState(enemy.deathState);
            }
            else
            {
                enemy.StartCoroutine(EnumeratorState(enemy));
            }

            
        }
        else if(!enemy.canBeDamaged)
        {
            return;
        }
        
    }

    public override IEnumerator EnumeratorState(EnemyStateManager enemy)
    {
        

        Debug.Log("Starting....");
        yield return new WaitForSecondsRealtime(enemy.damageTakenCooldown);
        Debug.Log("enemy can be damaged again");
        enemy.canBeDamaged=true;

        yield return null;
    }

    public override void ExitState(EnemyStateManager enemy)
    {
       enemy.StopCoroutine(EnumeratorState(enemy));
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        return;
    }
}
