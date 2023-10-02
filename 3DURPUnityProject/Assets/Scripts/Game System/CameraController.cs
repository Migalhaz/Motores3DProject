using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Game.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera m_virtualCamera;
        Cinemachine3rdPersonFollow m_followCamera;

        float m_defaultCameraSide;
        float m_defaultCameraDistance;

        [SerializeField] Transform m_playerTransform;

        [SerializeField] AxisState m_xAxis;
        [SerializeField] AxisState m_yAxis;

        [Header("Rotate Camera Settings")]
        [SerializeField] Transform m_lookAtTransform;
        [SerializeField, Min(0)] float m_playerRotateSpeed = 5f;
        Vector3 m_lookAtAngle;

        [Header("Aim Camera Settings")]
        [SerializeField, Min(0)] float m_cameraSideZoom = .9f;
        [SerializeField, Min(0)] float m_cameraLerpValue = 4f;

        private void Awake()
        {
            m_followCamera = m_virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            m_defaultCameraSide = m_followCamera.CameraSide;
            m_defaultCameraDistance = m_followCamera.CameraDistance;
        }

        private void Update()
        {
            RotateCam();
        }

        void RotateCam()
        {
            float deltaTime = Time.deltaTime;
            m_xAxis.Update(deltaTime);
            m_yAxis.Update(deltaTime);

            m_lookAtAngle.Set(m_yAxis.Value, m_xAxis.Value, 0f);
            m_lookAtTransform.eulerAngles = m_lookAtAngle;
            m_playerTransform.rotation = Quaternion.Slerp(m_playerTransform.rotation, Quaternion.Euler(0f, m_xAxis.Value, 0f), m_playerRotateSpeed * deltaTime);
        }

        public void AimZoomIn()
        {
            m_followCamera.CameraSide = m_cameraSideZoom;
            m_followCamera.CameraDistance = Mathf.Lerp(m_followCamera.CameraDistance, m_cameraSideZoom, Time.deltaTime * m_cameraLerpValue);
        }

        public void AimZoomOut()
        {
            m_followCamera.CameraSide = m_defaultCameraSide;
            m_followCamera.CameraDistance = Mathf.Lerp(m_followCamera.CameraDistance, m_defaultCameraDistance, Time.deltaTime * m_cameraLerpValue);
        }
    }
}
