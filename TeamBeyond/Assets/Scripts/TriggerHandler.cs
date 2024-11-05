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

    private int timer = 3;
    public AudioSource RodBack;
    public AudioSource RodCast;
    public AudioSource[] Splashes;


    private int randomSplash;
    private float time = 0f;
    private float randomValue = 0f;
    private int randomFish = 0;

    private float fishing_length;
    private string[] fishes = { "fort (uncommon)", "fort1 (common)", "fort2 (rare)", "fort3 (mythic)", "fort4 (legendary)" };
    
    private bool hit_play = false;

    public void Start()
    {
        Debug.Log(uiText);

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
                hit_play = false;
                RodCast.Play();
                rod.GetComponent<Animator>().SetBool("rod_throw", true);
                rod.GetComponent<Animator>().SetBool("rod_pull", false);

                time = 0f;
            }

            if (time > cooldown)
            {

                fishing = true;
                Debug.Log("wdsadw");
                rod.GetComponent<Animator>().SetBool("rod_throw", false);
            }

            if (fishing && time < cooldown)
            {
                fishing_length--;
            }
            Debug.Log("fishing_length " + fishing_length);
            if (fishing_length <= 0)
            {
                uiText.text = "Натисни SPACE";

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) && fishing)
                {
                    rod.GetComponent<Animator>().SetBool("rod_pull", true);
                    rod.GetComponent<Animator>().SetBool("rod_throw", false);

                    randomValue = Random.Range(40f, 150f);
                    randomFish = Random.Range(0, fishes.Length);

                    Debug.Log(fishes[randomFish]);
                    fishing = false;
                    fishing_length = randomValue;
                    
                    Debug.Log("fishing_length " + fishing_length);

                    uiText.text = "";
                }
            }

            if (fishing_length < -1 && hit_play == false)
            {
                this.randomSplash = Random.Range(0, Splashes.Length);
         
                Splashes[randomSplash].Play();
                
                
                
                hit_play = true;
            }

            if (fishing == false)
            {
                RodBack.Play();
                //rod.GetComponent<Animator>().SetBool("rod_pull", false);
                time = cooldown + 1;
            }

        }

    }

    public void OnTriggerEnter(Collider collision)
    {
        inside = true;
    }
}