using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager
{
    public Animator animator;

    // IK bones
    public Transform rootBone;
    public Transform rightUpperLeg, leftUpperLeg;
    public Transform rightLegDown, leftLegDown;
    public Transform rightFoot, leftFoot;

    // IK properties
    private float rightWeight, leftWeight;
    private Quaternion rightFootRotation, leftFootRotation, latestRightFootRoatation, latestLeftFootRotation;
    private Vector3 latestRightKneeDirection, latestLeftKneeDirection;
    private RaycastHit rightSurface, leftSurface;

    public AnimationManager(Animator animator, string animationControllerName) {
        this.animator = animator;
        SetAnimationController(animationControllerName);

        rootBone = animator.GetBoneTransform(HumanBodyBones.Spine);
        rightUpperLeg = animator.GetBoneTransform(HumanBodyBones.RightUpperLeg);
        leftUpperLeg = animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
        rightLegDown = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);
        leftLegDown = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
        rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);
        leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
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

    public void SetBoolValue(string boolName, bool state) {
        animator.SetBool(boolName, state);
    }

    public void AnimateIK(Vector3 controllerDirection, Quaternion controllerRotation, Vector3 controllerFootOffset) {
        rightWeight = animator.GetFloat("RightFootIK");
        leftWeight = animator.GetFloat("LeftFootIK");

        rightFootRotation = default;
        leftFootRotation = default;

        if (rightWeight < 0.8f) {
            rightFootRotation = controllerRotation;
            latestRightFootRoatation = rightFootRotation;
            latestRightKneeDirection = controllerDirection;
        } else {
            rightFootRotation = latestRightFootRoatation;
        }
        if (leftWeight < 0.8f) {
            leftFootRotation = controllerRotation;
            latestLeftFootRotation = leftFootRotation;
            latestLeftKneeDirection = controllerDirection;
        } else {
            leftFootRotation = latestLeftFootRotation;
        }

        rightSurface = Raycast.GetHit(new Ray(rightFoot.position, Vector3.down));
        leftSurface = Raycast.GetHit(new Ray(leftFoot.position, Vector3.down));

        Debug.DrawLine(rightSurface.point, rightSurface.point + rightSurface.normal);

        SetIKPosition(controllerFootOffset);
        SetIKHintPosition();
    }

    private void SetIKPosition(Vector3 controllerFootOffset) {
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightWeight);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftWeight);

        animator.SetIKPosition(AvatarIKGoal.RightFoot, rightSurface.point + controllerFootOffset);
        animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftSurface.point + controllerFootOffset);
        animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.FromToRotation(Vector3.up, rightSurface.normal) * rightFootRotation);
        animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.FromToRotation(Vector3.up, leftSurface.normal) * leftFootRotation);
    }

    private void SetIKHintPosition() {
        animator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, rightWeight);
        animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, leftWeight);

        animator.SetIKHintPosition(AvatarIKHint.RightKnee, rightSurface.point + latestRightKneeDirection);
        animator.SetIKHintPosition(AvatarIKHint.LeftKnee, leftSurface.point + latestLeftKneeDirection);
    }
}
