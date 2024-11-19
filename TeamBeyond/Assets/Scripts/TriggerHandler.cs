using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class TriggerHandler : MonoBehaviour
{
    public GameObject toThrowFrom; // The object you want to position another object under
    public GameObject baitball;  // The object you want to position under 'targetObject'
    public float distanceBelow = 1.0f; // The distance below the target object
    public float throwForce = 0f;


    public GameObject obj2;
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
    public GameObject fish;
    private bool canThrow = true;
    private bool inWater = false;

    private int[] fishesCatchPulls = {13,22,30,31 };
    private int catchesPull = 0;
    private bool hit_play = false;
    public Vector3 rotationSpeed = new Vector3(0, 30, 0); // Speed of rotation (degrees per second)

    private LineRenderer lineRenderer;
    public void Start()
    {
        Debug.Log(uiText);
        lineRenderer = GetComponent<LineRenderer>();
        fishing_length = Random.Range(4010f, 8000f);
        rod.SetActive(false);
    }


    public void FishingRodThrow()
    {

        baitball.transform.position = toThrowFrom.transform.position;
        // Ensure the baitBall has a Rigidbody component
        Rigidbody rb = baitball.GetComponent<Rigidbody>();


        FreeCamera freeCam = playerCamera.GetComponent<FreeCamera>();

        Vector3 cameraForward = freeCam.transform.forward;
        cameraForward.y = 0; // Optional: Ignore vertical direction if you want a flat throw


        // Normalize the direction to avoid scaling issues
        Vector3 throwDirection = cameraForward.normalized;

        // Apply a velocity to the baitBall in the direction of the specified camera
        rb.linearVelocity = throwDirection * (throwForce / 8);
    }

    public void Update()
    {
        time += Time.deltaTime;

        if (!inside && !active) return;


        if (Input.GetKeyDown("e"))
        {
            if (active)
            {
                active = false;
            }
            else
            {
                active = true;
            }
            
            FreeCamera freeCam = playerCamera.GetComponent<FreeCamera>();

            freeCam.Toggle();
            freeCam.movementDisabled = false;

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
            rod.SetActive(true);
            lineRenderer.SetPosition(0, baitball.transform.position); // Position of obj1
            lineRenderer.SetPosition(1, obj2.transform.position); // Position of obj2


            if (Input.GetMouseButton(0) && time > cooldown)
            {
                uiText.text = "Force " + ++throwForce;
                canThrow = true;


            }
            else if ((!Input.GetMouseButton(0))&& canThrow == true && time > cooldown)
            {
                FishingRodThrow();
                Debug.Log("hvurlqi se ot mosta");
                hit_play = false;
                RodCast.Play();
                canThrow = false;
                rod.GetComponent<Animator>().SetBool("rod_throw", true);
                rod.GetComponent<Animator>().SetBool("rod_pull", false);
                inWater = true;
                time = 0f;
                randomFish = Random.Range(0, fishes.Length);
                
            }

            if (time > cooldown)
            {

                fishing = true;
                Debug.Log("wdsadw");
                rod.GetComponent<Animator>().SetBool("rod_throw", false);
                rod.GetComponent<Animator>().SetBool("rod_inWater", true);

            }

           
            Debug.Log("fishing_length " + fishing_length);
            if (fishing_length <= 0)
            {
                
                
                if (Input.GetKeyDown(KeyCode.Space) && fishing && catchesPull != fishesCatchPulls[randomFish])
                {

                    catchesPull++;
                    uiText.text = "pulls " + catchesPull;

                    rod.GetComponent<Animator>().SetBool("rod_inWater", false);
                    rod.GetComponent<Animator>().SetBool("rod_pull", true);
                    

                }

                if (catchesPull == fishesCatchPulls[randomFish])
                {
                    rod.GetComponent<Animator>().SetBool("rod_inWater", false);
                    rod.GetComponent<Animator>().SetBool("rod_pull", true);
                    inWater = false;
                    uiText.text = "Done!!";

                    randomFish = Random.Range(0, fishes.Length);
                    
                    Debug.Log(fishes[randomFish]);
                    fishing = false;
                    fishing_length = Random.Range(4010f, 8000f);

                    Debug.Log("fishing_length " + fishing_length);

                    FreeCamera freeCam = playerCamera.GetComponent<FreeCamera>();
                    // Calculate the target position
                    Vector3 forward = freeCam.transform.forward;
                    Vector3 right = freeCam.transform.right;
                    Vector3 up = freeCam.transform.up;

                    // Position slightly in front of the camera
                    Vector3 targetPosition = freeCam.transform.position +
                                             forward * 2f +
                                             up * -0.5f +
                                             right * 0f;
                    
                    transform.Rotate(rotationSpeed* Time.deltaTime);
                    // Set object's position
                    fish.transform.position = targetPosition;

                    


                    uiText.text = "";
                }

            }

            if (fishing_length < -1)
            {
                
                
                if (!Splashes[randomSplash].isPlaying)
                {
                    this.randomSplash = Random.Range(0, Splashes.Length);
                    Splashes[randomSplash].Play();
                }
                
                
                
                
                
            }

            if (fishing == true && inWater == true)
            {
                fishing_length--;
            }

            if (fishing == false)
            {
                Vector3 targetPosition = transform.position + Vector3.down * Mathf.Abs(-30f);
                fish.transform.position = targetPosition;
                inWater = false;
                throwForce = 0f;
                catchesPull = 0;
                baitball.transform.position = obj2.transform.position;
                RodBack.Play();
                rod.GetComponent<Animator>().SetBool("rod_pull", true);
                time = cooldown + 1;
            }

        }
        else
        {
            rod.SetActive(false);
            
            
        }

    }



    public void OnTriggerEnter(Collider collision)
    {
        inside = true;
    }
}