using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //The pieces that make the door
    public GameObject[] pieces;

    //Information on where the door leads
    public ObjectExit exitData;

    //Door open sound effect
    public AudioSource audio;

    //The speed of opening
    public float openTime;

    //The amount the door should open
    public float rotateAmount;

    private float rotationStep;

    public bool open;

    private float time;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        this.time = 0f;

        //Calculate rotation step
        CalculateRotationStep();
    }

    //Calculates the rotation step
    public void CalculateRotationStep()
    {
        //Calculate rotation step based on the amount and speed
        this.rotationStep = rotateAmount / (50 * openTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        //Check if the door animation is active
        if (active)
        {
            //Check if there's still time remaining
            if (time <= openTime)
            {
                //Rotate every piece of the door on the Z-axis based on the step
                for(int i = 0; i < pieces.Length; i ++)
                {
                    pieces[i].transform.rotation *= Quaternion.Euler(0, 0, this.rotationStep * (this.open ? 1 : -1));
                }
            }
            else
            {
                //Otherwise, stop the animation and reset the time
                this.active = false;
                this.time = 0f;

                //Perform the exit
                this.gameObject.GetComponent<RedirectTrigger>().Trigger(exitData);
            }
        }
    }

    //Toggles the door state
    //C -> O, O -> C
    public void Toggle()
    {
        //Toggle the state
        this.open = !this.open;

        //Activate the animation
        this.active = true;

        //Play the sound effect
        audio.Play();

        //Reset timer
        this.time = 0;
    }
}
