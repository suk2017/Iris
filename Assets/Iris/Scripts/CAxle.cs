using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAxle : MonoBehaviour,ICInfo {


    //公开变量
    public float power = 100;
    public Transform decal;
    public bool clockwise = true;//顺时针旋转还是逆时针


    //私有变量
    private Transform target;//增加这个物体时使用的连接
    private List<FixedJoint> FJList;

    private Vector3 oriPos;//初始位置
    private Quaternion oriRot;//初始角度

    //加速变量
    Transform tr;
    Rigidbody rb;

    private void Awake()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
        FJList = new List<FixedJoint>();
    }

    public void _Spawn(Transform t)
    {
        //确定目标
        target = t;

        //初始化姿态
        oriPos = tr.position = target.up * 0.2f + target.position;
        oriRot = tr.rotation = target.rotation;

        //检测周围相邻的可连接的刚体
        FJList.Add(gameObject.AddComponent<FixedJoint>());
        FJList[0].connectedBody = target.parent.parent.GetComponent<Rigidbody>();
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

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            decal.position.Scale(new Vector3(0, 0, 1));
        }
    }
}
