using UnityEngine;

public class ReflectionTogle : MonoBehaviour
{
    public GameObject reflector;

    private bool active;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reflector.SetActive(false);
        this.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        reflector.SetActive(!this.active);
        this.active = !this.active;
    }
}
