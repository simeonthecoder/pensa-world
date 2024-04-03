using UnityEditor;
using UnityEngine;
public class CharacterCustomization : MonoBehaviour
{
    [SerializeField] private MeshFilter Head;
    [SerializeField] private MeshFilter[] HeadItems;
    [SerializeField] private MeshFilter Body;
    [SerializeField] private Mesh[] Bodytems;
    [SerializeField] private MeshFilter legs;
    [SerializeField] private Mesh[] LegItems;
    private int currentIndex = 0;

    int counter = 0;

    private void Start()
    {
        SwitchCosmeticLeft(Head, HeadItems);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            counter++;
        }
        switch (counter)
        {   

            case 0:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SwitchCosmeticLeft(Head, HeadItems);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SwitchCosmeticRight(Head, HeadItems);
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SwitchCosmeticLeft(Head, HeadItems);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SwitchCosmeticRight(Head, HeadItems);
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SwitchCosmeticLeft(Head, HeadItems);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SwitchCosmeticRight(Head, HeadItems);
                }
                break;

            case 3:
                counter = 0;
                break;
        }

        

        
    }

    public void SwitchCosmeticLeft(MeshFilter meshfilter, MeshFilter[] Items)
    {
        meshfilter.mesh = Items[currentIndex].mesh;
        currentIndex++;
        if (currentIndex >= Items.Length)
        {
            currentIndex = 0;
        }
    }
    public void SwitchCosmeticRight(MeshFilter meshfilter, MeshFilter[] Items)
    {
        meshfilter.mesh = Items[currentIndex].mesh;
        currentIndex--;
        if (currentIndex <= 0)
        {
            currentIndex = Items.Length - 1;
        }
    }
}
