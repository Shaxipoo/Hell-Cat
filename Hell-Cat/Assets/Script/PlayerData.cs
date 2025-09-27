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

    public static Dictionary<int, FileInfo> fileList = new Dictionary<int, FileInfo>();

    private static string savePath = Application.persistentDataPath + "/save.json";

    // 保存
    public static void Save()
    {
        SaveData data = new SaveData();
        data.playername = playername;
        data.fileList = new List<FileInfo>(fileList.Values);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("存档成功: " + savePath);
    }

    // 读取
    public static void Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("没有存档文件");
            return;
        }

        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        playername = data.playername;
        fileList.Clear();
        foreach (var file in data.fileList)
        {
            fileList[file.FileId] = file;
        }

        Debug.Log("读取存档成功");
    }

}
