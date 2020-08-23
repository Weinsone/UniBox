using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager
{
    private Animator animator;
    private Controller controller; // бэээ

    public AnimationManager(Controller controller, Animator animator) {
        this.animator = animator;
        this.controller = controller;
    }

    public void Animate(float x, float y) {
        if (controller.IsGrounded) {
            animator.SetBool("IsGrounded", true);
            
            if (InputHandler.IsMovementKeyPressed) {
                animator.SetBool("Running", true);
            } else {
                animator.SetBool("Running", false);
            }
        } else {
            animator.SetBool("IsGrounded", false);
        }

        // animator.SetFloat("X", x);
        // animator.SetFloat("Y", y);
    }
}
