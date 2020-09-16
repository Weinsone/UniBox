﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Пѡлагаетсѧ вѡ использованїе всѣмъ тварѧмъ Божїимъ ѿ игрокове дѡ ботовъ.
    Только православная церковь признаёт игроков (людей) равными ботам (андроидам).
*/
public class Controller : MonoBehaviour
{
    private Vector3 movement;

    private CharacterController charController;
    private AnimationManager animationManager;

    public float speed, verticalSpeed, gravity, terminaVelocity, jumpForce, rotationSpeed; // заменить бы на int, чтоб быстрее работало
    private bool isJumping;
    public bool IsGrounded { get; private set; }

    public Vector3 eyeLevel, footOffset;

    private void Start() {
        charController = GetComponent<CharacterController>();
        animationManager = new AnimationManager(GetComponent<Animator>(), string.Empty);
    } 

    private void Update() {
        Fall(); // Если контроллер все время не тянуть вниз, даже когда он на земле, то charController.isGrounded начинает выдавать рандомное значение

        IsGrounded = charController.isGrounded; // Значение для следующего кадра
    }

    private void ApplyMovement() {
        charController.Move(movement * Time.deltaTime);
    }

    private void Fall() {
        if (IsGrounded) {
            if (verticalSpeed < gravity) { // Сброс verticalSpeed, если он был уменьшен через условие ниже
                verticalSpeed = gravity;
                animationManager.SetBoolValue("Falling", false);
            }
        } else {
            if (verticalSpeed > terminaVelocity) {
                verticalSpeed += gravity;
                animationManager.SetBoolValue("Falling", true);
            }
        }
        movement.y = verticalSpeed;
        ApplyMovement();
    }

    public void Move(float keyX, float keyY) {
        movement = Vector3.zero;
        movement.x = keyX * speed;
        movement.z = keyY * speed;

    // <старый кусок>
        // Transform targetCamera = GameLevel.LocalPlayerCamera.Camera.transform;
        // targetCamera.eulerAngles = new Vector3(0, targetCamera.eulerAngles.y, 0);
        // movement = targetCamera.TransformDirection(movement);

        // // if (tipaIsThirdPersonEnabled) {
        //     Quaternion dir = Quaternion.LookRotation(movement);
        //     transform.rotation = Quaternion.Lerp(transform.rotation, dir, rotationSpeed);
        // // }
    // </старый кусок>

        Transform targetCamera = GameLevel.LocalPlayerCamera.Camera.transform;
        targetCamera.eulerAngles = new Vector3(0, targetCamera.eulerAngles.y, 0);
        movement = targetCamera.TransformDirection(movement);

        // Quaternion dir = Quaternion.LookRotation(targetCamera);
        transform.rotation = Quaternion.Lerp(transform.rotation, GameLevel.LocalPlayerCamera.Camera.transform.rotation, rotationSpeed);

        Animate(keyX, keyY);
        ApplyMovement();
    }

    private void Animate(float x, float y) {
        animationManager.SetMovementValues(x, y);
    }

    private void OnAnimatorIK() {
        animationManager.AnimateIK(transform.forward, transform.rotation, footOffset);
    }

    public void Look(Vector3 dir) {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotationSpeed);
    }

    public void Jump() {
        if (IsGrounded) {
            verticalSpeed = jumpForce;
        } // else { DoubleJump() } :D
    }
}