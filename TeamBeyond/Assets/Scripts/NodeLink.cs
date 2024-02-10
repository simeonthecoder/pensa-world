using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackNode : MonoBehaviour
{
    private PathManager pathManager;

    // Start is called before the first frame update
    void Start()
    {
        pathManager = GameObject.Find("Path Manager").GetComponent<PathManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Call the Back() function of the main camera
    //to return to the previous node
    void OnMouseDown()
    {
        pathManager.camera.GetComponent<DotNavigator>().Back();
    }
}
