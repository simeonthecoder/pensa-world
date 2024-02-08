using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Vector3 previous;

    // Start is called before the first frame update
    void Start()
    {
        previous = transform.position;
    }

    void Update() {
        if(Input.GetKeyDown("r"))
        {
            GetComponent<Animator>().enabled = !GetComponent<Animator>().enabled;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if((transform.position - previous).magnitude >= 0.25) {
            GetComponent<Animator>().enabled = false;
        }

        previous = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
