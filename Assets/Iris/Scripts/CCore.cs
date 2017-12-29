using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCore : MonoBehaviour, ICInfo
{

    //公有变量


    //私有变量
    public Vector3 oriPos;
    public Quaternion oriRot;

    //加速变量
    Transform tr;
    Rigidbody rb;

    private void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();

        oriPos = tr.position;
        oriRot = tr.rotation;
    }

    public void _Destroy()
    {

    }

    public void _Reset()
    {
        tr.position = oriPos;
        tr.rotation = oriRot;
        rb.isKinematic = true;
    }

    public void _Spawn(Transform t)
    {

    }

    public void _Start()
    {
        rb.isKinematic = false;
    }
}
