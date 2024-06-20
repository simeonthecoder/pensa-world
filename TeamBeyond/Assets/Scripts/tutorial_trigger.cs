using UnityEngine;
using UnityEngine.UI;
public class tutorial_trigger : MonoBehaviour
{
    public GameObject text;

    private bool inside = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
   
    void Update()
    {
        if (inside)
        {
            Debug.Log("its inside");
            text.SetActive(true);
            
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("GEI NEGUR");
        inside = true;
    }
}
