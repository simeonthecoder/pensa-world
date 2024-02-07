using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectTrigger : MonoBehaviour
{
    public bool triggered;
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

    public void Trigger(ObjectExit data)
    {
        this.data = data;

        triggered = true;
    }
}
