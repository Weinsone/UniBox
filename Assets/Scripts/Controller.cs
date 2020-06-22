using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Пѡлагаетсѧ вѡ использованїе всѣмъ тварѧмъ Божїимъ ѿ игрокове дѡ ботовъ.
// только православная церковь признаёт игроков (людей) равными ботам (андроидам). Коннор был бы доволен…
public class Controller : MonoBehaviour
{
    GameObject mainCamera;
    private Vector3 movement;
    private CharacterController charController;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private float speed = 5, verticalSpeed, gravity = -0.4f, terminaVelocity = -10, jumpForce = 15;

    void Start() {
        charController = GetComponent<CharacterController>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    } 

    // Падение и AI
    void Update() {
        if (verticalSpeed > terminaVelocity) {
            verticalSpeed += gravity;
        }

        Debug.Log("Vert Sp is " + verticalSpeed);
        Move(0, verticalSpeed, 0);

        isGrounded = charController.isGrounded; Debug.Log(isGrounded);
    }

    public void View(float x, float y) {
        mainCamera.transform.rotation = Quaternion.AngleAxis(x, Vector3.up) * Quaternion.AngleAxis(y, Vector3.right);
    }

    public void Move(float x, float y, float z) {
        movement = Vector3.zero;
        movement.x = x * speed;
        movement.y = y;
        movement.z = z * speed;
        charController.Move(movement * Time.deltaTime);
    }

    public void Jump() {
        Debug.Log($"Короче, слушай, божье слово: {isGrounded}");
        if (isGrounded) {
            // Move(0, verticalSpeed, 0);
            verticalSpeed = jumpForce;
        }
    }
}