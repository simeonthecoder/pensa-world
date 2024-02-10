using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedirectManager : MonoBehaviour
{
    //A list of triggers that have to be regurarly checked up upon
    public RedirectTrigger[] triggers;

    //Contains the tickCount
    public int time;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //What should happen when a trigger is activated
    public void Exit(ObjectExit exitData)
    {
        //Set data about where the scene was exited from
        PlayerPrefs.SetInt(exitData.memoryLocation, exitData.index);

        //Checks if it's a webpage redirect
        if(exitData.webpageRedirect)
        {
            //Converts exitData.location to a URL
            //Currently works only locally
            Application.OpenURL("http://localhost:8080/" + exitData.location);

            //Quits the application to stop the music from playing and stop wasting resources
            //Especially helpful since many tabs get opened when navigating around
            Application.Quit();
        }
        else
        {
            //Otherwise, loads scene based on the exitData.location
            SceneManager.LoadScene(exitData.location);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Performs a check every 5 ticks
        if(time++ % 5 == 0)
        {
            //Iterate trough all triggers
            for(int i = 0; i < triggers.Length; i ++)
            {
                //If triggered, exit and reset its state
                if(triggers[i].triggered)
                {
                    Exit(triggers[i].data);
                    triggers[i].triggered = false;
                }
            }
        }
    }
}
