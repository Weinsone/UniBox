using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Тупо помагает классам IBot думатб
*/
public class AIManager
{
    public Vector3 TargetPosition { get; private set; }

    public bool IsEnemyInView(Vector3 from, Vector3 viewDirection, float viewAngle, float viewDistance) {
        foreach (var target in Server.Targets) {
            if (Vector3.Distance(from, target.position) <= viewDistance) {
                Vector3 dirToTarget = target.position - from;
                float dot = Vector3.Dot(viewDirection, dirToTarget.normalized);
                float angleRadians = Mathf.Acos(dot);
                if (angleRadians * Mathf.Rad2Deg < viewAngle) {
                    TargetPosition = target.position;
                    // Debug.Log($"Dot: {dot}; Degree: {angleRadians * Mathf.Rad2Deg}; Player: {target.position}");
                    Debug.Log("Видно");
                    return true;
                }
            }
        }
        Debug.Log("Не видно");
        return false;
    }
}
