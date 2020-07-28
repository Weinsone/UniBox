﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Тупо помагает классам IBot думатб
*/
public static class AIManager
{
    public static Vector3 EnemyPosition { get; private set; }

    public static bool IsEnemyInView(Vector3 from, Vector3 viewDirection, float viewAngle, float viewDistance) {
        foreach (var target in Server.Targets) {
            if (Vector3.Distance(from, target.position) <= viewDistance) {
                Vector3 dirToTarget = target.position - from;
                float dot = Vector3.Dot(viewDirection, dirToTarget.normalized);
                float angleRadians = Mathf.Acos(dot);
                if (angleRadians * Mathf.Rad2Deg < viewAngle) {
                    Debug.Log($"Dot: {dot}; Degree: {angleRadians * Mathf.Rad2Deg}; Player: {target.position}");
                    return true;
                }
            }
        }
        return false;
    }
}
