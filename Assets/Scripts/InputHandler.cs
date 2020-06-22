using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Только для локального игрока
public class InputHandler
{
    Controller playerController; // Local player

    public void Initialize(Client player) { // гглянь в GameLevel
        playerController = player.GetController().GetComponent<Controller>(); // Получение контроллера локального игрока (конроллер - визуальное продествалени игрока)
    }

    public void ReadInput() { 
        playerController.Move(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // 1 - нажата W, -1 - нажата S, аналогично с A, D
        Debug.Log(Input.GetAxis("Horizontal")); Debug.Log(Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.Space)) {
            playerController.Jump();
            Debug.Log("Вы дрочите призраку в воздухе, подпрыгивая, чтобы дотянуться до его пениса " + Input.GetKey(KeyCode.Space));
        }
    }

    public void ReadViewInput() {
        playerController.View(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Debug.Log(Input.GetAxis("Mouse X")); Debug.Log(Input.GetAxis("Mouse Y"));
    }
}