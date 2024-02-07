using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float time;
    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = Random.Range(0, 100);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.position += new Vector3(0, Mathf.Sin(time + offset) / 300f, 0); 
    }
}
