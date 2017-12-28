using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools.roam
{
    public class HorizontalRotate : MonoBehaviour
    {
        public float smoothTime = 0.1f;
        public float speed = 2;

        private Quaternion target;

        private void Start()
        {
            target = transform.localRotation;
        }
        private void Update()
        {
            target *= Quaternion.Euler(0, RoamManager.horizontalRotateValue() * speed, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, smoothTime);
        }
    }
}
