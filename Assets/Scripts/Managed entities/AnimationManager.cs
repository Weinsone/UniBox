using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager
{
    private Animator animator;

    public AnimationManager(Animator animator, string animationControllerName) {
        this.animator = animator;
        SetAnimationController(animationControllerName);
    }

    public void SetAnimationController(string animationControllerName) {

    }

    public void SetMovementValues(float x, float y) {
        // if (controller.IsGrounded) {
        //     animator.SetBool("IsGrounded", true);
            
        //     if (InputHandler.IsMovementKeyPressed) {
        //         animator.SetBool("Running", true);
        //     } else {
        //         animator.SetBool("Running", false);
        //     }
        // } else {
        //     animator.SetBool("IsGrounded", false);
        // }

        animator.SetFloat("X", x);
        animator.SetFloat("Y", y);
    }

    public void SetBool(string boolName, bool state) {
        animator.SetBool(boolName, state);
    }
}
