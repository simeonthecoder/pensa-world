using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class TriggerHandler : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject rod;
    public GameObject targetPosition;
    private bool inside = false;
    private bool active = false;
    public TMP_Text uiText;
    public float cooldown;
    public bool fishing = false;

    private float time = 0f;
    private float randomValue = 0f;

    private float fishing_length;

    public void Start()
    {
        fishing_length = Random.Range(40f, 200f);
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
                fishing = true;

                Debug.Log("wdsadw");
                rod.GetComponent<Animator>().SetBool("rod_throw", false);

                uiText.text = "GEIIII ICE";
            }

            if (fishing == true)
            {
                Debug.Log("fishing_length " + fishing_length);
                fishing_length--;
            }

            if (fishing_length <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) && fishing)
                {
                    uiText.text = "Press SPACE";

                    rod.GetComponent<Animator>().SetBool("rod_pull", true);

                    randomValue = Random.Range(4f, 20f);
                    fishing = false;
                    fishing_length = randomValue;
                    Debug.Log("fishing_length " + fishing_length);
                }
            }

            if (fishing == false)
            {
                rod.GetComponent<Animator>().SetBool("rod_pull", false);
                
            }

        }

    }

    public void OnTriggerEnter(Collider collision)
    {
        inside = true;
    }
}