using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DistrictSpawner : MonoBehaviour
{
    public GameObject streetBlock;

    public int sizeX;
    public int sizeY;

    public bool spawn = false;
    public bool spawnOnStart;

    public void Spawn()
    {
        for(int i = 0; i < sizeX; i ++)
        {
            for(int j = 0; j < sizeY; j ++)
            {
                GameObject currBlock = Instantiate(streetBlock);

                currBlock.transform.position = this.transform.position + new Vector3(i * 10 - sizeX * 5 + 5, 0.1f, j * 10 - sizeY * 5 + 5);
                currBlock.transform.rotation = this.transform.rotation;

                // currBlock.GetComponent<StreetBlock>().Spawn();
            }
        }
    }

    void Start()
    {
        if(spawnOnStart)
        {
            Spawn();
        }
    }

    void Update()
    {
        if(spawn)
        {
            spawn = false;
            Spawn();
        }
    }
}
