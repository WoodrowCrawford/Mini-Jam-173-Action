using System.Collections;
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
        

        if (Time.unscaledTime - lastComboEnd > 0.2f && comboCounter <= combos.Count)
        {
            weapon.damageBox.enabled = true;

            CancelInvoke("EndCombo");

            if(Time.unscaledTime - lastClickedTime >= 0.5f)
            {
                animator.runtimeAnimatorController = combos[comboCounter]._animatorOV;
                animator.Play("Attack State", 0, 0);
                WeaponBehavior.damage= combos[comboCounter].damage;
                comboCounter++;
                lastClickedTime = Time.unscaledTime;

                if (comboCounter >= combos.Count)
                {
                    comboCounter = 0;
                }
            }
        }

        
    }

    public void ExitAttack()
    {

        


        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.65f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {

           weapon.damageBox.enabled = false;
            Invoke("EndCombo", 0.5f);

        }
    }

    public void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.unscaledTime;
    }

    public IEnumerator TakeDamage()
    {
        //die, then wait a second and reset the game
        animator.SetTrigger("Die");
        yield return null;
        
    }

}
