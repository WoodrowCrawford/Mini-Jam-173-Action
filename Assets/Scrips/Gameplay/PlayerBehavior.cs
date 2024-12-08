using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    [SerializeField] public WeaponBehavior weapon;
    public List<AttackSO> combos;
    public float lastClickedTime;
    public float lastComboEnd;

    public int comboCounter;

    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerInputBehavior.OnAttack += Attack;
    }

    private void OnDisable()
    {
        PlayerInputBehavior.OnAttack -= Attack;
    }

    private void Update()
    {
        ExitAttack();
    }

    public void Attack()
    {
        if (Time.time - lastComboEnd > 0.2f && comboCounter <= combos.Count)
        {
            CancelInvoke("EndCombo");

            if(Time.unscaledTime - lastClickedTime >= 0.2f)
            {
                animator.runtimeAnimatorController = combos[comboCounter]._animatorOV;
                animator.Play("Attack State", 0, 0);
                weapon.damage= combos[comboCounter].damage;
                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter >= combos.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    public void ExitAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1f *Time.unscaledTime);
        }
    }

    public void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.unscaledTime;
    }

}
