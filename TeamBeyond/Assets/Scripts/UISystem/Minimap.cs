using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public RectTransform minimapImage;  // Reference to minimap image (RectTransform)
    public Transform player;           // Reference to the player object
    public float mapSize = 150f;       // Size of the map (playing field size)
    public float zoomFactor = 2f;      // Factor to zoom in (higher value = more zoomed in)
    public float offset = 0f;          // Optional offset to fine-tune the minimap position

    void Update()
    {
        // Get player position in the world (2D, ignoring Y-axis for the minimap)
        Vector2 playerPos = new Vector2(player.position.x, player.position.z);

        // Normalize player position in the game world relative to map size
        Vector2 normalizedPos = playerPos / mapSize;

        // Update the minimap position based on the player's position
        minimapImage.anchoredPosition = new Vector2(
            -normalizedPos.x * minimapImage.rect.width * zoomFactor + offset,
            -normalizedPos.y * minimapImage.rect.height * zoomFactor + offset
        );

        // Apply zoom by adjusting the minimap size (localScale)
        minimapImage.localScale = new Vector3(zoomFactor, zoomFactor, 1f);

        // Rotate the minimap to match the player's rotation (Y-axis)
        //minimapImage.rotation = Quaternion.Euler(0, 0, player.rotation.eulerAngles.y);
    }
}
