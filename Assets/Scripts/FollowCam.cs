using Unity.Cinemachine;
using UnityEngine;

public class FollowCam : Singleton<FollowCam>
{
    private CinemachineCamera cinemachineCamera;

    private void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void SetFollowTarget(Transform followTarget)
    {
        cinemachineCamera.Follow = followTarget;
    }
}
