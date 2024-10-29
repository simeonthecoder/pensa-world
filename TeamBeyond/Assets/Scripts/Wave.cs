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
        //Determines the phase offset of the sine wave
        //Breaks up the animation of the object
        offset = Random.Range(0, 100);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        //Calculate position based on sine waves
        //Take the offset into account to break up the animation
        transform.position += new Vector3(0, Mathf.Sin(time + offset) / 300f, 0) * 30; 
    }
}
