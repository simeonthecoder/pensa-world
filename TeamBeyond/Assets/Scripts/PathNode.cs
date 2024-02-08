using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    private PathManager pathManager;
    private float time;

    public int index;

    public Door[] linkedDoors;

    public Vector3 offset;

    public bool waving;

    // Start is called before the first frame update
    void Start()
    {
        if(waving)
        {
            Destroy(this.gameObject.GetComponent<MeshRenderer>());
        }

        waving = !waving;
        pathManager = GameObject.Find("Path Manager").GetComponent<PathManager>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(waving)
        {
            this.transform.localScale = new Vector3(
                (Mathf.Sin(time) / 2 + 1.5f),
                (Mathf.Sin(time) / 2 + 1.5f),
                (Mathf.Sin(time) / 2 + 1.5f)
            );
        }
    }

    void OnMouseDown()
    {
        pathManager.camera.GetComponent<DotNavigator>().Move(this.gameObject.transform.position + offset);
        pathManager.camera.GetComponent<DotNavigator>().dots.Push(this.gameObject);

        if(waving)
        {
            Color oldColor = this.gameObject.GetComponent<MeshRenderer>().material.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0);
            this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
        }

        for(int i = 0; i < linkedDoors.Length; i ++)
        {
            linkedDoors[i].Toggle();
        }
    }

    void OnMouseEnter()
    {
        if(waving)
        {
            Color oldColor = this.gameObject.GetComponent<MeshRenderer>().material.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0.3f);
            this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
        }
    }

    void OnMouseExit()
    {
        if(waving)
        {
            Color oldColor = this.gameObject.GetComponent<MeshRenderer>().material.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0f);
            this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
        }
    }
}
