using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    private PathManager pathManager;
    private float time;

    public Door[] linkedDoors;

    // Start is called before the first frame update
    void Start()
    {
        pathManager = GameObject.Find("Path Manager").GetComponent<PathManager>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        this.transform.localScale = new Vector3(Mathf.Sin(time) / 2 + 1, Mathf.Sin(time) / 2 + 1, Mathf.Sin(time) / 2 + 1);
    }

    void OnMouseDown()
    {
        pathManager.camera.GetComponent<DotNavigator>().Move(this.gameObject.transform.position);
        pathManager.camera.GetComponent<DotNavigator>().dots.Push(this.gameObject);

        Color oldColor = this.gameObject.GetComponent<MeshRenderer>().material.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0);
        this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);

        for(int i = 0; i < linkedDoors.Length; i ++)
        {
            linkedDoors[i].Toggle();
        }
    }
}
