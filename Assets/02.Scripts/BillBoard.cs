using UnityEngine;

public enum EBillBoardType
{
    LookAtCamera,
    CameraForward,
}

public class BillBoard : MonoBehaviour
{
    private Transform camTransform;

    [SerializeField]
    private EBillBoardType billBoardType;

    [SerializeField]
    private bool isLockX;
    [SerializeField]
    private bool isLockY;
    [SerializeField]
    private bool isLockZ;

    private Vector3 originalRotation;

    private void Awake()
    {
        originalRotation = transform.rotation.eulerAngles;
    }

    private void LateUpdate()
    {
        //camTransform = Camera.main.transform;

        //transform.LookAt(transform.position + (camTransform.rotation * Vector3.forward));

        switch(billBoardType)
        {
            case EBillBoardType.LookAtCamera:
                if (camTransform == null)
                {
                    camTransform = Camera.main.transform;
                }
                transform.LookAt(camTransform.position, Vector3.up);
                break;

            case EBillBoardType.CameraForward:
                if (camTransform == null)
                {
                    camTransform = Camera.main.transform;
                }
                transform.forward = camTransform.forward;
                break;
        }

        Vector3 rotation = transform.rotation.eulerAngles;
        if(isLockX)
            rotation.x = originalRotation.x;
        if (isLockY)
            rotation.y = originalRotation.y;
        if (isLockZ)
            rotation.z = originalRotation.z;
        transform.rotation = Quaternion.Euler(rotation);
    }
}