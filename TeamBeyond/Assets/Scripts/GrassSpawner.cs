using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    public GameObject grassPlane;

    public int levelsCount;
    public float heightDistance;

    private List<GameObject> grassObjects;

    // Start is called before the first frame update
    void Start()
    {
        grassObjects = new List<GameObject>();

        for(int i = 0; i < levelsCount; i ++)
        {
            GameObject currPlane = Instantiate(grassPlane);
            currPlane.transform.position = grassPlane.transform.position + new Vector3(
                0f, heightDistance * (i + 1), 0f
            );

            currPlane.transform.SetParent(grassPlane.transform.parent);

            currPlane.GetComponent<AttachTo>().target = grassPlane;

            grassObjects.Add(currPlane);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle(bool status)
    {
        foreach (GameObject plane in grassObjects)
        {
            plane.SetActive(status);
        }

        grassPlane.SetActive(status);
    }
}
