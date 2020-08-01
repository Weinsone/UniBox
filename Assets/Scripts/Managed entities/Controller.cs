﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Пѡлагаетсѧ вѡ использованїе всѣмъ тварѧмъ Божїимъ ѿ игрокове дѡ ботовъ.
    Только православная церковь признаёт игроков (людей) равными ботам (андроидам). Коннор был бы доволен...
    Поясняю странный коммент Ви выше: этот класс будет общим для игрока и AI ботов.
*/
public class Controller : MonoBehaviour
{
    private Vector3 movement;
    private CharacterController charController;
    public float speed, verticalSpeed, gravity, terminaVelocity, jumpForce, rotationSpeed; // заменить бы на int, чтоб быстрее работало
    public bool IsGrounded { get; private set; }
    public Vector3 eyeLevel;

    private void Start() {
        charController = GetComponent<CharacterController>();
    } 

    private void Update() {
        Falling();
        IsGrounded = charController.isGrounded;
    }

    private void ApplyMovement() {
        charController.Move(movement * Time.deltaTime);
    }

    private void Falling() {
        if (verticalSpeed > terminaVelocity) {
            verticalSpeed += gravity;
        }
        movement.y = verticalSpeed;
        ApplyMovement();
    }

    public void Move(float x, float z) {
        movement = Vector3.zero;
        movement.x = x * speed;
        movement.z = z * speed;

        Transform targetCamera = GameLevel.LocalPlayerCamera.Camera.transform;
        targetCamera.eulerAngles = new Vector3(0, targetCamera.eulerAngles.y, 0);
        movement = targetCamera.TransformDirection(movement);

        // if (tipaIsThirdPersonEnabled) {
            Quaternion dir = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, dir, rotationSpeed);
        // }

        ApplyMovement();
    }

    public void Jump() {
        if (IsGrounded) {
            verticalSpeed = jumpForce;
        } // else { DoubleJump() } :D
    }
}