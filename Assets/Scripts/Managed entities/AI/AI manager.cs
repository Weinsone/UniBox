using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
    Тупо помагает классам IBot думатб
*/
public static class AIManager
{
    public static NavMeshPath navMeshPath = new NavMeshPath();
    // public Vector3 targetPosition { get; private set; }

    public static bool IsEnemyInView(Vector3 from, Vector3 viewDirection, float viewAngle, float viewDistance, out Vector3 targetPosition) {
        bool result = false;
        foreach (var target in Server.Targets) {
            if (target.position != from && Vector3.Distance(from, target.position) <= viewDistance) {
                Vector3 dirToTarget = target.position - from;
                float dot = Vector3.Dot(viewDirection, dirToTarget.normalized);
                if (dot < 1) { // Если цель прямо перед носом
                    float angleRadians = Mathf.Acos(dot);
                    result = angleRadians * Mathf.Rad2Deg <= viewAngle;
                    // Debug.Log($"Вы слышите голос с небес: {result}; Рептилоидный angle: {angleRadians}; Человеческий angle: {angleRadians * Mathf.Rad2Deg}\r\nЛол, а dot: {dot}");
                } else {
                    result = true;
                }
            }
            if (result) { // пхаха, это все для того чтобы targetPosition = target.position два раза в коде не писать
                targetPosition = target.position;
                return true;
            }
        }
        targetPosition = Vector3.zero;
        return false;
    }

    public static Vector3[] GetPath(Vector3 startPosition, Vector3 targetPosition) {
        NavMesh.CalculatePath(startPosition, targetPosition, NavMesh.AllAreas, navMeshPath);
        return navMeshPath.corners;
    }

    public static Vector3 GetDirectionOfPath(Vector3 startPosition, Vector3 targetPosition) {
        Vector3[] path = GetPath(startPosition, targetPosition);
        if (path.Length > 1) { // Для построения направления нужно не меньше двух точек
            for (int i = 0; i < path.Length - 1; i++) {
                Debug.DrawLine(path[i], path[i + 1]);
            }

            Vector3 directionToPath = path[1] - path[0]; // path[0] - координаты начальной позиции; path[1] - координата направления (следующий элемент пути)
            return directionToPath.normalized;
        } else {
            Debug.LogWarning("Путь не найден");
            return Vector3.zero;
        }
    }
}
