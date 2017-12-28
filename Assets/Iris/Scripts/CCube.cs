using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCube : MonoBehaviour {

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

    //加速变量
    Transform tr;
    Camera mainCam;

    private void Start()
    {
        tr = transform;
        mainCam = Camera.main;
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

    public void getPos()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] results = Physics.RaycastAll(ray);

    }
}
