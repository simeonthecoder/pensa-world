using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerHandler : MonoBehaviour
{
    public GameObject toThrowFrom; // The object you want to position another object under
    public GameObject baitball;  // The object you want to position under 'targetObject'
    public float distanceBelow = 1.0f; // The distance below the target object
    public float throwForce = 0f;

    public GameObject displayPoint;

    public GameObject obj2;
    public GameObject playerCamera;
    public GameObject rod;
    public GameObject targetPosition;
    private bool inside = false;
    private bool active = false;
    public TMP_Text uiText;
    public TMP_Text uiTextLeftMiddle;
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
    public string[] fishes = { "fish (uncommon)", "fish1 (common)", "fish2 (rare)", "fish3 (mythic)", "fish4 (legendary)" };
    public string[] rodTypes = { "default", "normal", "super" };
    public string rodType = "default";
    public double[] rodDurability = { 100, 150, 250 };
    public GameObject[] fishObjs;
    public GameObject fish;
    private bool canThrow = true;
    private bool inWater = false;

    private int[] fishesCatchPulls = {8,13,19,25,31 };
    private int catchesPull = 0;
    private bool hit_play = false;
    public Vector3 rotationSpeed = new Vector3(0, 30, 0); // Speed of rotation (degrees per second)

   
    private int fallTimer = 5;
    
    private bool moveUp = true;

    private LineRenderer lineRenderer;
    public void Start()
    {
        Debug.Log(uiText);
        lineRenderer = GetComponent<LineRenderer>();
        fishing_length = Random.Range(1010f, 4000f);

        rod.SetActive(false);
        rod.GetComponent<Animator>().SetBool("rod_throw", false);
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
        rb.linearVelocity = throwDirection * (throwForce * 3);
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
            uiTextLeftMiddle.text = "Durability " + rodDurability[0];
            lineRenderer.SetPosition(0, baitball.transform.position); // Position of obj1
            lineRenderer.SetPosition(1, obj2.transform.position); // Position of obj2


            if (Input.GetMouseButton(0) && time > cooldown && fishing == true && canThrow == true)
            {
                
                throwForce += 1.6f * Time.deltaTime;
                uiText.text = "Force " + throwForce;
                catchesPull = 0;
                fish.transform.position = new Vector3(
                    displayPoint.transform.position.x,
                    displayPoint.transform.position.y - 400,
                    displayPoint.transform.position.z

                );

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
                rod.GetComponent<Animator>().SetBool("rod_inWater", false);
                inWater = true;
                time = 0f;
                randomFish = Random.Range(0, fishes.Length);
                
            }

            if (time > cooldown)
            {


                fishing = true;
                Debug.Log("wdsadw");
                rod.GetComponent<Animator>().SetBool("rod_throw", false);
                rod.GetComponent<Animator>().SetBool("rod_pull", false);
                rod.GetComponent<Animator>().SetBool("rod_inWater", true);

            }


           
            Debug.Log("fishing_length " + fishing_length);
            if (fishing_length <= 0)
            {
                
                Rigidbody rb = baitball.GetComponent<Rigidbody>();

                rb.constraints = RigidbodyConstraints.FreezePositionZ;
                rb.constraints = RigidbodyConstraints.FreezePositionX;
                
                if (Input.GetKeyDown(KeyCode.Space) && fishing && catchesPull != fishesCatchPulls[randomFish])
                {

                    catchesPull++;
                    if (rodType == rodTypes[0])
                    {
                        rodDurability[0] -= 0.5;
                        Debug.Log("Durability: " + rodDurability[0]);
                    }
                    uiText.text = "pulls " + catchesPull;

                    rod.GetComponent<Animator>().SetBool("rod_throw", false);
                    rod.GetComponent<Animator>().SetBool("rod_pull", true);
                    rod.GetComponent<Animator>().SetBool("rod_inWater", false);

                    float distancePull = (rod.transform.position - baitball.transform.position).magnitude;
                    baitball.transform.position = Vector3.MoveTowards(baitball.transform.position, rod.transform.position, (distancePull / fishesCatchPulls[randomFish]));

                    if (Vector3.Distance(baitball.transform.position, rod.transform.position) <= 4f)
                    {
   
                        baitball.transform.position = rod.transform.position - (rod.transform.position - baitball.transform.position).normalized * 4f;
                        
                    }


                }


                if (catchesPull == fishesCatchPulls[randomFish])
                {

                    fish = fishObjs[randomFish];
                    rod.GetComponent<Animator>().SetBool("rod_throw", false);
                    rod.GetComponent<Animator>().SetBool("rod_pull", true);
                    rod.GetComponent<Animator>().SetBool("rod_inWater", false);
                    inWater = false;
                    uiText.text = "Done!!";

                    
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

                    transform.Rotate(rotationSpeed * Time.deltaTime);
                    // Set object's position
                    fish.transform.position = targetPosition;
                    
                    uiText.text = fishes[randomFish];
                    if (hit_play == false)
                    {
                        RodBack.Play();
                        Debug.Log("BAN-KAI");
                        hit_play = true;
                    }
                    fishing = false;

                }



            }

            if (fishing_length < -1)
            {

                

                if (!Splashes[randomSplash].isPlaying)
                {
                    this.randomSplash = Random.Range(0, Splashes.Length);
                    Splashes[randomSplash].Play();
                    baitball.transform.position = new Vector3(
                        baitball.transform.position.x,
                        baitball.transform.position.y + 0.02f,
                        baitball.transform.position.z
                    );
                }

                if (moveUp)
                {
                    
                    moveUp = false;
                }
                else if (moveUp == false)
                {
                    fallTimer--;
                    //if (fallTimer < 0)
                    //{
                        //fallTimer = 5;
                        //fallAmount = Random.Range(0f, 4f);
                        //moveUp = true;
                    //}
                }
                
                
                
                
            }

            if (fishing == true && inWater == true)
            {
                fishing_length-=Time.deltaTime*60;
            }

            if (fishing == false)
            {


                fish.transform.position = new Vector3(
                    displayPoint.transform.position.x,
                    displayPoint.transform.position.y,
                    displayPoint.transform.position.z
                );  

                fish.transform.position = displayPoint.transform.position;

                baitball.transform.position = obj2.transform.position;
                
                rod.GetComponent<Animator>().SetBool("rod_throw", false);
                rod.GetComponent<Animator>().SetBool("rod_pull", false);
                rod.GetComponent<Animator>().SetBool("rod_inWater", false);
                Vector3 targetPosition = transform.position + Vector3.down * Mathf.Abs(-30f);
                
                inWater = false;
                throwForce = 0f;

                time = cooldown + 1;
                canThrow = true;
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