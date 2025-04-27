using UnityEngine;

public class QuarterCamera
{
    private Vector3 _rotation;
    private Vector3 _offset;

    public QuarterCamera(CameraOffset offset)
    {
        _rotation = offset.RotationOffset;
        _offset = offset.PositionOffset;
    }

    public void UpdateCamera(Transform cameraTransform, Transform target)
    {
        cameraTransform.rotation = Quaternion.Euler(_rotation);
        cameraTransform.position = target.position + _offset;
    }
}