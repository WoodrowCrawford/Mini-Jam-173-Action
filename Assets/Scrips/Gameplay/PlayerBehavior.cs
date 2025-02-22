using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public delegate void PlayerEventHandler();

    public static event PlayerEventHandler onDeath;
    public static event PlayerEventHandler onHealthChangedCallback;

    public static PlayerBehavior instance;

    [SerializeField] public WeaponBehavior weapon;
    public Animator animator;

    [Header("Player values")]
    public int health = 3;
    public int maxHealth;
    
   
    public int maxTotalHealth;

    public bool isDead = false;
    public bool canBeDamaged = false;
    public float invulnerabilityTime = 1f;

    [Header("Combo values")]
    public List<AttackSO> combos;
    public float lastClickedTime;
    public float lastComboEnd;
    public int comboCounter;

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }

        
    }

    private void OnEnable()
    {
        onDeath += Die;

        PlayerInputBehavior.OnAttack += Attack;
        EnemyDeathState.onEnemyKilledWithTimeSlow += () => Heal(1);

        //wave clear rewards
        Wave1AreaBehavior.onWave1Ended += AddHealth;
        Wave2AreaBehavior.onWave2Ended += AddHealth;
        Wave3AreaBehavior.onWave3Ended += AddHealth;
        Wave4AreaBehavior.onWave4Ended += AddHealth;
    }

    private void OnDisable()
    {
        onDeath -= Die;

        PlayerInputBehavior.OnAttack -= Attack;
        EnemyDeathState.onEnemyKilledWithTimeSlow -= () => Heal(1);

        Wave1AreaBehavior.onWave1Ended -= AddHealth;
        Wave2AreaBehavior.onWave2Ended -= AddHealth;
        Wave3AreaBehavior.onWave3Ended -= AddHealth;
        Wave4AreaBehavior.onWave4Ended -= AddHealth;
    }



    private void Start()
    {
        animator = GetComponent<Animator>();
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
                //play attack sound
                SoundFXManager.instance.PlaySoundFXClipAtSetVolume(SoundFXManager.instance.swordHitClip, this.transform, false, 1f, 0.5f);

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

    public void Die()
    {
        animator.SetTrigger("Die");
        Debug.Log("Die");   
    }

    public IEnumerator TakeDamage(int damage)
    {
        //first check if they can take damage
        if(canBeDamaged || !PlayerInputBehavior.playerIsInvulnerable)
        {
            //if true, check to see if the player is already dead

            if (!isDead)
            {

                //take damage
                health = health - damage;
                ClampHealth();

                //play take damage sound
                SoundFXManager.instance.PlaySoundFXClipAtSetVolume(SoundFXManager.instance.characterHitClip, this.transform, false, 1f, 0.5f);
                

                if(health <= 0)
                {
                    health = 0;
                    onDeath?.Invoke();
                    Debug.Log("Do death stuff");
                }

                canBeDamaged = false;
                yield return new WaitForSecondsRealtime(invulnerabilityTime);
                canBeDamaged = true;

               
            }
        }
        //if false, return false
        yield break;

    }

    public void PlayFootstepLeftSound()
    {
        SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.instance.footstepLeftClip, this.transform, false, 1f);
    }

    public void PlayFootstepRightSound()
    {
        SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.instance.footstepRightClip, this.transform, false, 1f);
    }


    public void Heal(int health)
    {
        SoundFXManager.instance.PlaySoundFXClipAtSetVolume(SoundFXManager.instance.healClip, this.transform, false, 1f, 1f);

        this.health += health;
        ClampHealth();
    }

    public void AddHealth()
    {
        if (maxHealth < maxTotalHealth)
        {
            maxHealth += 1;
            health = maxHealth;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }
}
