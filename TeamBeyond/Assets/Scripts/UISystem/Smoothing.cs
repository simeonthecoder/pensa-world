using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // The player to follow
    public Vector3 offset = new Vector3(0, 5, -5); // Offset position
    public float smoothSpeed = 0.125f; // Smoothness factor

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate desired position
            Vector3 desiredPosition = target.position + offset;

            // Smoothly move the camera to the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Optional: Look at the player
            transform.LookAt(target);
        }
    }
}
