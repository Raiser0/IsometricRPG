using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimControl : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) 
        {
            animator.SetBool("isWalk", true);
        }
        else 
        {
            animator.SetBool("isWalk", false);
        }
    }
}