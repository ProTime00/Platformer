using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelParser : MonoBehaviour
{
    private readonly string[] _filename = {"Level1", "Level2"};
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public Transform environmentRoot;
    public GameObject waterPrefab;
    public GameObject goldPrefab;
    public int level;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel(level);
    }

    // --------------------------------------------------------------------------
    private void LoadLevel(int levelToLoad)
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{_filename[levelToLoad]}.txt";

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }

        int row = 0;
        // Go through the rows from bottom to top
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] letters = currentLine.ToCharArray();
            for (int column = 0; column < letters.Length; column++)
            {
                var letter = letters[column];
                // Todo - Instantiate a new GameObject that matches the type specified by letter
                // Todo - Position the new GameObject at the appropriate location by using row and column
                // Todo - Parent the new GameObject under levelRoot
                var pos = new Vector3(column, row, 0f);
                GameObject block = null;

                switch (letter)
                {
                    case 'x':
                        block = rockPrefab;
                        break;
                    case 'b':
                        block = brickPrefab;
                        break;
                    case '?':
                        block = questionBoxPrefab;
                        break;
                    case 's':
                        block = stonePrefab;
                        break;
                    case 'w':
                        block = waterPrefab;
                        break;
                    case 'g':
                        block = goldPrefab;
                        break;
                }

                if (block is not null)
                {
                    Instantiate(block, pos, Quaternion.identity, environmentRoot);
                }
            }

            row++;
        }
    }
}
