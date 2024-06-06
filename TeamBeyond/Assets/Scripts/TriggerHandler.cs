using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject rod;
    public GameObject targetPosition;
    private bool inside = false;
    private bool active = false;

    public void Start()
    {
        Debug.Log("starting niga");
        rod.SetActive(false);
    }

    public void Update()
    {
        if (!inside && !active) return;

        if (Input.GetKeyDown("g"))
        {
            active = true;
            FreeCamera freeCam = playerCamera.GetComponent<FreeCamera>();

            freeCam.Toggle();
            freeCam.movementDisabled = true;

            freeCam.TeleportPlayer(new Vector3(-10, 0, -10));
            freeCam.transform.position = targetPosition.transform.position;
            inside = false;
            rod.SetActive(true);


            rod.transform.position = targetPosition.transform.position;
            rod.transform.position = new Vector3(
                targetPosition.transform.position.x + 1,
                targetPosition.transform.position.y - 1,
                targetPosition.transform.position.z + 1
            );

            
        }
        if (active)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("hvurlqi se ot mosta");
                rod.GetComponent<Animator>().SetBool("rod_throw", true);
                
            }
        }


    }

    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("GEI NEGUR");
        inside = true;
    }
}
