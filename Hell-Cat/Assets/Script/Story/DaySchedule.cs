using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DaySchedule
{
    //Day ID
    public int dayId;
    //Mails (Documents)

    public List<Mail> mails;

    //Case List
    public List<CatChapter> chapterList;

    public int daySuggestNumber;
    
    //End Sentense


}
