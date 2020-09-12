using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerMenu
{
    public static Canvas screenCanvas;

    public static void ShowCursor() {
        Cursor.visible = true;
    }

    public static void HideCursor() {
        Cursor.visible = false;
    }

    public static void ShowContextMenu() {

    }

    public static void ShowQuickMenu() {
        if (InputHandler.IsLeftMouseKeyPressed) {
            DetectUI();
            Debug.Log("Eue!");
        }
    }

    public static void ShowGameplayMenu() {

    }

    private static void DetectUI() {
        GameObject foundObject = Raycast.GetUIHit(Input.mousePosition);
        if (foundObject != null) {
            GameLevel.LocalPlayer.usingComputer = foundObject.GetComponentInParent<VirtualMachine>();
        }
    }
}
