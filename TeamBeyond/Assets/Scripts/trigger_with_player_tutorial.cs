using UnityEngine;

public class trigger_with_player_tutorial : MonoBehaviour
{
    public GameObject player;
    public bool inside;

    void Start()
    {
        
    }

    void Update()
    {

    

    }
    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("its inside");
        inside = true;
    }
}
