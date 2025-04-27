using System;
using UnityEngine;
using static UnityEditor.SceneView;

public enum ECameraType
{
    FirstPerson,
    ThirdPerson,
    QuarterView
}
[Serializable]
public struct CameraOffset
{
    public Vector3 PositionOffset;
    public Vector3 RotationOffset;
}

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] 
    private Transform target;
    [SerializeField] 
    private CameraOffset firstPersonOffset;
    [SerializeField] 
    private CameraOffset thirdPersonOffset;
    [SerializeField] 
    private CameraOffset quarterViewOffset;
    [SerializeField] 
    private float rotationSpeed = 300f;

    private ECameraType _currentCameraType = ECameraType.FirstPerson;
    public ECameraType CurrentCameraType => _currentCameraType;

    private FPSCamera firstPerson;
    private TPSCamera _thirdPerson;
    private QuarterCamera _quarterView;

    private float _rotationX;
    private float _rotationY;

    public static event Action<ECameraType> OnCameraTypeChanged;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        firstPerson = new FPSCamera(firstPersonOffset.PositionOffset, rotationSpeed);
        _thirdPerson = new TPSCamera(thirdPersonOffset, ref _rotationX, ref _rotationY, rotationSpeed);
        _quarterView = new QuarterCamera(quarterViewOffset);
    }

    private void Update()
    {
        HandleInput();

        switch (_currentCameraType)
        {
            case ECameraType.FirstPerson:
                firstPerson.UpdateCamera(transform, target);
                break;
            case ECameraType.ThirdPerson:
                _thirdPerson.UpdateCamera(transform, target);
                break;
            case ECameraType.QuarterView:
                _quarterView.UpdateCamera(transform, target);
                break;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SetCameraType(ECameraType.FirstPerson);
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SetCameraType(ECameraType.ThirdPerson);
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetCameraType(ECameraType.QuarterView);
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.F1)) Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetKeyDown(KeyCode.F2)) Cursor.lockState = CursorLockMode.None;
    }

    private void SetCameraType(ECameraType newType)
    {
        _currentCameraType = newType;
        OnCameraTypeChanged?.Invoke(newType); // 이벤트 발행
    }

    private void OnDrawGizmos()
    {
        if (target == null) return;

        // FPS Offset
        Gizmos.color = Color.red;
        Vector3 fpsPosition = target.position + firstPersonOffset.PositionOffset;
        Gizmos.DrawSphere(fpsPosition, 0.1f);
        Gizmos.DrawLine(target.position, fpsPosition);

        // TPS Offset
        Gizmos.color = Color.green;
        Vector3 tpsPosition = target.position + thirdPersonOffset.PositionOffset;
        Gizmos.DrawSphere(tpsPosition, 0.1f);
        Gizmos.DrawLine(target.position, tpsPosition);

        // Quarter View Offset
        Gizmos.color = Color.blue;
        Vector3 quarterPosition = target.position + quarterViewOffset.PositionOffset;
        Gizmos.DrawSphere(quarterPosition, 0.1f);
        Gizmos.DrawLine(target.position, quarterPosition);
    }

}