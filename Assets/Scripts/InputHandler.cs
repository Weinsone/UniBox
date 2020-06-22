using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Только для локального игрока
public class InputHandler
{
    Controller playerController; // Local player

    public void Initialize(Client player) {
        playerController = player.GetController().GetComponent<Controller>(); // Получение контроллера локального игрока (конроллер - визуальное продествалени игрока)
    }

    public void ReadKeyInput() { 
        playerController.Move(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // 1 - нажата W, -1 - нажата S, аналогично с A, D

        if (Input.GetKey(KeyCode.Space)) {
            playerController.Jump();
        }
    }

    public void ReadMouseInput() {
        playerController.View(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}