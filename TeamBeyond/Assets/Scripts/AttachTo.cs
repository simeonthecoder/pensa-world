using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachTo : MonoBehaviour
{
    public GameObject target;

    private Vector3 initialOffset;

    // Start is called before the first frame update
    void Start()
    {
        this.initialOffset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.transform.position + initialOffset;
    }
}
