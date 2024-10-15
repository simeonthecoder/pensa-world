using UnityEngine;

public class placement_controller : MonoBehaviour
{
    public GameObject display_ball;
    public GameObject ball00;
    public GameObject ball01;
    public GameObject ball02;
    public GameObject ball03;
    public GameObject ball04;

    public AudioSource balls_drop_sfx;

    private bool ball_displaying_next = false;
    
    private GameObject nextBallDisplay;
    public GameObject[] balls_placeing;

    public int randomBall;

    public Transform targetObject;  
    private Vector3 offset = new Vector3(0, -1, 0); 

    public float sensitivity = 0.1f;  
    private Vector3 lastMousePosition;

    public float clickCooldown = 1.0f; 
    private float lastClickTime = 0f; 

    public float minX = -5f; 
    public float maxX = 5f;  

    void Start()
    {
        balls_placeing = new GameObject[] { ball00, ball01, ball02, ball03, ball04};
        bool ball_displaying_next = false;
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        
        Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;

        
        float newX = transform.position.x + (deltaMousePosition.x * sensitivity);

        
        newX = Mathf.Clamp(newX, minX, maxX);

        
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        
        lastMousePosition = Input.mousePosition;


        if (ball_displaying_next == false)
        {
            this.randomBall = Random.Range(0, balls_placeing.Length);
            nextBallDisplay = Instantiate(balls_placeing[this.randomBall], display_ball.transform.position, Quaternion.identity);
            ball_displaying_next = true;
        }



        if (Input.GetMouseButtonDown(0) && Time.time >= lastClickTime + clickCooldown)
        {
            balls_drop_sfx.Play();
            GameObject newObject = Instantiate(balls_placeing[this.randomBall]);
            newObject.transform.position = targetObject.position + offset;

            
            if (nextBallDisplay != null)
            {
                Destroy(nextBallDisplay);
            }

            lastClickTime = Time.time;
            ball_displaying_next = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            
            Debug.Log("Current X Position: " + transform.position.x);
        }
    }
}
