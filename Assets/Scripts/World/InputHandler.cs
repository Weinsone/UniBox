using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Только для локального игрока
public static class InputHandler
{
    private static Controller playerController; // Local player
    private static CameraController playerCamera;

    public static void Initialize(Client player, CameraController camera) {
        playerController = player.GetController().GetComponent<Controller>(); // Получение контроллера локального игрока (конроллер - визуальное продестваление игрока)
        playerCamera = camera;
    }

    public static void ReadKeyInput() { 
        playerController.Move(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // 1 - нажата W, -1 - нажата S, аналогично с A, D

        if (Input.GetKey(KeyCode.Space)) {
            playerController.Jump();
        }
    }

    public static void ReadMouseInput() {
        playerCamera.View(Input.GetAxis("Mouse X") * 5 /* sensitivity */, Input.GetAxis("Mouse Y") * 5 /* sensitivity */); // НАСТРОИТЬ SENSITIVITY!
        playerCamera.UpdatePosition(playerController.transform.position);
    }
}