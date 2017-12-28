using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools.roam
{
    public class VerticalRotate : MonoBehaviour
    {
        public float smoothTime = 0.1f;
        public float speed = 2;
        public float min = 0;
        public float max = 90;

        private Quaternion target;

        private void Start()
        {
            target = transform.localRotation;
        }
        private void Update()
        {
            float r = RoamManager.verticalRotateValue() * speed;
            r = (r < (max - 90)) ? (max - 90) : r;//r < -(90-max) 多0.1也可以
            target *= Quaternion.Euler(-r, 0, 0);
            Vector3 v = target.eulerAngles;
            v.x = v.x > 180 ? 0 : v.x;
            v.x = v.x > max ? max : v.x;
            v.x = v.x < min ? min : v.x;
            target = Quaternion.Euler(v);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, smoothTime);
        }
    }
}
