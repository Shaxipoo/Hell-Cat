using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Chat Phase")]
    [Tooltip("其他猫的chat")]
    public List<TextAsset> ranChatList;

    [Tooltip("主线猫")]
    public List<TextAsset> mainCatChatList;


}
