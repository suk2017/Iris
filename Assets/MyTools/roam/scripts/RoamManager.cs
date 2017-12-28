using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.roam;

public class RoamManager : MonoBehaviour
{

    public CameraMove cameraMove;
    public CameraMoveFree cameraMoveFree;
    public HorizontalRotate horizontalRotate;
    public VerticalRotate verticalRotate;
    public CameraDrag cameraDrag;
    [SerializeField]private Transform target;
    
    private bool? viewMode2;
    private static RoamMode mode;

    private void Start()
    {
        cameraMoveFree.hr = horizontalRotate.transform;
    }
    private void Update()
    {
        //是否可以自由移动
        bool? freeMode = GlobalInput.GetFreeMoveMode();
        if (freeMode == true)
        {
            changeRoamMode(RoamMode.free);//自由移动
        }
        else if (freeMode == false)
        {
            changeRoamMode(RoamMode.trace);//跟踪移动
        }
        switch (mode)//自由移动 or 追踪移动
        {
            case RoamMode.free: cameraMove.enabled = false; cameraMoveFree.enabled = true; break;
            case RoamMode.trace: cameraMove.enabled = true; cameraMoveFree.enabled = false; break;
        }

        //是否可以自由旋转
        cameraDrag.enabled = verticalRotate.enabled = horizontalRotate.enabled = GlobalInput.GetViewMode();//用于B旋转(主旋转)锁定

        //是否追踪目标方向
        bool? traceMode = GlobalInput.GetTraceMode();//获取是否为A旋转(跟踪旋转)
        if(traceMode == true)
        {
            cameraMove.lockView = true;
        }
        else if(traceMode == false)
        {
            cameraMove.lockView = false;
        }
    }

    #region 状态
    private static void changeRoamMode(RoamMode m)
    {
        mode = m;
    }
    #endregion

    #region  接口
    public static float CameraDrag()
    {
        return GlobalInput.CameraDrag();
    }
    public static float verticalRotateValue()
    {
        return GlobalInput.VerticalRotateValue();
    }
    public static float horizontalRotateValue()
    {
        return GlobalInput.HorizontalRotateValue();
    }
    public static Vector3 CameraMove()
    {
        return GlobalInput.GetMove();
    }

    /// <summary>
    /// 因为target定为私有变量 因此只能调用此方法来获得
    /// </summary>
    /// <returns>目标</returns>
    public Transform getTarget()
    {
        return target;
    }

    /// <summary>
    /// 若要重新设置目标 则需要调用此方法 不能直接复制
    /// </summary>
    /// <param name="t">欲设定的目标</param>
    public void setTarget(Transform t)
    {
        target = t;
        cameraMove.target = t;
    }
    
    #endregion
}

public enum RoamMode
{
    trace,//跟踪目标
    free //自由移动
}

