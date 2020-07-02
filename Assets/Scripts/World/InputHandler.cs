using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Только для локального игрока
public static class InputHandler
{
    public static float HorizontalKeyInput {
        get {
            return Input.GetAxis("Horizontal");
        }
    }

    public static float VerticalKeyInput {
        get {
            return Input.GetAxis("Vertical");
        }
    }

    public static float HorizontalMouseInput {
        get {
            return Input.GetAxis("Mouse X") * 5 /* sensitivity */;
        }
    }

    public static float VerticalMouseInput {
        get {
            return Input.GetAxis("Mouse Y") * 5 /* sensitivity */;
        }
    }

    public static bool IsJumpKeyPressed() {
        if (Input.GetKey(KeyCode.Space)) {
            return true;
        } else {
            return false;
        }
    }
}