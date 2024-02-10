using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    private PathManager pathManager;
    private float time;

    public int index;

    //Contains all the doors that should be toggled when activated
    public Door[] linkedDoors;

    //The offset from the node position to the supposed pos
    public Vector3 offset;

    //Determines whether the node should be visible;
    //In that case, it has animations
    //FALSE - default behavior, TRUE - invisible
    public bool waving;

    // Start is called before the first frame update
    void Start()
    {
        //If it's "waving", remove the mesh renderer component from the node
        if(waving)
        {
            Destroy(this.gameObject.GetComponent<MeshRenderer>());
        }

        //Flip the valie in waving, so that T->F and F->T
        waving = !waving;

        //Finds the path manager
        pathManager = GameObject.Find("Path Manager").GetComponent<PathManager>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        //Checks whether the node sphere should be animated
        if(waving)
        {
            //Uses sine waves to add a scaling anim
            this.transform.localScale = new Vector3(
                (Mathf.Sin(time) / 2 + 1.5f),
                (Mathf.Sin(time) / 2 + 1.5f),
                (Mathf.Sin(time) / 2 + 1.5f)
            );
        }
    }

    //When the node is clicked
    //Works even if it's invisible
    void OnMouseDown()
    {
        //Set the new target of the camera, add the node as a visited one
        pathManager.camera.GetComponent<DotNavigator>().Move(this.gameObject.transform.position + offset);
        pathManager.camera.GetComponent<DotNavigator>().dots.Push(this.gameObject);

        //Color animation, depending on the value in the waving variable
        if(waving)
        {
            Color oldColor = this.gameObject.GetComponent<MeshRenderer>().material.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0);
            this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
        }

        //Activate all doors linked to the node
        for(int i = 0; i < linkedDoors.Length; i ++)
        {
            linkedDoors[i].Toggle();
        }
    }

    //On mouse hover
    //Animates the color of "non-waving" nodes
    void OnMouseEnter()
    {
        if(waving)
        {
            Color oldColor = this.gameObject.GetComponent<MeshRenderer>().material.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0.3f);
            this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
        }
    }

    //On mouse exiting from the "hitbox"
    //Reverts the color back, depending of it's waving behaviour
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
