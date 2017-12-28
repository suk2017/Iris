using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //公开变量
    public bool playing = false;
    public GameObject CoreCube;//核心
    public GameObject choosedObject;//被选中的物体

    //私有变量
    private List<Rigidbody> rigidbodys;
    private List<Vector3> position;
    private List<Quaternion> rotation;

    //加速变量
    Camera mainCam;

    private void Start()
    {
        //私有变量初始化
        rigidbodys = new List<Rigidbody>();
        position = new List<Vector3>();
        rotation = new List<Quaternion>();

        //加速变量初始化
        mainCam = Camera.main;

        //游戏初始化
        rigidbodys.Add(CoreCube.GetComponent<Rigidbody>());
        position.Add(CoreCube.transform.position);
        rotation.Add(CoreCube.transform.rotation);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction);
    }
    Ray ray;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _ToggleStatus();
        }
        ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] info = Physics.RaycastAll(ray);
        for (int i = 0; i < info.Length; ++i)
        {
            bool breakNow = false;
            switch (info[i].collider.tag)
            {
                case "Body":
                    {
                        if (Input.GetKey(KeyCode.X) || Input.GetKeyDown(KeyCode.X))
                        {
                            _Destroy(info[i].rigidbody);
                        }
                        breakNow = true;
                    }
                    break;
                case "Connection":
                    {
                        if (Input.GetMouseButtonDown(0) && !playing)
                        {
                            _Spawn(info[i], info[i].collider.transform.up, info[i].transform.position);
                        }
                        breakNow = true;
                    }
                    break;
                case "Wheel":
                    {


                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            info[i].rigidbody.GetComponent<CWheel>()._ToggleTurn();
                        }
                        else if (Input.GetKey(KeyCode.X) || Input.GetKeyDown(KeyCode.X))
                        {
                            _Destroy(info[i].rigidbody);
                        }
                        breakNow = true;
                    }
                    break;
            }
            if (breakNow)
            {
                break;
            }
        }
    }
    

    public void _ToggleStatus()
    {
        if (playing)
        {
            for (int i = 0; i < rigidbodys.Count; ++i)
            {
                rigidbodys[i].isKinematic = true;
                rigidbodys[i].transform.position = position[i];
                rigidbodys[i].transform.rotation = rotation[i];
            }
            playing = false;
        }
        else
        {
            for (int i = 0; i < rigidbodys.Count; ++i)
            {
                rigidbodys[i].isKinematic = false;
            }
            playing = true;
        }
    }

    public void _ChooseObject(GameObject g)
    {
        choosedObject = g;
    }

    public void _Destroy(Rigidbody r)
    {
        int i = rigidbodys.FindIndex(new System.Predicate<Rigidbody>(delegate (Rigidbody r0) { return r0 == r; }));
        rigidbodys.RemoveAt(i);
        position.RemoveAt(i);
        rotation.RemoveAt(i);

        Destroy(r.gameObject);
    }

    public void _Spawn(RaycastHit rh, Vector3 direction, Vector3 pos)
    {
        GameObject go = Instantiate(choosedObject, direction + pos, Quaternion.identity);
        Rigidbody r = go.GetComponent<Rigidbody>();
        Transform tf = go.transform;


        r.isKinematic = true;
        rigidbodys.Add(r);

        position.Add(tf.position);
        rotation.Add(tf.rotation);

        FixedJoint fj = rh.rigidbody.gameObject.AddComponent<FixedJoint>();
        fj.connectedBody = r;
        fj.connectedAnchor = direction / 2 + pos;
        
    }


}
