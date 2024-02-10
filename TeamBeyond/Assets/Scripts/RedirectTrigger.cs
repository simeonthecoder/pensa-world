using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectTrigger : MonoBehaviour
{
    //Contains the state of the trigger
    public bool triggered;

    //The exit data
    public ObjectExit data;
    
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Trigger the trigger
    public void Trigger(ObjectExit data)
    {
        this.data = data;

        //Set triggered to true
        //Once registered and handled, it will return back to false
        triggered = true;
    }
}
