using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogueLine
{
    // All Ids
    public int textId;

    public string characterName;
    public string characterImage;

    public string text;

    public int nextId;

    public int specialActionId; //1=Option 2=End Story 3=Show Doc
    public int documentSetId;

    public string option1;
    public int option1NextId;

    public string option2;
    public int option2NextId;

    public string option3;
    public int option3NextId;

    public string option4;
    public int option4NextId;

    public string option5;
    public int option5NextId;
}

public class DialogueLoader : MonoBehaviour
{
    public Dictionary<int, DialogueLine> dialogueDict;

    [SerializeField]
    private TextAsset csvFile;

    private void Awake()
    {
        LoadCSV();

        //Debug.Log(dialogueDict[1].characterName + "£º" + dialogueDict[1].text);
    }

    private void LoadCSV()
    {
        dialogueDict = new Dictionary<int, DialogueLine>();
        //TextAsset csvFile = Resources.Load<TextAsset>("Script/Dialog Table - Sheet1");
        string[] lines = csvFile.text.Split('\n');

        for(int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = ParseCSVLine(lines[i]);

            DialogueLine line = new DialogueLine();

            line.textId = ParseIntOrDefault(values[0]);
            line.characterName = ParseStringOrDefault(values[1]);
            line.characterImage = ParseStringOrDefault(values[2]);
            line.text = ParseStringOrDefault(values[3]);
            line.nextId = ParseIntOrDefault(values[4]);
            line.specialActionId = ParseIntOrDefault(values[5]);
            line.documentSetId = ParseIntOrDefault(values[6]);

            line.option1 = ParseStringOrDefault(values[7]);
            line.option1NextId = ParseIntOrDefault(values[8]);
            line.option2 = ParseStringOrDefault(values[9]);
            line.option2NextId = ParseIntOrDefault(values[10]);
            line.option3 = ParseStringOrDefault(values[11]);
            line.option3NextId = ParseIntOrDefault(values[12]);
            line.option4 = ParseStringOrDefault(values[13]);
            line.option4NextId = ParseIntOrDefault(values[14]);
            line.option5 = ParseStringOrDefault(values[15]);
            line.option5NextId = ParseIntOrDefault(values[16]);


            dialogueDict.Add(line.textId, line);
        }
    }

    private int ParseIntOrDefault(string s)
    {
        s = s.Trim();
        if (string.IsNullOrEmpty(s))
            return -1;

        int result;
        if (int.TryParse(s, out result))
            return result;

        return -1; 
    }

    private string ParseStringOrDefault(string s)
    {
        s = s.Trim();
        if (string.IsNullOrEmpty(s))
            return "-1";
        return s;
    }


    private string[] ParseCSVLine(string line)
    {
        var matches = Regex.Matches(line,
            @"(?:^|,)(?:""(?<val>(?:[^""]|"""")*)""|(?<val>[^,""]*))");

        string[] values = new string[matches.Count];
        for (int i = 0; i < matches.Count; i++)
        {
            values[i] = matches[i].Groups["val"].Value.Replace("\"\"", "\"").Trim();
        }
        return values;
    }
}
