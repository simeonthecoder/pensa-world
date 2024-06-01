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

            Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
            string saveString = SceneManager.GetActiveScene().name;
            
            PlayerPrefs.SetFloat($"{saveString}_x", playerPos.x);
            PlayerPrefs.SetFloat($"{saveString}_y", playerPos.y);
            PlayerPrefs.SetFloat($"{saveString}_z", playerPos.z);

            GameObject.FindWithTag("MainCamera").GetComponent<FreeCamera>().SaveData();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + (offset == 0 ? 1 : offset));
        }
    }

    void OnTriggerEnter()
    {
        inside = true;
    }

    void OnTriggerExit()
    {
        inside = false;
    }
}
