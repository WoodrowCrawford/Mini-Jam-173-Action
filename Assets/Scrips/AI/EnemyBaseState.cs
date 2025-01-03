using System.Collections;
using UnityEngine;

public abstract class EnemyBaseState
{
    public delegate void Exit();
    public static Exit onSwitchState;

    public abstract void EnterState(EnemyStateManager enemy);

    public abstract void UpdateState(EnemyStateManager enemy);

    public abstract IEnumerator EnumeratorState(EnemyStateManager enemy);

    public abstract void ExitState(EnemyStateManager enemy);
}
