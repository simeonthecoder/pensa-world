using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class WordEngine : MonoBehaviour
{
    public PlayerTyper playerTyper;

    private List<int> wordPicked;
    private List<int> correctWord;

    private List<string> validWords;

    public int LetterCount = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Random.seed = 42;
        validWords = new();
        PickWord();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickWord()
    {
        int wordIndex = Random.Range(0, 11264-1);

        StreamReader reader = new StreamReader("Assets\\usable.txt");

        bool fillValid = (validWords.Count == 0);

        for (int i = 0; i < 11264 && !reader.EndOfStream; i++)
        {
            string word = reader.ReadLine();

            if (i == wordIndex) this.correctWord = word.Split(" ").Select(int.Parse).ToList();
            if (fillValid) validWords.Add(string.Join(" ", word.Split(" ").Select(int.Parse).ToList()));
        }

        for (int i = 0; i < wordIndex - 1; i++) reader.ReadLine();
    }

    public bool Send (List<int> word)
    {
        if (!validWords.Contains(string.Join(" ", word))) return false;

        int[] mask = new int[LetterCount];

        List<int> copy = new();
        for (int i = 0; i < correctWord.Count; i++)
            copy.Add(correctWord[i]);

        for (int i = 0; i < word.Count; i ++)
        {
            if (word[i] == correctWord[i])
            {
                mask[i] = 2;
                copy.Remove(word[i]);
            }
        }

        for (int i = 0; i < word.Count; i++)
        {
            if (copy.Contains(word[i]))
            {
                mask[i] = 1;
                copy.Remove(word[i]);
            }
        }

        playerTyper.Feedback(mask);

        return true;
    }
}
