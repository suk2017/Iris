using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInput : MonoBehaviour {

    #region UI操作
    /// <summary>是 所有确认操作</summary>
    public static bool _YesOKButton()
    {
        return Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter);
    }
    /// <summary>否</summary>
    public static bool _NoButton()
    {
        return Input.GetKeyDown(KeyCode.RightControl);
    }
    /// <summary>取消 所有取消操作</summary>
    public static bool _CancelButton()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
    
    #endregion

    #region 船舶航行
    public static Vector3 GetDirection()
    {
        RaycastHit[] infos = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 200, LayerMask.GetMask("sea")/*Layer == sea*/);
        return infos[0].point;
    }
    public static bool Moving()
    {
        if (freeMoveMode) { return false; }
        return Input.GetMouseButton(0) && (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), 200, LayerMask.GetMask("sea")));
    }
    public static bool Move_Stop()
    {
        return Input.GetMouseButtonUp(0);
    }
    #endregion

    #region 镜头控制（基础）
    public static float HorizontalRotateValue()
    {
        return Input.GetAxis("Mouse X");
    }
    public static float VerticalRotateValue()
    {
        return Input.GetAxis("Mouse Y");
    }
    public static float CameraDrag()
    {
        return Input.GetAxis("Mouse ScrollWheel");
    }
    #endregion

    #region 镜头控制（高级）
    
    private static bool freeView = false;
    public static bool GetViewMode()
    {
        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.Z))
        {
            return freeView = !freeView;
        }
        else
        {
            return freeView;
        }
    }//主旋转(B旋转)启闭

    private static bool freeMoveMode = false;
    public static bool? GetFreeMoveMode()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetMouseButtonDown(1))
        {
            return freeMoveMode = !freeMoveMode;
        }
        else
        {
            return null;
        }
    }//自由移动启闭

    public static Vector3 GetMove()
    {
        if (Input.GetMouseButton(0))
        {
            return new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
        }
        else
        {
            return Vector3.zero;
        }
    }//自由移动输入

    private static bool TraceMode = false;
    public static bool? GetTraceMode()//跟踪旋转（A旋转）
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            return TraceMode = !TraceMode;
        }
        else
        {
            return null;
        }

    }
    #endregion


    #region 地形生成
    /// <summary>地形生成中 获取中心</summary>
    public static Vector3 GetCenter()
    {
        return Input.mousePosition;
    }
    
    /// <summary>开始生成</summary>
    public static bool Creating()
    {
        return Input.GetMouseButton(0);
    }
    public static bool Raising()
    {

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool ShortCut_undo()//ctrl + z
    {
        return (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            && (Input.GetKeyDown(KeyCode.Z));
    }
    public static bool ShortCut_redo()//
    {
        return (Input.GetKeyDown(KeyCode.LeftCommand) || Input.GetKeyDown(KeyCode.RightControl))
            && (Input.GetKeyDown(KeyCode.Y));
    }
    #endregion

}
