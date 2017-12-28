 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools.roam
{
    /// <summary>
    /// 此类既实现了 目标追踪又可以自由移动
    /// </summary>
    public class CameraMoveFree : MonoBehaviour
    {
        public float smoothTime = 0.05f;
        public float speed = 0.2f;

        [HideInInspector]public Transform hr;
        private Vector3 target = Vector3.zero;//虚拟目标 不显示在屏幕上

        private void OnEnable()
        {
            target = transform.position;
        }

        private void Update()
        {
            target += hr.localRotation * RoamManager.CameraMove() * speed;
        }

        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, target, smoothTime);
        }

    }
}