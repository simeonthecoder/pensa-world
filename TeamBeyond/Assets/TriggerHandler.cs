using UnityEngine;

public class TriggerHandler : MonoBehaviour
{


    public GameObject playerCamera;
    

    private bool inside = false;

    public void Start()
    {

    }
    public void Update()
    {
        if (!inside) return;

        if (Input.GetKeyDown("e"))
        {
            FreeCamera freeCam = playerCamera.GetComponent<FreeCamera>();

            freeCam.enabled = true;
            freeCam.movementDisabled = true;

            
            
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("GEI NEGUR");
        inside = true;
    }
}
