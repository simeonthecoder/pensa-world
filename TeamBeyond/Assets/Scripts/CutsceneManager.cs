using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public bool trigger;

    private int tick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tick++;

        if (tick % 10 != 0) return;

        if(trigger)
        {
            trigger = false;
            Destroy(GetComponent<Animator>());
        }
    }
}
