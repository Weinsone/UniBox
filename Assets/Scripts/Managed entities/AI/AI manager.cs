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
        bool result = false;
        foreach (var target in Server.Targets) {
            if (target.position != from && Vector3.Distance(from, target.position) <= viewDistance) {
                Vector3 dirToTarget = target.position - from;
                float dot = Vector3.Dot(viewDirection, dirToTarget.normalized);
                if (dot < 1) {
                    float angleRadians = Mathf.Acos(dot);
                    result = angleRadians * Mathf.Rad2Deg <= viewAngle;
                    // Debug.Log($"Вы слышите голос с небес: {result}; Рептилоидный angle: {angleRadians}; Человеческий angle: {angleRadians * Mathf.Rad2Deg}\r\nЛол, а dot: {dot}");
                } else {
                    result = true;
                    // Debug.Log("ОДИН НАХУЙ, ПРОШЕЛ ЕБАТЬ ψ(._. )> " + dot);
                }
            }
            if (result) { // пхаха, это все для того чтобы TargetPosition = target.position два раза в коде не писать
                TargetPosition = target.position;
                return true;
            }
        }
        return false;
    }
}
