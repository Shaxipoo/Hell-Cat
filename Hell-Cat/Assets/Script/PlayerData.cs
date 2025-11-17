using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string playername;
    public List<FileInfo> fileList;
}

public static class PlayerData
{
    public static string playername = "Annie";



}
