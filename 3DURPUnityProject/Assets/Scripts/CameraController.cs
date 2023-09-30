using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Game.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform m_playerTransform;

        [SerializeField] AxisState m_xAxis;
        [SerializeField] AxisState m_yAxis;

        [Header("Rotate Camera Settings")]
        [SerializeField] Transform m_lookAtTransform;
        [SerializeField, Min(0)] float m_playerRotateSpeed = 5f;
        Vector3 m_lookAtAngle;

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
    }
}
