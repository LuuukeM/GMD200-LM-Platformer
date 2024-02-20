using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [SerializeField] private GameObject attackArea;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource punchSoundEffect;

    private bool attacking = false;

    private float timeToAttack = 0.25f;
    private float timer = 0f;

    void Start()
    {
        attackArea.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Attack();
            punchSoundEffect.Play();
        }
        
        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack) 
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(false);
            }
        }
    }
    void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(true);
        PlayAttackAnimation();
    }
}
