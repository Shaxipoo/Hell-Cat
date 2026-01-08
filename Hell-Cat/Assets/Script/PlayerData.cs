using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class CatData
{
    public int currentChapter = 0;
    public bool isHeaven = false;
}
public static class PlayerData
{
    public static string playername = "Annie";

    public static Dictionary<int, CatData> chapterData = new Dictionary<int, CatData>();

    public static void AddData(int chapterId, CatData data)
    {
        chapterData[chapterId] = data;
    }

    public static CatData GetOrCreateData(int chapterId)
    {
        if (!chapterData.TryGetValue(chapterId, out var data))
        {
            data = new CatData();
            chapterData[chapterId] = data;
        }
        return data;
    }

    public static int GetCurrentChapter(int chapterId)
    {
        return GetOrCreateData(chapterId).currentChapter;
    }

    public static void SetCurrentChapter(int chapterId, int currentChapter)
    {
        GetOrCreateData(chapterId).currentChapter = currentChapter;
    }

    public static bool GetIsHeaven(int chapterId)
    {
        return GetOrCreateData(chapterId).isHeaven;
    }

    public static void SetIsHeaven(int chapterId, bool isHeaven)
    {
        GetOrCreateData(chapterId).isHeaven = isHeaven;
    }

    public static bool TryGetData(int chapterId, out CatData data)
    {
        return chapterData.TryGetValue(chapterId, out data);
    }

}



