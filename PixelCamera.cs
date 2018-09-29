using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCamera : MonoBehaviour {

    public static float pixeltounits = 1f;
    public static float size = 5f;
    Vector2 res = new Vector2(160, 144);
    private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        if(camera.orthographic)
        {
            float direction = Screen.height;
            float resolution = res.y;
            size = direction / resolution;
            pixeltounits *= size;
            camera.orthographicSize = (direction / 2.0f) / pixeltounits;
        }
    }
}
