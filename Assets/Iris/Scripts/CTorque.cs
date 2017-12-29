using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTorque : MonoBehaviour, ICInfo
{
    //公开变量


    //私有变量
    private bool clockwise = true;//顺时针旋转还是逆时针
    private Transform target;//增加这个物体时使用的连接
    private FixedJoint fj;

    private Vector3 oriPos;//初始位置
    private Quaternion oriRot;//初始角度

    //加速变量
    Transform tr;
    Rigidbody rb;

    private void Awake()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
        fj = new FixedJoint();
    }


    public void _Spawn(Transform t)
    {
        //确定目标
        target = t;

        //初始化姿态
        print(target);
        oriPos = tr.position = target.up * 0 + target.position;
        oriRot = tr.rotation = target.rotation;

        //检测周围相邻的可连接的刚体
        fj = gameObject.AddComponent<FixedJoint>();
        fj.connectedBody = target.parent.parent.GetComponent<Rigidbody>();//目标组件/Connection/当前物体
        //fj.axis = Vector3.up;
        //////判断另一方向
    }

    public void _Destroy()
    {
        //其它需要通知的组件

        //销毁自己
        Destroy(this.gameObject);
    }

    public void _Reset()
    {
        //恢复姿态
        tr.position = oriPos;
        tr.rotation = oriRot;

        //消除物理效果
        //rb.Sleep();
        rb.isKinematic = true;
    }
    public void _Start()
    {
        rb.isKinematic = false;
    }
}
