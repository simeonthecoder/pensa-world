using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotNavigator : MonoBehaviour
{
    public Vector3 target;

    //Holds the previously visited positions
    public Stack<Vector3> before;

    //Holds the previous nodes
    public Stack<GameObject> dots;

    //The root node
    public GameObject root;

    //The name of the current scene as its name in PlayerPrefs
    public string sceneLocationTag;

    //Contains the various entry nodes when transitioning to the scene
    public GameObject[] nodeIndex;

    //Whether the entry node is reset back to the camera position
    public bool specialFlag;

    //Holds the "whoosh" sound effect
    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        //Set the first target as the current position
        this.target = transform.position;

        //Init before and dots
        this.before = new();
        this.dots = new();

        if(specialFlag)
        {
            //Reset entry position back to the camera pos
            PlayerPrefs.SetInt(sceneLocationTag, 0);
        }

        //Get the index of the entrance node
        int entranceIndex = PlayerPrefs.GetInt(sceneLocationTag);

        //Check if the index exists
        if(entranceIndex < nodeIndex.Length) {
            //If so, move the camera there and set it as the initial target
            this.gameObject.transform.position = nodeIndex[entranceIndex].transform.position;
            this.target = nodeIndex[entranceIndex].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Performs the interpolation
        //Will eventually converge to target, snapped anyway
        this.transform.position = Vector3.Lerp(transform.position, target, 7f * Time.deltaTime);

        if(Input.GetKeyDown("b"))
        {
            Back();
        }

        //If the camera pos is within a certain distance, snap it to the target node
        if((this.target - this.gameObject.transform.position).magnitude < 0.1f)
        {
            this.gameObject.transform.position = this.target;
        }
    }

    public void Back()
    {
        //Check if nodes have been visited before
        if(this.before.Count > 0)
        {
            //Get the previous position and previous node
            //and set the new target to the previous position
            this.target = this.before.Pop();
            GameObject targetGameObject = this.dots.Pop();

            //Change the color of the node set as the new target
            Color oldColor = targetGameObject.GetComponent<MeshRenderer>().material.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 1f);
            targetGameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
        }
        else
        {
            //Interpolates smoothly to the root node position
            this.target = root.transform.position;
        }

        //Play sound effect
        sound.Play();
    }

    public void Move(Vector3 position) {
        //Push the position into the visited positions stack
        this.before.Push(transform.position);

        //Set the new position as the target
        this.target = position;

        //Play sound effect
        sound.Play();
    }
}
