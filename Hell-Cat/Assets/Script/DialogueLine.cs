using System.Collections.Generic;
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

    public string option1;
    public int option1NextId;

    public string option2;
    public int option2NextId;

    public string option3;
    public int option3NextId;
}

public class DialogueLoader : MonoBehaviour
{
    public Dictionary<int, DialogueLine> dialogueDict;

    private void Start()
    {
        LoadCSV();
        Debug.Log(dialogueDict[1].characterName + "£º" + dialogueDict[1].text);
    }

    private void LoadCSV()
    {
        dialogueDict = new Dictionary<int, DialogueLine>();
        TextAsset csvFile = Resources.Load<TextAsset>("Dialogues/dialogues");
        string[] lines = csvFile.text.Split('\n');

        for(int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(',');

            DialogueLine line = new DialogueLine();
            line.textId = int.Parse(values[0]);
            line.characterName = values[1];
            line.characterImage = values[2];
            line.text = values[3];
            line.nextId = int.Parse(values[4]);
            line.specialActionId = int.Parse(values[5]);

            line.option1 = values[6];
            line.option1NextId = int.Parse(values[7]);
            line.option2 = values[8];
            line.option2NextId = int.Parse(values[9]);
            line.option3 = values[10];
            line.option3NextId = int.Parse(values[11]);

            dialogueDict.Add(line.textId, line);
        }
    }

}
