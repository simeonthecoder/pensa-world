using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedirectManager : MonoBehaviour
{
    public RedirectTrigger[] triggers;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Exit(ObjectExit exitData)
    {
        PlayerPrefs.SetInt(exitData.memoryLocation, exitData.index);

        if(exitData.webpageRedirect)
        {
            Application.OpenURL("http://localhost:8080/" + exitData.location);
        }
        else
        {
            SceneManager.LoadScene(exitData.location);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < triggers.Length; i ++)
        {
            if(triggers[i].triggered)
            {
                Exit(triggers[i].data);
                triggers[i].triggered = false;
            }
        }
    }
}
