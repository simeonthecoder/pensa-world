using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetBlock : MonoBehaviour
{
    public GameObject layouts;

    public bool spawn = false;
    public bool spawnOnStart;

    public Vector3 offset;

    private int[] rotations = {
        0, 90, -90, 180
    };

    public void Spawn()
    {
        int rot = Random.Range(0, 4);

        GameObject layout = Instantiate(
            layouts.transform.GetChild(Random.Range(0, layouts.transform.childCount)).gameObject,
            transform.position + offset,
            transform.rotation * Quaternion.Euler(0, rotations[rot], 0)
        );

        // layout.GetComponent<StreetBlock>().Spawn();
    }

    void Start()
    {
        if(spawnOnStart
            //&& !Application.isPlaying
        )
        {
            Spawn();
        }
    }

    void Update()
    {
        if(spawn)
        {
            Spawn();
            spawn = false;
        }
    }
}
