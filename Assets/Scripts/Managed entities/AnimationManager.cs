using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager
{
    private Animator animator;
    private Controller controller;

    public AnimationManager(Controller controller, Animator animator) {
        this.animator = animator;
        this.controller = controller;
    }

    public void Animate() {
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
    }
}
