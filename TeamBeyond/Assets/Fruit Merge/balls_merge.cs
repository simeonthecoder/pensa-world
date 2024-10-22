using UnityEngine;

public class balls_merge : MonoBehaviour
{
    public GameObject endLine;

    public AudioSource balls_merge_sfx;
    private static bool hasReplaced = false;
    public GameObject[] balls;
    private bool save;

    void Start()
    {
        bool save = false;
     
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
                    midpoint.y += 1f;
                    Instantiate(balls[++i], midpoint, Quaternion.identity);
                    balls_merge_sfx.Play();


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


    void Update()
    {
        Vector3 position = transform.position;

        
        if (position.y < endLine.transform.position.y)
        {
            //Debug.Log("true saveae");
            save = true;
        }

  
        if (position.y > endLine.transform.position.y && save == true)
        {
            foreach (GameObject obj in balls)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }
                
        }

    }
}


