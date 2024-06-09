using UnityEngine;
using UnityEngine.UI;

public class TriggerHandler : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject rod;
    public GameObject targetPosition;
    private bool inside = false;
    private bool active = false;
    public Text uiText;
    public float cooldown;

    private float time = 0f;
    private float randomValue = 0f;

    public TextController textController;

    private float fishing_lenght;

    public void Start()
    {
        randomValue = Random.Range(4f, 20f);
        Debug.Log("starting niga");
        rod.SetActive(false);
    }

    public void Update()
    {
        time += Time.deltaTime;

        if (!inside && !active) return;

        if (Input.GetKeyDown("e"))
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
            if (Input.GetMouseButtonDown(0) && time > cooldown)
            {
                Debug.Log("hvurlqi se ot mosta");
                rod.GetComponent<Animator>().SetBool("rod_throw", true);
                time = 0f;
            }

            if (time > cooldown)
            {
                fishing_lenght--;
                rod.GetComponent<Animator>().SetBool("rod_throw", false);
            }

            if (Input.GetKeyDown(KeyCode.Space) && fishing_lenght <= 0)
            {

                textController.ChangeText("Press SPACE");
                rod.GetComponent<Animator>().SetBool("rod_pull", true);

                randomValue = Random.Range(4f, 20f);

            }
        }


    }

    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("GEI NEGUR");
        inside = true;
    }
}