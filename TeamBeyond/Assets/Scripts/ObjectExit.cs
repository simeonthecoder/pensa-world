using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExit : MonoBehaviour
{
    //TRUE -> the redirect location is a webpage
    //FALSE -> it's a Unity scene
    public bool webpageRedirect;

    //The "location" from where the exit is called
    public string location;

    //The location in memory that should be checked to determine the entrance index
    public string memoryLocation;
    public int index;

    public GameObject trigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
