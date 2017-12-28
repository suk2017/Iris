using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnNormals : MonoBehaviour {
    
	void Start () {
        MeshFilter mf = GetComponent<MeshFilter>();
        Vector3[] n = mf.mesh.normals;
        for(int i = 0; i < n.Length; ++i)
        {
            n[i] = -n[i];
        }
        mf.mesh.normals = n;

	}
	
}
