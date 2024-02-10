using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes an object float into existance at the start
public class FloatIn : MonoBehaviour
{
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        //Sets the target position of the interpolation
        //To the current initial position
        this.targetPos = this.gameObject.transform.position;

        //Calculates new initial position
        this.gameObject.transform.position = new Vector3(
            this.gameObject.transform.position.x,
            -7,
            this.gameObject.transform.position.z
        );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //The result will eventually converge to targetPos
        this.transform.position = Vector3.Lerp(transform.position, targetPos, 0.03f);
    }
}
