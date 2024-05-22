using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGrass : MonoBehaviour
{
    public bool enabled;
    public GrassSpawner GrassSpawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        enabled = !enabled;
        GrassSpawner.Toggle(enabled);
    }
}
