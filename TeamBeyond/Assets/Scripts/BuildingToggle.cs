using UnityEngine;

public class BuildingToggle : MonoBehaviour
{
    private bool enabled;
    public GameObject container;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("MI AMORE!!!");

        enabled = !enabled;

        if(enabled)
        {
            container.SetActive(true);
        }
        else
        {
            Destroy(container);
            //container.SetActive(false);
        }
    }
}
