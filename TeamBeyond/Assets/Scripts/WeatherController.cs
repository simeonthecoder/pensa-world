using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public Material cloudMaterial;

    private float time;

    public bool userControl;

    public float curr;

    // Start is called before the first frame update
    void Start()
    {
        curr = PlayerPrefs.GetFloat("curr");

        if (curr == 0) curr = 0.75f;

        userControl = true;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(!userControl) curr = Mathf.PerlinNoise(0, time / 20f);
        
        if(Input.GetKeyDown("z"))
        {
            userControl = !userControl;
        }

        if(userControl && Input.GetKey("."))
        {
            curr += Time.deltaTime;
        }
        else if(userControl && Input.GetKey(","))
        {
            curr -= Time.deltaTime;
        }

        curr = Mathf.Max(0f, Mathf.Min(1f, curr));

        PlayerPrefs.SetFloat("curr", curr);

        cloudMaterial.SetFloat("_Cloudiness", Mathf.Lerp(0.6f, -0.1f, curr));
        cloudMaterial.SetFloat("_Alpha", Mathf.Lerp(2, 0.1f, curr));
    }
}
