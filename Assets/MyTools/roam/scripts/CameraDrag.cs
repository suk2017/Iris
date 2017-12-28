using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools.roam
{
    public class CameraDrag : MonoBehaviour
    {
        public float smoothTime = 0.1f;
        public float speed = 2;
        public float min = -10;
        public float max = 0;

        private float target;

        private void OnEnable()
        {
            target = transform.localPosition.z;
        }
        private void Update()
        {
            target += RoamManager.CameraDrag() * (transform.localPosition.z - 0.01f) * -speed;
            target = target > max ? max : target;
            target = target < min ? min : target;
            transform.localPosition = new Vector3(0, 0, Mathf.Lerp(transform.localPosition.z, target, smoothTime));
        }
    }
}