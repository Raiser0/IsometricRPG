using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim1 : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] PlayerMovement playerMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerMovement.EnemyDead) 
        {
            animator.SetBool("isDead", true);
        }
        else 
        { 
            animator.SetBool("isDead", false);
        }
    }
}