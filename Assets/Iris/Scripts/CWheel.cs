using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWheel : MonoBehaviour
{
    public float power = 100;
    public Transform decal;

    private bool clockwise = true;
    private Transform tr;
    private Rigidbody rb;


    private void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddTorque(tr.right * power * (clockwise ? 1 : -1));
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddTorque(-tr.right * power * (clockwise ? 1 : -1));
        }
    }

    public void _ToggleTurn()
    {
        decal.localScale = Vector3.Scale(decal.localScale, new Vector3(1, 1, -1));
        clockwise = !clockwise;
    }
}
