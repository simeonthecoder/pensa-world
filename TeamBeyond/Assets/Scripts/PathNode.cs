using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
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

    void OnMouseDown()
    {
        pathManager.camera.GetComponent<DotNavigator>().Move(this.gameObject.transform.position);
    }
}
