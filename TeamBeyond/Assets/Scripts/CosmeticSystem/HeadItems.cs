using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    [SerializeField] private MeshFilter head;
    [SerializeField] private MeshFilter body;
    [SerializeField] private MeshFilter legs;

    [SerializeField] private MeshFilter[] headItems;
    [SerializeField] private MeshFilter[] bodyItems;
    [SerializeField] private MeshFilter[] legItems;

    private Dictionary<int, (MeshFilter bodyPart, MeshFilter[] bodyPartItems)> Cosmetics;

    private int currentIndex = 0;
    int cosmeticSlotSelector = 0;

    private void Start() {
        Cosmetics = new();

        Cosmetics.Add(0, (head, headItems));
        Cosmetics.Add(1, (body, bodyItems));
        Cosmetics.Add(2, (legs, legItems));
    }

    private int GetInputDirection()
    {
        bool left = Input.GetKeyDown(KeyCode.LeftArrow);
        bool right = Input.GetKeyDown(KeyCode.RightArrow);

        bool change = left || right;
        if(!change) return -1;

        if(!left && right) left = false;

        return left ? -1 : 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) cosmeticSlotSelector++;
        cosmeticSlotSelector = cosmeticSlotSelector % 3;

        int direction = GetInputDirection();
        if(direction == -1) return;

        SwitchCosmetic(direction, Cosmetics[cosmeticSlotSelector].bodyPartItems.Length);
        SetItem(Cosmetics[cosmeticSlotSelector].bodyPart, Cosmetics[cosmeticSlotSelector].bodyPartItems);

        Debug.Log(currentIndex);
    }

    private int InBounds(int currentIndex, int upperBound)
    {
        if (currentIndex < 0) currentIndex = upperBound - 1;
        if (currentIndex >= upperBound) currentIndex = 0;

        return currentIndex;
    }

    public void SwitchCosmetic(int direction, int upperBound)
    {
        currentIndex = InBounds(currentIndex + direction, upperBound);
    }

    public void SetItem(MeshFilter meshfilter, MeshFilter[] Items)
    {
        meshfilter.mesh = Items[currentIndex].mesh;
    }
}
