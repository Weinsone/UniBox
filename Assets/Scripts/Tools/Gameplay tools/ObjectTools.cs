using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolList
{
    mover, // +
    vertexSnap, // +
    fixJoint, // -
    hingeJount // -
}

public static class ObjectTools
{
    public static ToolList SelectedTool { get; private set; } = ToolList.mover;

    public static List<GameObject> CreatedObjects { get; private set; } = new List<GameObject>();
    public static float animationSpeed = 20f;

    // Tools properties
    private static bool isImplement;
    private static GameObject movingObject;
    private static Vector3 offset;

    public static void SetTool(ToolList tool) {
        SelectedTool = tool;
    }

    public static void Implement(bool toolState) {
        switch (SelectedTool) {
            case ToolList.mover:
                Mover.Move(toolState);
                break;
            case ToolList.vertexSnap:
                VertexSnap.Snap(toolState);
                break;
        }
    }

    public static void Spawn(GameObject gameObject) {
        MonoBehaviour.Instantiate(
            original: gameObject,
            position: GameLevel.LocalPlayer.Controller.transform.position + GameLevel.LocalPlayer.Controller.transform.forward,
            gameObject.transform.rotation
        );
        CreatedObjects.Add(gameObject);
    }

    private static class Mover
    {
        public static void Move(bool move) {
            RaycastHit hit = Raycast.GetHit(PlayerMenu.CursorDirection);

            if (hit.collider != null) {
                if (isImplement) {
                    if (move) {
                        movingObject.transform.position = Vector3.Lerp(movingObject.transform.position, hit.point + offset, animationSpeed * Time.deltaTime);
                    } else {
                        movingObject.layer = default;
                        isImplement = false;
                    }
                } else {
                    if (move) {
                        movingObject.layer = 2;
                        isImplement = true;
                    } else {
                        movingObject = hit.collider.gameObject;
                        offset = movingObject.transform.position - hit.point;
                    }
                }
            }
        }
    }

    private static class VertexSnap
    {
        public static float radius = 0.3f;
        private static GameObject targetObject; // moveObject - что двигать, targetObject - к чему двигать

        public static void Snap(bool grabObject) {
            RaycastHit hit = Raycast.GetHit(PlayerMenu.CursorDirection);

            if (hit.collider != null) {
                targetObject = hit.collider.gameObject;

                if (isImplement) {
                    if (grabObject) {
                        movingObject.transform.position = Vector3.Lerp(movingObject.transform.position, GetNearestVertex(hit.point) + offset, animationSpeed * Time.deltaTime);
                    } else {
                        movingObject.layer = default;
                        isImplement = false;
                    }
                } else {
                    if (grabObject) {
                        movingObject = targetObject;
                        movingObject.layer = 2;
                        isImplement = true;
                    } else {
                        offset = targetObject.transform.position - GetNearestVertex(hit.point);
                    }
                }
            }
        }

        private static Vector3 GetNearestVertex(Vector3 hitPoint) {
            Vector3[] vertices = targetObject.GetComponent<MeshFilter>().mesh.vertices;
            PlayerMenu.DrawPoints(vertices);
            for (int i = 0; i < vertices.Length; i++) {
                Vector3 vertex = targetObject.transform.TransformPoint(vertices[i]);
                if (Vector3.Distance(hitPoint, vertex) < radius) {
                    return vertex;
                }
            }
            return hitPoint;
        }
    }
}
