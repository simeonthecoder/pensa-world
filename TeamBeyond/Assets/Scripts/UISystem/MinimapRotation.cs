using UnityEngine;

public class MinimapRotation : MonoBehaviour
{
    public Transform cameraTransform;  // Reference to the main camera or player camera

    void Update()
    {
        // Rotate the minimap mask to match the camera's Y-axis rotation
        // We only need to rotate on the Y-axis to match the camera's rotation.
        transform.rotation = Quaternion.Euler(0f, 0f, cameraTransform.rotation.eulerAngles.y);
    }
}
