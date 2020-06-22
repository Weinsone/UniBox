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
    private float speed = 5, gravity = -2, jumpForce = 5;

    void Start() {
        charController = GetComponent<CharacterController>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    } 

    // Падение и AI
    void Update() {
        if (!charController.isGrounded)
            Move(0, gravity, 0);
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
        Debug.Log("Char movement is " + movement);
    }

    public void Jump() {
        if (charController.isGrounded)
            Move(0, jumpForce, 0);
    }
}