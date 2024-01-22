using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotNavigator : MonoBehaviour
{
    private Vector3 target;
    private Stack<Vector3> before;

    // Start is called before the first frame update
    void Start()
    {
        this.target = transform.position;
        this.before = new();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(transform.position, target, 0.03f);

        if(Input.GetKeyDown("b") && this.before.Count > 0)
        {
            this.target = this.before.Pop();
        }
    }

    public void Move(Vector3 position) {
        this.before.Push(transform.position);
        this.target = position;
    }
}
