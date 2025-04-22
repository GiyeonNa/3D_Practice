using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] 
    private CinemachineCamera[] virtualCameras;

    private int currentCameraIndex = 0;

    void Start()
    {
        // �ʱ�ȭ: ù ��° ī�޶� Ȱ��ȭ
        ActivateCamera(0);
    }

    void Update()
    {
        // ��: Ű �Է����� ī�޶� ��ȯ
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ActivateCamera(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ActivateCamera(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ActivateCamera(2);
        }
    }

    private void ActivateCamera(int index)
    {
        if (index < 0 || index >= virtualCameras.Length) 
            return;

        // ��� ī�޶� ��Ȱ��ȭ
        //for (int i = 0; i < virtualCameras.Length; i++)
        //{
        //    virtualCameras[i].Priority = (i == index) ? 10 : 0;
        //}
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].gameObject.SetActive(i == index);
        }

        currentCameraIndex = index;
    }
}
