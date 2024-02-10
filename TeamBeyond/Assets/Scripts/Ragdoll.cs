using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    //Holds the position from an eariler tick
    private Vector3 previous;

    // Start is called before the first frame update
    void Start()
    {
        //Init the previous position
        previous = transform.position;
    }

    // Update is called once per frame
    void Update() {
        //Toggles ragdoll mode
        if(Input.GetKeyDown("r"))
        {
            //Disables the animation, so the ragdoll can take over
            GetComponent<Animator>().enabled = !GetComponent<Animator>().enabled;
        }
    }

    void FixedUpdate()
    {
        //Check whether the speed of the player is above a certain threshold
        if((transform.position - previous).magnitude >= 0.25) {
            //In that case, the player enters ragdoll mode
            GetComponent<Animator>().enabled = false;
        }

        //Set the previous position to the current one
        previous = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
