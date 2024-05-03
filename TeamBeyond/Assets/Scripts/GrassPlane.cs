using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassPlane : MonoBehaviour
{
    private Vector3 offset;

    public GameObject baseLayer;

    public void Start()
    {
        this.offset = this.transform.position - baseLayer.transform.position;
    }

    private void Update()
    {
        transform.position = baseLayer.transform.position + this.offset;
    }
}
