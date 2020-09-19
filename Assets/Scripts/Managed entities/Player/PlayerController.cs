using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Отвечает за передвижение ManagedEntity.EntytyModel
*/
public class PlayerController : MonoBehaviour, IController
{
    private Vector3 frameMovement;

    private CharacterController charController;
    private AnimationManager animationManager;

    public float Speed { get; set; }
    public float verticalSpeed, gravity, terminalVelocity, jumpForce, rotationSpeed; // заменить бы на int, чтоб быстрее работало
    public bool IsGrounded { get; private set; }

    public Vector3 EyeLevel { get; set; }
    public Vector3 footOffset;

    public void ApplySettings(EntitySettings settings) {
        Speed = settings.speed;
        gravity = GameSettings.Gravity;
        terminalVelocity = settings.terminalVelocity;
        jumpForce = settings.jumpForce;
        rotationSpeed = settings.rotationSpeed;

        EyeLevel = settings.eyeLevel;
        footOffset = settings.footOffset;

        frameMovement = Vector3.zero;

        charController = transform.gameObject.AddComponent<CharacterController>();
        charController.radius = settings.colliderRadius;
        charController.height = settings.colliderHeigh;
        charController.center = settings.colliderOffset;

        animationManager = new AnimationManager(GetComponent<Animator>(), string.Empty);
    }

    private void Update() {
        Fall(); // Если контроллер все время не тянуть вниз, даже когда он на земле, то charController.isGrounded начинает выдавать рандомное значение

        IsGrounded = charController.isGrounded; // Значение для следующего кадра
    }

    private void ApplyMovement() {
        charController.Move(frameMovement * Time.deltaTime);
    }

    private void Fall() {
        if (IsGrounded) {
            if (verticalSpeed < gravity) { // Сброс verticalSpeed, если он был уменьшен через условие ниже
                verticalSpeed = gravity;
                animationManager.SetBoolValue("Falling", false);
            }
        } else {
            if (verticalSpeed > terminalVelocity) {
                verticalSpeed += gravity;
                animationManager.SetBoolValue("Falling", true);
            }
        }
        frameMovement.y = verticalSpeed;
        ApplyMovement();
    }

    private void Move(float keyX, float keyY) {
        frameMovement.x = keyX * Speed;
        frameMovement.z = keyY * Speed;

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
        frameMovement = targetCamera.TransformDirection(frameMovement);

        // Quaternion dir = Quaternion.LookRotation(targetCamera);
        transform.rotation = Quaternion.Lerp(transform.rotation, GameLevel.LocalPlayerCamera.Camera.transform.rotation, rotationSpeed);

        AnimateMovement(keyX, keyY);
        ApplyMovement();
    }

    public void Goto(Vector3 position, bool immediately) {
        if (immediately) {
            charController.enabled = false;
            transform.position = position;
            charController.enabled = true;
        } else {
            Move(position.x, position.z);
        }
    }

    private void AnimateMovement(float x, float y) {
        animationManager.SetMovementValues(x, y);
    }

    public void SetAnimation(string animationName) {

    }

    private void OnAnimatorIK() {
        animationManager.AnimateIK(transform.forward, transform.rotation, footOffset);
    }

    public void Look(Vector3 direction) {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);
    }

    public void Jump() {
        if (IsGrounded) {
            verticalSpeed = jumpForce;
        } // else { DoubleJump() } :D
    }
}