using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
    public int time = -1;
    public GameObject quest;
    public string QuestName;
    public string QuestDescription;
    public Color Color = Color.white;

    public Image QuestColor;
    public TextMeshProUGUI name;
    public TextMeshProUGUI info;
    public bool questActive = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        name.text = QuestName;
        info.text = QuestDescription;
        QuestColor.color = Color;

        if (time != -1)
        {

        }


        

    }

    // Update is called once per frame
    void Update()
    {
        name.text = QuestName;
        info.text = QuestDescription;
        QuestColor.color = Color;
        if (Input.GetKeyDown("e"))
        {
            questActive = true;
        }
        if (Input.GetKeyDown("q"))
        {
            questActive = false;
        }
        if (questActive)
        {
            quest.GetComponent<Animator>().SetBool("questOn", true);
        }
        else
        {
            quest.GetComponent<Animator>().SetBool("questOn", false);
        }


    }
}
