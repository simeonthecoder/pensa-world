using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

public class PlayerTyper : MonoBehaviour
{
    public WordEngine wordEngine;

    public GameObject font;
    public GameObject square;

    private Sprite[] letters;
    private int index = 0, vlineIndex = 0;

    private Stack<GameObject> keysPressed = new();
    private List<int> keys = new();

    private List<GameObject> screen = new();

    private bool idling;

    private Dictionary<char, int> mappings = new()
    {
        { 'a', 0 }, { 'b', 1 }, { 'c', 22 }, { 'd', 4 }, { 'e', 5 }, { 'f', 20}, { 'g', 3 }, { 'h', 21 }, { 'i', 8 },
        { 'j', 9 }, { 'k', 10 }, { 'l', 11 }, { 'm', 12}, { 'n', 13 }, { 'o', 14 }, { 'p', 15 }, { 'q', 29 }, { 'r', 16 },
        { 's', 17 }, { 't', 18 }, { 'u', 19 }, { 'v', 6 }, { 'w', 2 }, { 'x', 26 }, { 'y', 27 }, { 'z', 7 }, { '`', 23 },
        { '[', 24 }, { ']', 25 }, { '\\', 28 }
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.letters = Resources.LoadAll<Sprite>("font");
        Debug.Log(letters.Length);
    }

    // Update is called once per frame
    void Update()
    {
        string input = Input.inputString;

        if (Input.GetKeyDown(KeyCode.Backspace) && index > 0)
        {
            index--;
            Destroy(keysPressed.Pop());
            keys.RemoveAt(keys.Count - 1);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !idling)
        {
            bool r = wordEngine.Send(keys);

            if (r)
            {
                keys.Clear();

                vlineIndex++;
                keysPressed.Clear();

                index = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && idling)
        {
            Clear();
            idling = false;

            wordEngine.PickWord();
        }
        else if (!string.IsNullOrEmpty(input) && index < wordEngine.LetterCount)
        {
            int key = mappings[input[0]];

            GameObject currLetter = Instantiate(font);

            currLetter.transform.position = new Vector3(
                transform.position.x + index++ * 0.8f,
                transform.position.y - vlineIndex * 1.3f,
                0
            );

            keysPressed.Push(currLetter);
            keys.Add(key);

            currLetter.GetComponent<SpriteRenderer>().sprite = this.letters[key];
            currLetter.GetComponent<SpriteRenderer>().color = Color.white;

            screen.Add(currLetter);
        }
    }

    public void Feedback(int[] mask)
    {
        for (int i = 0; i < mask.Length; i++)
        {
            GameObject currSquare = Instantiate(square);
            currSquare.transform.position = new Vector3(transform.position.x + i * 0.8f, transform.position.y - vlineIndex * 1.3f, 0);

            Color col;

            switch (mask[i])
            {
                case 1:
                    col = new Color(0.7f, 0.7f, 0);
                    break;

                case 2:
                    col = Color.green;
                    break;

                default:
                    col = Color.gray;
                    break;
            }

            currSquare.GetComponent<SpriteRenderer>().color = col;

            screen.Add(currSquare);
        }

        if (string.Join("", mask) == new string('2', wordEngine.LetterCount)) idling = true;
    }
    public void Clear()
    {
        foreach (var item in screen) Destroy(item);
        screen.Clear();

        index = 0;
        vlineIndex = 0;
    }

    public void Idle()
    {
        idling = true;
    }
}
