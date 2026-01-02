using UnityEngine;

public class MouseUtil
{
    private static readonly Camera camera = Camera.main;

    public static Vector3 GetMousePositionInWorldSpace(float zValue = 0f)
    {
        Plane dragPlane = new(camera.transform.forward, new Vector3(0, 0, zValue));
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out var distance)) return ray.GetPoint(distance);
        return Vector3.zero;
    }
}