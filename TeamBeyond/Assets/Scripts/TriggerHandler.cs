using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject rod;

    private bool inside = false;

    public void Start()
    {
        rod.SetActive(false);
    }

    public void Update()
    {
        if (!inside) return;

        if (Input.GetKeyDown("e"))
        {
            FreeCamera freeCam = playerCamera.GetComponent<FreeCamera>();

            freeCam.Toggle();
            freeCam.movementDisabled = true;

            freeCam.TeleportPlayer(new Vector3(-10, 0, -10));

            inside = false;

            rod.SetActive(true);
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("GEI NEGUR");
        inside = true;
    }
}
