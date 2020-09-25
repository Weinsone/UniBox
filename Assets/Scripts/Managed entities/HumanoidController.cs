using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Отвечает за передвижение ManagedEntity.EntytyModel
*/
public class HumanoidController : MonoBehaviour, IController
{
    [SerializeField]
    private Vector3 frameMovement;

    private CharacterController charController;
    private AnimationManager animationManager;

    public float acceleration, slowDownSpeed, speed, verticalSpeed, gravity, terminalVelocity, jumpForce, rotationSpeed; // заменить бы на int, чтоб быстрее работало
    public bool slowDown;
    public bool IsGrounded { get; private set; }

    public Vector3 EyeLevel { get; set; }
    public Vector3 footOffset;

    public Vector3 Velocity {
        get {
            return charController.velocity;
        }
    }

    public void ApplySettings(EntitySettings settings) {
        acceleration = settings.acceleration;
        slowDownSpeed = settings.slowDownSpeed;
        speed = settings.speed;
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
        if (slowDown) {
            frameMovement = Vector3.Lerp(frameMovement, Vector3.zero, slowDownSpeed * Time.deltaTime);
            AnimateMovement(transform.InverseTransformDirection(frameMovement).x, transform.InverseTransformDirection(frameMovement).z);
            if (frameMovement == Vector3.zero) {
                slowDown = false;
            }
        }
    }

    private void FixedUpdate() {
        IsGrounded = charController.isGrounded;
        Fall();
    }

    private void ApplyMovement() {
        charController.Move(frameMovement * Time.deltaTime);
    }

    private void Fall() {
        if (IsGrounded) {
            if (verticalSpeed < gravity - 0.1f) { // Сброс verticalSpeed, если он был уменьшен через условие ниже (0.1f - погрешность, для сравнения, т.к к float "=" хрень применишь)
                verticalSpeed = gravity;
                animationManager.SetBoolValue("Falling", false);
            }
        } else {
            if (verticalSpeed > terminalVelocity) {
                verticalSpeed += gravity;
                animationManager.SetBoolValue("Falling", true);
            }
        }
        frameMovement.y = verticalSpeed; // Если контроллер все время не тянуть вниз, даже когда он на земле, то charController.isGrounded начинает выдавать рандомное значение
        ApplyMovement();
    }

    public void MoveTowards(Vector3 direction) {
        slowDown = false;
        frameMovement.x = Mathf.Lerp(frameMovement.x, direction.x * speed, acceleration * Time.deltaTime);
        frameMovement.z = Mathf.Lerp(frameMovement.z, direction.z * speed, acceleration * Time.deltaTime);

        // float keyX = transform.InverseTransformDirection(direction).x, keyY = transform.InverseTransformDirection(direction).z;
        float keyX = transform.InverseTransformDirection(frameMovement).x, keyY = transform.InverseTransformDirection(frameMovement).z;
        AnimateMovement(keyX, keyY);

        ApplyMovement();

        // frameMovement.x = 0; frameMovement.z = 0;
    }

    private void AnimateMovement(float x, float y) {
        animationManager.SetMovementValues(x, y);
    }

    public void Teleport(Vector3 position) {

    }

    public void PlayAnimation(string animationName) {

    }

    private void OnAnimatorIK() {
        animationManager.AnimateIK(transform.forward, transform.rotation, footOffset);
    }

    private void OnRightFootStep() {
        animationManager.CastRightSurface();
    }

    public void OnLeftFootStep() {
        animationManager.CastLeftSurface();
    }

    public void LookAt(Vector3 target) {
        Vector3 direction = target - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);
    }

    public void Look(Quaternion direction) {
        transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotationSpeed);
    }

    public void Stop() {
        if (frameMovement.x != 0 && frameMovement.z != 0) {
            slowDown = true;
        }
    }

    public void Jump() {
        if (IsGrounded) {
            verticalSpeed = jumpForce;
        } // else { DoubleJump() } :D
    }
}