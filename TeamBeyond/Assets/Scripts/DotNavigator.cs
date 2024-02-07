using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotNavigator : MonoBehaviour
{
    public Vector3 target;
    public Stack<Vector3> before;
    public Stack<GameObject> dots;

    public GameObject root;

    public string sceneLocationTag;

    public GameObject[] nodeIndex;

    public bool specialFlag;

    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        this.target = transform.position;

        this.before = new();
        this.dots = new();

        if(specialFlag)
        {
            PlayerPrefs.SetInt(sceneLocationTag, 0);
        }

        int entranceIndex = PlayerPrefs.GetInt(sceneLocationTag);

        if(entranceIndex < nodeIndex.Length) {
            this.gameObject.transform.position = nodeIndex[entranceIndex].transform.position;
            this.target = nodeIndex[entranceIndex].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(transform.position, target, 7f * Time.deltaTime);

        if(Input.GetKeyDown("b") && this.before.Count > 0)
        {
            this.target = this.before.Pop();
            GameObject targetGameObject = this.dots.Pop();

            Color oldColor = targetGameObject.GetComponent<MeshRenderer>().material.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 1f);
            targetGameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);

            sound.Play();
        }
        else if(Input.GetKeyDown("b") && this.before.Count == 0)
        {
            //this.gameObject.transform.position = root.transform.position;
            this.target = root.transform.position;

            sound.Play();
        }

        if((this.target - this.gameObject.transform.position).magnitude < 0.1f)
        {
            this.gameObject.transform.position = this.target;
        }
    }

    public void Move(Vector3 position) {
        this.before.Push(transform.position);
        this.target = position;

        sound.Play();
    }
}
