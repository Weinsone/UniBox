using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectTools
{
    public static float animationSpeed = 20f;
    public static List<GameObject> CreatedObjects { get; private set; } = new List<GameObject>();

    public static void Spawn(GameObject gameObject) {
        MonoBehaviour.Instantiate(
            original: gameObject,
            position: GameLevel.LocalPlayer.Controller.transform.position + GameLevel.LocalPlayer.Controller.transform.forward,
            gameObject.transform.rotation
        );
        CreatedObjects.Add(gameObject);
    }

    public static void Move(bool grabObject, bool vertexSnap) {
        if (vertexSnap) {
            VertexSnap.Snap(grabObject);
        } else {
            // Обычное перемещение объекта
        }
    }

    private static class VertexSnap
    {
        public static float radius = 0.3f;
        private static bool isSnapping;
        private static GameObject movingObject, targetObject; // moveObject - что двигать, targetObject - к чему двигать
        private static Vector3 distance; // Дистанция между вертексом объекта и центром объекта

        public static void Snap(bool grabObject) {
            RaycastHit hit = Raycast.GetHit(PlayerMenu.CursorDirection);

            if (hit.collider != null) {
                targetObject = hit.collider.gameObject;

                if (isSnapping) {
                    if (grabObject) {
                        movingObject.transform.position = Vector3.Lerp(movingObject.transform.position, GetNearestVertex(hit.point) + distance, animationSpeed * Time.deltaTime);
                    } else {
                        movingObject.layer = default;
                        isSnapping = false;
                    }
                } else {
                    if (grabObject) {
                        movingObject = targetObject;
                        movingObject.layer = 2;
                        isSnapping = true;
                    } else {
                        distance = targetObject.transform.position - GetNearestVertex(hit.point);
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
