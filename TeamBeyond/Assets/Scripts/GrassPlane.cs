using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassPlane : MonoBehaviour
{
    public int index;

    public float windSpeed;
    public Vector2 windDirection;

    public float strength;

    private Vector3 startState;
    private float time;

    public void Start()
    {
        this.startState = transform.position;
        this.time = 0f;
    }

    private void Update()
    {
        this.time += Time.deltaTime;

        Vector2 windVector = windSpeed * windDirection;

        transform.position = startState + new Vector3(
            (Mathf.PerlinNoise(time * windVector.x, time * windVector.y) - 0.5f) * index * index * strength,
            0f,
            (Mathf.PerlinNoise(time * windVector.y, time * windVector.x) - 0.5f) * index * index * strength
        );
    }
}
