﻿using UnityEngine;
using System.Collections;

public static class InputControl {
    private static Matrix4x4 calibrationMatrix;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
    public static Vector3 GetMoveDirection() {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public static bool GetInvunerabilityUsed() {
        return Input.GetKeyDown(KeyCode.Space);
    }
#elif UNITY_ANDROID

    static Vector3 accelerometerInput;
    public static void calibrateAccelerometer() {
        Vector3 wantedDeadZone = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), wantedDeadZone);
        //create identity matrix ... rotate our matrix to match up with down vec
        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotateQuaternion, new Vector3(1f, 1f, 1f));
        //get the inverse of the matrix
        calibrationMatrix = matrix.inverse;
    }

    public static Vector3 GetMoveDirection() {
        float interpolationTime = 0.07f;
        
        accelerometerInput = Vector3.Lerp(accelerometerInput, calibrationMatrix.MultiplyVector(Input.acceleration), Time.deltaTime/interpolationTime);
        if (accelerometerInput.sqrMagnitude > 1.0f) accelerometerInput.Normalize();

        return accelerometerInput;
    }
    public static bool GetInvunerabilityUsed() {
        return Input.touchCount >= 1;
    }

#endif
}
