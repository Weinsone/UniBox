using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController
{
    public GameObject Camera { get; private set; }
    public Vector3 offset = new Vector3(0, 0, 3);

    private Quaternion rotation;
    private float x, y;

    public CameraController(GameObject camera) {
        Camera = camera;
    }

    public void UpdateViewDirection(float x, float y) {
        this.x += x;
        this.y += y;
        this.y = Mathf.Clamp(this.y, -90, 90);
        rotation = Quaternion.AngleAxis(this.x, Vector3.up) * Quaternion.AngleAxis(-this.y, Vector3.right);
    }

    public void UpdatePosition(Vector3 positionTarget) {
        if (GameLevel.LocalPlayer.isUseComputer) {
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, GameLevel.LocalPlayer.usingComputer.computerCanvas.transform.position - GameLevel.LocalPlayer.usingComputer.computerCanvas.transform.forward, 5 *Time.deltaTime);
            Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, GameLevel.LocalPlayer.usingComputer.computerCanvas.transform.rotation, 5 * Time.deltaTime);
        } else {
            Camera.transform.position = positionTarget - (rotation * offset);
            Camera.transform.LookAt(positionTarget);
        }
    }
}
