using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCycle : MonoBehaviour
{
    private DayAndNight sun;

    public float cycleStart;
    public float cycleEnd;

    // Start is called before the first frame update
    void Start()
    {
        this.sun = GameObject.Find("Sun").GetComponent<DayAndNight>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Light>().enabled = (sun.currentTime >= cycleStart && sun.currentTime <= cycleEnd);
    }
}
