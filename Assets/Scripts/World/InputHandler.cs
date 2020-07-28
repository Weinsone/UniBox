using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Только для локального игрока
*/
public static class InputHandler
{
    // private static float horizontalMouseInput, verticalMouseInput;

    public static float HorizontalKeyInput  { get; private set; }
    public static float VerticalKeyInput  { get; private set; }
    public static bool JumpInput { get; private set; }
    public static bool IsMovementKeyPressed {
        get {
            HorizontalKeyInput = Input.GetAxis("Horizontal");
            VerticalKeyInput = Input.GetAxis("Vertical");
            JumpInput = Input.GetKey(KeyCode.Space); // можно попробовать GetButtonDown()

            if (HorizontalKeyInput != 0 || VerticalKeyInput != 0) {
                return true;
            } else {
                return false;
            }
        }
    }
    // Для мышки нужно тоже саме сделать, как в IsMovementKeyPressed
    public static float HorizontalMouseInput {
        get {
            return Input.GetAxis("Mouse X") * 5 /* * sensitivity */;
        }
    }
    public static float VerticalMouseInput {
        get {
            return Input.GetAxis("Mouse Y") * 5 /* * sensitivity */;
        }
    }
}