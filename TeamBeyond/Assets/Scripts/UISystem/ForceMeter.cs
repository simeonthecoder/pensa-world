using UnityEngine;

public class ForceMeter : MonoBehaviour
{
    public GameObject bar_bottom;
    public GameObject bar_top; 
    public Material lineMaterial; 

    private LineRenderer lineRenderer;

    public float moveSpeed = 0.1f; 
    public float fallSpeed = 0.1f;
    public float maxHeightAboveBar = 170f;

    private bool goUp = false;

    public bool spamTurn = false;
    public bool holdTurn = false;
    

    void Start()
    {

        lineRenderer = GetComponent<LineRenderer>();

        
        lineRenderer.startWidth = 63f;
        lineRenderer.endWidth = 63f;
        lineRenderer.positionCount = 2;

       
        if (lineMaterial != null)
        {
            lineRenderer.material = lineMaterial;
        }
    }

    void Update()
    {
        // Auto move limit up and down

        //if (!goUp)
        //{
        //limit.transform.position -= Vector3.up * moveSpeed;
        //if (limit.transform.position.y < bar.transform.position.y)
        //{
        //goUp = true;
        //}
        //}
        //else
        //{
        //limit.transform.position += Vector3.up * moveSpeed;
        //if (limit.transform.position.y > bar.transform.position.y + maxHeightAboveBar)
        //{
        //goUp = false;
        //}
        //}


        //spam to progress
        if (spamTurn)
        {
            holdTurn = false;
            //moveSpeed has to be bigger than fall speed
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {

                if (bar_top.transform.position.y < bar_bottom.transform.position.y + maxHeightAboveBar)
                {
                    bar_top.transform.position += Vector3.up * moveSpeed;
                    Debug.Log("NATISKASH ME");
                }

            }
            else
            {
                if (bar_top.transform.position.y > bar_bottom.transform.position.y)
                {
                    bar_top.transform.position -= Vector3.up * fallSpeed;
                }
            }
        }
        if (holdTurn)
        {
            spamTurn = false;
            //hold to move

            if (Input.GetMouseButton(0))
            {

                if (bar_top.transform.position.y < bar_bottom.transform.position.y + maxHeightAboveBar)
                {

                    bar_top.transform.position += Vector3.up * moveSpeed;
                    Debug.Log("2");

                }

            }
            else
            {

                if (bar_top.transform.position.y > bar_bottom.transform.position.y)
                {
                    bar_top.transform.position -= Vector3.up * fallSpeed;
                }

            }
        }


        // Manual movement with keys
        if (Input.GetKey(KeyCode.B) && bar_top.transform.position.y < bar_bottom.transform.position.y + maxHeightAboveBar)
        {
            bar_top.transform.position += Vector3.up * moveSpeed;
        }

        if (Input.GetKey(KeyCode.N) && bar_top.transform.position.y > bar_bottom.transform.position.y)
        {
            bar_top.transform.position -= Vector3.up * moveSpeed;
        }

        // Calculate progress (0 to 1)
        float currentHeight = bar_top.transform.position.y - bar_bottom.transform.position.y;
        float progress = Mathf.Clamp01(currentHeight / maxHeightAboveBar);

        // Update line positions
        lineRenderer.SetPosition(0, bar_bottom.transform.position);
        lineRenderer.SetPosition(1, bar_top.transform.position);

        // Color transition: Red → Yellow → Green
        Color color;
        if (progress < 0.5f) // 0% to 50% (Red to Yellow)
        {
            color = Color.Lerp(Color.red, Color.yellow, progress * 2f);
        }
        else // 50% to 100% (Yellow to Green)
        {
            color = Color.Lerp(Color.yellow, Color.green, (progress - 0.5f) * 2f);
        }

        // Apply the color to the material
        if (lineMaterial != null)
        {
            lineMaterial.color = color;
        }

        // Debug log progress
        Debug.Log($"Progress: {progress * 100:F1}% - Color: {color}");
    }
}
