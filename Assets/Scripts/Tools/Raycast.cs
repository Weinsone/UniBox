using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Raycast
{
    private static RaycastHit hit;
    private static PointerEventData pointerData = new PointerEventData(EventSystem.current);

    public static bool GetHitGameObjectByTag(Ray ray, string tag, out GameObject foundObject, int layer = 1 << 0) {
        hit = GetHit(ray);
        Debug.Log(hit.collider.tag);
        if (hit.collider != null && hit.collider.tag == tag) {
            foundObject = hit.collider.gameObject;
            return true;
        } else {
            foundObject = null;
            return false;
        }
    }

    public static RaycastHit GetHit(Ray ray, int layer = 1 << 0) {
        if (Physics.Raycast(ray, out hit)) {
            return hit;
        }
        Debug.LogError("Ваааааще не так чё-то");
        return default;
    }

    public static RaycastHit GetHit(Vector3 origin, Vector3 direction, int layer = 1 << 0) { // эТА шТУкА ЧЁТО НЕ раБотаЕТ
        if (Physics.Raycast(origin, direction, out hit)) {
            return hit;
        }
        Debug.LogError("Чё-то не так");
        return default;
    }

    public static GameObject GetUIHit(Vector3 cursorPosition) {
        pointerData.position = cursorPosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        if (results.Count > 0) {
             return results[0].gameObject;
        }
        return null;
    }
}
