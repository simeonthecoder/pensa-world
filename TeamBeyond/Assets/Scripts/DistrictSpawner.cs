using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictSpawner : MonoBehaviour
{
    public GameObject streetBlock;

    public Vector2 distanceMultiplier;

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

                currBlock.transform.position = this.transform.position + new Vector3(
                    i * 11 * distanceMultiplier.x - sizeX * 5 + 5,
                    0.1f, j * 11 * distanceMultiplier.y - sizeY * 5 + 5
                );
                
                currBlock.transform.rotation = this.transform.rotation;

                // currBlock.GetComponent<StreetBlock>().Spawn();
            }
        }

        if(distanceMultiplier == new Vector2(0, 0))
        {
            distanceMultiplier = new Vector2(1, 1);
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
