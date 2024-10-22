using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class scoreSystem : MonoBehaviour
{
    public TMP_Text textMeshPro;

    public List<GameObject> objectList;
    public int[] scoreAdds;
    public string[] ballsTags;
    int score = 0;
    public int totalScore;


    void UpdateTextContent(string newText)
    {

        textMeshPro.text = newText;

    }

    void Start()
    {

        
       
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (!objectList.Contains(collider.gameObject))
        {
            objectList.Add(collider.gameObject);
            

            
            for (int i = 0; i < ballsObj.Length; i++)
            {
                if (ballsObj[i] == collider.gameObject)
                {
                    score = scoreAdds[i]; 
                    totalScore += score; 
                    
                }
            }

            
            UpdateTextContent("Score: " + totalScore.ToString());
            Debug.Log(collider.gameObject.name + " added to the list with score: " + score);
        }

    }


    void OnTriggerExit2D(Collider2D collider)
    {
        
        if (objectList.Contains(collider.gameObject))
        {
            objectList.Remove(collider.gameObject);
 
            UpdateTextContent("Score: " + totalScore.ToString());
            Debug.Log(collider.gameObject.name + " removed from the list.");
        }
    }

    void Update()
    {

       
        

    }
}


