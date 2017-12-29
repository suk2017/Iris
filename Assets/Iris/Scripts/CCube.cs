using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCube : MonoBehaviour,ICInfo {

    //公有变量
    public float length = 1;
    public float width = 1;
    public float height = 1;

    public enum CStatus
    {
        START,//刚被创建
        L,//正在创建长
        W,//正在创建宽
        H,//正在创建高
        END//创建结束
    }

    //私有变量
    private CStatus status = CStatus.START;
    private Transform target;
    private Vector3 oriPos;
    private Quaternion oriRot;
    private List<FixedJoint> FJList;

    //加速变量
    Transform tr;
    Rigidbody rb;

    private void Awake()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
        FJList = new List<FixedJoint>();

        oriPos = tr.position;
        oriRot = tr.rotation;
    }

    private void Update()
    {
        //switch (status)
        //{
        //    case CStatus.START:
        //        {

        //            if (Input.GetMouseButtonDown(0))
        //            {

        //            }
        //        }
        //        break;
        //    case CStatus.L: break;
        //    case CStatus.W: break;
        //    case CStatus.H: break;
        //    case CStatus.END: break;
        //}
    }

    //public void getPos()
    //{
    //    Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit[] results = Physics.RaycastAll(ray);

    //}

    public void _Spawn(Transform t)
    {
        //确定目标
        target = t;

        //初始化姿态
        oriPos = tr.position = target.up*0.5f + target.position;
        oriRot = tr.rotation = target.rotation;

        //检测周围相邻的可连接的刚体
        FJList.Add(gameObject.AddComponent<FixedJoint>());
        FJList[0].connectedBody = target.parent.parent.GetComponent<Rigidbody>();
        //FJList[0].connectedAnchor = target.localPosition;
        //////判断其它方向
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
