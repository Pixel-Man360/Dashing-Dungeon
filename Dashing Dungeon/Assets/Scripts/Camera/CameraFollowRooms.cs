using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraFollowRooms : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;

    public static CameraFollowRooms instance;

    void Awake() 
    {
        if(instance == null)
          instance = this;
    }

    public void SetCameraFollowTarget(Transform target)
    {
        _camera.Follow = target;
    }

}
