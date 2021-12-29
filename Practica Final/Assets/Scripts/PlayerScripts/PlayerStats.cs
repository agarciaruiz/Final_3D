using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : CharacterStats
{
    [SerializeField] private PlayerHUD playerHUD;
    //[SerializeField] private AudioClip hurtClip;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerInput playerInput;
    private InputAction hurtAction;

    private void Start()
    {
        InitVariables();
        GetReferences();
    }

    private void GetReferences()
    {
        hurtAction = playerInput.actions["Hurt"];
    }

    public override void Die()
    {
        base.Die();
        animator.SetTrigger("IsDead");
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        playerHUD.UpdateHealth(health, maxHealth);
    }

    public override void CheckShield()
    {
        base.CheckShield();
        playerHUD.UpdateShield(shield, maxShield);
    }

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        StartCoroutine(HurtAnim());
    }

    private void LateUpdate()
    {
        if (hurtAction.triggered)
        {
            TakeDamage(30);
        }
    }

    IEnumerator HurtAnim()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Hurt Layer"), 1);
        animator.SetTrigger("Hurt");
        yield return new WaitForSeconds(0.9f);
        animator.SetLayerWeight(animator.GetLayerIndex("Hurt Layer"), 0);
    }

    public void EndGame()
    {
        ListenerMethods.ChangeScene(Scenes.gameOver);
    }
}
