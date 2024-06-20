using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreeCamera : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of camera movement
    public float lookSpeed = 2f; // Speed of camera rotation

    public bool enabled;
    public bool movementDisabled;

    public vThirdPersonCamera camera;
    public Invector.vCharacterController.vThirdPersonInput playerController;
    public GameObject player;
    public GameObject[] body;

    public GameObject[] entryPoints;

    private int playerSnap;

    public float distance;

    void Start()
    {
        distance = PlayerPrefs.GetFloat("CamDistance");

        if (distance == 0) distance = 1.7f;

        playerSnap = 20;

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
        else
        {
            enabled = false;

            camera.enabled = true;
            playerController.enabled = true;
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
        //Debug.Log($"enabled: {enabled} movementDisabled: {movementDisabled} camera.enabled: {camera.enabled} playerController.enabled: {playerController.enabled}");

        if (playerSnap > 0 && PlayerPrefs.GetInt("FreeCam") != 1)
        {
            playerController.enabled = false;

            GameObject player = GameObject.FindWithTag("Player");
            string saveString = SceneManager.GetActiveScene().name;

            if (PlayerPrefs.GetFloat($"{saveString}_x") == 0)
            {
                playerSnap = -1;
                return;
            }

            try
            {
                player.transform.position = entryPoints[PlayerPrefs.GetInt("EntryPoint")].transform.position;
            }
            catch (Exception e)
            {
                player.transform.position = entryPoints[0].transform.position;
            }

            playerSnap--;
        }

        if (playerSnap <= 0 && playerSnap > -10)
        {
            camera.enabled = true;
            enabled = false;

            playerController.enabled = true;
            movementDisabled = false;

            playerSnap = -200;
        }

        if (Input.GetKeyDown("c"))
        {
            Toggle();
        }

        if (Input.GetKeyDown("t"))
        {
            TeleportPlayer(Vector3.zero);
            Destroy(GetComponent<DotNavigator>());
        }

        distance -= Input.mouseScrollDelta.y;
        distance = Mathf.Max(-0.1f, distance);

        camera.defaultDistance = distance;

        foreach (GameObject curr in body)
        {
            curr.GetComponent<Renderer>().enabled = (distance != -0.1f);
        }

        if (!enabled) return;

        //player.transform.position = transform.position;

        // Movement controls
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift)) moveDirection *= 3;

        if (!movementDisabled) transform.Translate(moveDirection);
        if (movementDisabled) return;

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

        PlayerPrefs.SetFloat("CamDistance", distance);
    }
}