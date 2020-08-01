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

    public void View(float x, float y) {
        if (!Input.GetKey(KeyCode.C)) { // Debug
            this.x += x;
            this.y += y;
            this.y = Mathf.Clamp(this.y, -90, 90);
            rotation = Quaternion.AngleAxis(this.x, Vector3.up) * Quaternion.AngleAxis(-this.y, Vector3.right);
        }
    }

    public void UpdatePosition(Vector3 target) {
        Camera.transform.position = target - (rotation * offset);
        Camera.transform.LookAt(target);
    }
}
