using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [SerializeField]
    private CinemachineCamera[] virtualCameras; // Updated type to CinemachineCamera  

    private int currentCameraIndex = 0;
    private CinemachineImpulseSource currentImpulseSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // 초기화: 첫 번째 카메라 활성화  
        ActivateCamera(0);
    }

    void Update()
    {
        // 예: 키 입력으로 카메라 전환  
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

        // 모든 카메라 비활성화  
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].gameObject.SetActive(i == index);
        }

        currentCameraIndex = index;
        var activeCamera = virtualCameras[currentCameraIndex];
        currentImpulseSource = activeCamera.GetComponent<CinemachineImpulseSource>();

        if (currentImpulseSource == null)
            currentImpulseSource = activeCamera.gameObject.AddComponent<CinemachineImpulseSource>();
    }
}
