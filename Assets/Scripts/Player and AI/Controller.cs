using System.Collections;
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
    [SerializeField] private bool isGrounded, isThirdPerson;
    [SerializeField] private float speed = 5, verticalSpeed, gravity = -0.4f, terminaVelocity = -10, jumpForce = 15, rotationSpeed = 5; // заменить бы на int, чтоб быстрее работало

    void Start() {
        charController = GetComponent<CharacterController>();
    } 

    // Падение и AI
    void Update() {
        if (verticalSpeed > terminaVelocity) {
            verticalSpeed += gravity;
        }
        // Debug.Log("Vert Sp is " + verticalSpeed);
        Move(0, verticalSpeed, 0);
        isGrounded = charController.isGrounded;
    }

    public void Move(float x, float y, float z) {
        movement = Vector3.zero;
        movement.x = x * speed;
        movement.y = y;
        movement.z = z * speed;

        Transform targetCamera = GameLevel.PlayerCamera.Camera.transform;
        targetCamera.eulerAngles = new Vector3(0, targetCamera.eulerAngles.y, 0);
        movement = targetCamera.TransformDirection(movement);
        Debug.Log($"Movement is {movement}");

        Quaternion dir = Quaternion.LookRotation(movement);
        transform.rotation = Quaternion.Lerp(transform.rotation, dir, rotationSpeed * Time.deltaTime);

        charController.Move(movement * Time.deltaTime);
    }

    public void Jump() {
        if (isGrounded) {
            verticalSpeed = jumpForce;
        }
    }
}