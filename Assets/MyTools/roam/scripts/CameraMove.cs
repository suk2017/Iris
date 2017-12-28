using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools.roam
{
    /// <summary>
    /// 此类没有从RoamManager中获得输入
    /// </summary>
    [RequireComponent(typeof(RoamManager))]
    public class CameraMove : MonoBehaviour
    {
        [HideInInspector]public Transform target;//追踪目标
        public float smoothTimeA = 0.8f;
        public float smoothTimeB = 0.8f;
        public bool lockView = false;//锁定
        [Tooltip("从自由移动恢复后不复原到移动前位置")]public bool keepPos = false;

        private void OnEnable()
        {
            if(keepPos && target) { target.transform.position = transform.position; }
        }
        private void Start()
        {
            target = GetComponent<RoamManager>().getTarget();
        }

        private void FixedUpdate()//不能用 Update 或 LateUpdate
        {
            transform.position = Vector3.Lerp(transform.position, target.position, smoothTimeA);
            if (lockView) { transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, smoothTimeB); }
        }

        public void rotateSelf()// 只改变自己 且假定只有一个子物体
        {
            if (!target) { return; }
            Transform t = transform.GetChild(0);
            transform.DetachChildren();
            transform.rotation = target.rotation;
            t.parent = transform;
        }
    }
}
