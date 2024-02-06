using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject[] pieces;
    public ObjectExit exitData;

    public float openTime;
    public float rotateAmount;

    private float rotationStep;

    public bool open;

    private float time;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        this.time = 0f;
        this.active = false;

        CalculateRotationStep();
    }

    public void CalculateRotationStep()
    {
        this.rotationStep = rotateAmount / (50 * openTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (active)
        {
            if (time <= openTime)
            {
                for(int i = 0; i < pieces.Length; i ++)
                {
                    pieces[i].transform.rotation *= Quaternion.Euler(0, 0, this.rotationStep * (this.open ? 1 : -1));
                }
            }
            else
            {
                this.active = false;
                this.time = 0f;

                this.gameObject.GetComponent<RedirectTrigger>().Trigger(exitData);
            }
        }
    }

    public void Toggle()
    {
        this.open = !this.open;
        this.active = true;

        this.time = 0;
    }
}
