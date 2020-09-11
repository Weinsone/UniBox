using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Raycast
{
    public static RaycastHit GetRayHit(Ray ray, int layer = 1 << 0) {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            return hit;
        }
        Debug.LogError("Ваааааще не так чё-то");
        return default;
    }

    public static RaycastHit GetRayHit(Vector3 origin, Vector3 direction, int layer = 1 << 0) { // эТА шТУкА ЧЁТО НЕ раБотаЕТ
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit)) {
            return hit;
        }
        Debug.LogError("Чё-то не так");
        return default;
    }
}
