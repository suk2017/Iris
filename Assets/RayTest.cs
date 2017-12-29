using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour {

    public GameObject mark;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] info = Physics.RaycastAll(ray);

            print(info.Length);
            for (int i = 0; i < info.Length; ++i)
            {
                Instantiate(mark, info[i].point, Quaternion.identity).name = i+"";
            }
        }

    }
}
