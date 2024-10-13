using UnityEngine;

public class balls_merge : MonoBehaviour

{

    public GameObject replacementObject;
    public GameObject ball00;
    public GameObject ball01;
    public GameObject ball02;


    private static bool hasReplaced = false;
    public GameObject[] balls;

    void Start()
    {

        balls = new GameObject[] { ball00,ball01, ball02 };
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collided with has the same tag and ensure replacement happens only once globally
        if (collision.gameObject.tag == gameObject.tag && !hasReplaced)
        {
            for (int i = 0; i < balls.Length; i++)
            {
                if (gameObject.tag == balls[i].tag)
                {

                    Vector3 midpoint = (transform.position + collision.transform.position) / 2;
                    Instantiate(balls[i += 1], midpoint, Quaternion.identity);
                    // Destroy both objects involved in the collision
                    Destroy(collision.gameObject);
                    hasReplaced = true;
                    Destroy(gameObject);

                }
            }
        }

        else
        {
            hasReplaced = false;
        }
    }

}


