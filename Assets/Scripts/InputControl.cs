using UnityEngine;
using System.Collections;

public static class InputControl {
#if UNITY_EDITOR
    public static Vector3 GetMoveDirection() {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public static bool GetInvunerabilityUsed() {
        return Input.GetKeyDown(KeyCode.Space);
    }
#elif UNITY_ANDROID
    public static Vector3 GetMoveDirection() {
        return new Vector2(Input.acceleration.x, Input.acceleration.y);
    }
    public static bool GetInvunerabilityUsed() {
        return Input.touchCount >= 1;
    }
#endif
}
