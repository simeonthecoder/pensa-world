using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    private bool inside = false;
    public int offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!inside) return;

        if (Input.GetKeyDown("e"))
        {
            Debug.Log(offset == 0 ? 1 : offset);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + (offset == 0 ? 1 : offset));
        }
    }

    void OnTriggerEnter()
    {
        inside = true;
    }
}
