using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    /// <summary>
    /// Reference to main camera.
    /// </summary>
    private static Camera MainCamera
    {
        get
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }
            return _mainCamera;
        }
    }
    private static Camera _mainCamera;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Quaternion GetRotationTo(Vector2 target, Vector2 position, float offset = -90)
    {
        Vector2 myPos = position;
        Vector2 direction = target - myPos;
        direction.Normalize();

        return Quaternion.Euler(0, 0, (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + offset);        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Quaternion GetRotationToMouse(Vector2 position, float offset = -90)
    {
        return GetRotationTo(MainCamera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)), position, offset);
    }
}