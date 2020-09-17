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
    public static bool IsMovementKeyPressed {
        get {
            HorizontalKeyInput = Input.GetAxis("Horizontal");
            VerticalKeyInput = Input.GetAxis("Vertical");

            if (HorizontalKeyInput != 0 || VerticalKeyInput != 0) {
                return true;
            } else {
                return false;
            }
        }
    }
    public static bool JumpInput {
        get {
            return Input.GetKey(KeyCode.Space); // можно попробовать GetButtonDown()
        }
    }

    public static bool GameplayMenuState { get; private set; }
    public static bool QuckMenuState { get; private set; }
    public static bool IsGameplayMenuKeyPressed {
        get {
            GameplayMenuState = Input.GetKey(KeyCode.Q);
            QuckMenuState = Input.GetKey(KeyCode.C);
            if (GameplayMenuState || QuckMenuState) {
                return true;
            } else {
                return false;
            }
        }
    }

    // Для мышки нужно тоже саме сделать, как в IsMovementKeyPressed
    public static Vector3 MousePosition {
        get {
            return Input.mousePosition;
        }
    }
    public static bool IsLeftMouseKeyPressed {
        get {
            return Input.GetKey(KeyCode.Mouse0);
        }
    }
    public static bool IsRightMouseKeyPressed {
        get {
            return Input.GetKey(KeyCode.Mouse1);
        }
    }
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