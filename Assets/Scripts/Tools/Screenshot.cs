using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Screenshot
{
    public static Texture2D CaptureIcon(GameObject target) {
        target = MonoBehaviour.Instantiate(target);
        Texture2D result = Capture(GameObject.Find("Capture camera").GetComponent<Camera>(), 128, 128);
        
        target.GetComponent<MeshRenderer>().enabled = false;
        MonoBehaviour.Destroy(target);
        return result;
    }

    public static Texture2D Capture(Camera captureCamera, int width, int height) {
        RenderTexture targetTexture = RenderTexture.GetTemporary(width, height);
        Texture2D result = new Texture2D(width, height, TextureFormat.RGB24, false);

        captureCamera.targetTexture = targetTexture;
        
        captureCamera.Render();
        RenderTexture.active = targetTexture;

        result.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);
        result.Apply();
        return result;
    }
}
