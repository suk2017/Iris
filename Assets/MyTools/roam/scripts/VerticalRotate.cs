using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools.roam
{
    public class VerticalRotate : MonoBehaviour
    {
        [Tooltip("平滑度")] public float smoothTime = 0.1f;
        [Tooltip("灵敏度")] public float speed = 2;
        [Tooltip("是否限制在min和max之间")] public bool clamp = false;
        [Tooltip("下限[-90,90)")] public float min = 0;
        [Tooltip("上限(-90,90]")] public float max = 90;

        private Quaternion target;

        private void Start()
        {
            target = transform.localRotation;
        }
        private void Update()
        {
            float r = -RoamManager.verticalRotateValue() * speed;
            if (min > max)
            {
                float temp = max;
                max = min;
                min = temp;
            }
            if (clamp)
            {
                /* r < (90-max) 多0.1也可以
                 * 这是为了防止冲破阻碍（冲过去就回不来了）
                 * 在任何角度都不能达到这个速率临界值 
                 * 否则请使用 当前x轴转换后的角度α
                 * 即 r < (90-α)
                 */
                r = ((max + r) > 90) ? (90 - max) : r;//速率不能超过速率上限
                r = ((min + r) < -90) ? (-90 - min) : r;//速率不能超过速率下限
                print(r);
                target *= Quaternion.Euler(r, 0, 0);//执行旋转
                Vector3 v = target.eulerAngles;
                //v.x = v.x > 180 ? 0 : v.x;
                if(v.x < 180)
                {
                    v.x = v.x > max ? max : v.x;//
                }
                else
                {
                    v.x = (v.x - 360) < min ? (min + 360) : v.x;
                }
                target = Quaternion.Euler(v);
            }
            else
            {
                target *= Quaternion.Euler(r, 0, 0);
                print(target);
            }
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, smoothTime);
        }
    }
}
