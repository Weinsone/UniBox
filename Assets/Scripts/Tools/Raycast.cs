using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Raycast
{
    private static RaycastHit hit;
    
    public static RaycastHit GetRayHit(Ray ray) {
        if (Physics.Raycast(ray, out hit)) {
            return hit;
        }
        Debug.LogError("Ваааааще не так чё-то");
        return default;
    }

    public static RaycastHit GetRayHit(Vector3 origin, Vector3 direction) { // эТА шТУкА ЧЕТО НЕ раБотаЕТ
        if (Physics.Raycast(origin, direction, out hit)) {
            return hit;
        }
        Debug.LogError("Чё-то не так");
        return default;
    }
}
