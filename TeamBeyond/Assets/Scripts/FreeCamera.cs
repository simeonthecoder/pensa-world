using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of camera movement
    public float lookSpeed = 2f; // Speed of camera rotation

    public bool enabled;
    public bool movementDisabled;

    public vThirdPersonCamera camera;
    public Invector.vCharacterController.vThirdPersonInput playerController;
    public GameObject player;

    void Start()
    {
        if (PlayerPrefs.GetInt("FreeCam") == 1)
        {
            enabled = true;

            camera.enabled = !enabled;
            playerController.enabled = !enabled;

            transform.position = new Vector3(
                PlayerPrefs.GetFloat("FreeCamX"),
                PlayerPrefs.GetFloat("FreeCamY"),
                PlayerPrefs.GetFloat("FreeCamZ")
            );

            transform.eulerAngles = new Vector3(
                PlayerPrefs.GetFloat("FreeCamRotX"),
                PlayerPrefs.GetFloat("FreeCamRotY"),
                PlayerPrefs.GetFloat("FreeCamRotZ")
            );
        }
    }

    public void Toggle()
    {
        enabled = !enabled;

        camera.enabled = !enabled;
        playerController.enabled = !enabled;
    }
    
    public void TeleportPlayer(Vector3 offset)
    {
        player.transform.position = transform.position + offset;
    }

    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            Toggle();
        }

        if (Input.GetKeyDown("t"))
        {
            TeleportPlayer(Vector3.zero);
            Destroy(GetComponent<DotNavigator>());
        }

        if (!enabled) return;

        //player.transform.position = transform.position;

        // Movement controls
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift)) moveDirection *= 3;

        if (!movementDisabled) transform.Translate(moveDirection);

        // Rotation controls
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 rotation = new Vector3(-mouseY, mouseX, 0f) * lookSpeed;
        transform.eulerAngles += rotation;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("FreeCam", enabled ? 1 : 0);

        PlayerPrefs.SetFloat("FreeCamX", transform.position.x);
        PlayerPrefs.SetFloat("FreeCamY", transform.position.y);
        PlayerPrefs.SetFloat("FreeCamZ", transform.position.z);

        PlayerPrefs.SetFloat("FreeCamRotX", transform.eulerAngles.x);
        PlayerPrefs.SetFloat("FreeCamRotY", transform.eulerAngles.y);
        PlayerPrefs.SetFloat("FreeCamRotZ", transform.eulerAngles.z);
    }
}