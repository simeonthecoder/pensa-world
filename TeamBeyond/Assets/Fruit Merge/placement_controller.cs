using UnityEngine;

public class placement_controller : MonoBehaviour
{

   

    

    public GameObject ball00;
    public GameObject ball01;
    public GameObject ball02;

    private GameObject[] balls;

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
        balls = new GameObject[] { ball00, ball01, ball02 };
        
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        // Calculate how much the mouse moved since the last frame
        Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;

        // Move the object along the X-axis based on mouse movement
        float newX = transform.position.x + (deltaMousePosition.x * sensitivity);

        // Clamp the X position between minX and maxX
        newX = Mathf.Clamp(newX, minX, maxX);

        // Update the object's position
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Update the last mouse position for the next frame
        lastMousePosition = Input.mousePosition;

        // Check if the left mouse button is pressed and if the cooldown has passed
        if (Input.GetMouseButtonDown(0) && Time.time >= lastClickTime + clickCooldown)
        {
            // Create a copy of the object
            int randomNumber = Random.Range(0, balls.Lenght);
            GameObject newObject = Instantiate(ball02);

            // Position the new object at the target object's position, but below it (using the offset)
            newObject.transform.position = targetObject.position + offset;

            // Update the last click time to the current time
            lastClickTime = Time.time;
        }

        // Check if the F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Log the current X position of the object to the debug console
            Debug.Log("Current X Position: " + transform.position.x);
        }
    }
}
