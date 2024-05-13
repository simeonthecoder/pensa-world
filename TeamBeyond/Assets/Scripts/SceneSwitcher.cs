using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Check if the "F" key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Get the index of the current scene
            int currentIndex = SceneManager.GetActiveScene().buildIndex;

            // Calculate the index of the next scene
            int nextIndex = (currentIndex + 1) % SceneManager.sceneCountInBuildSettings;

            GetComponent<FreeCamera>().SaveData();

            // Load the next scene
            SceneManager.LoadScene(nextIndex);
        }
    }
}