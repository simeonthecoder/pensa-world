using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    public GameObject grassPlane;

    public int levelsCount;
    public float heightDistance;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < levelsCount; i ++)
        {
            GameObject currPlane = Instantiate(grassPlane);
            currPlane.transform.position = grassPlane.transform.position + new Vector3(
                0f, heightDistance * (i + 1), 0f
            );

            currPlane.transform.SetParent(grassPlane.transform.parent);

            currPlane.GetComponent<AttachTo>().target = grassPlane;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
