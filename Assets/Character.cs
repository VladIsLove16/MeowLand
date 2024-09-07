using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }
    public void Move(Vector3 speed)
    {
        rb.AddForce(speed);
    }
}
