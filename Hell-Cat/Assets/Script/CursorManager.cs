using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    public Texture2D cursorTexture_Normal;
    public Texture2D cursorTexture_Highlight;
    public Texture2D cursorTexture_White;
    public Texture2D cursorTexture_Green;
    public CursorMode cursorMode = CursorMode.Auto;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        ChangeCursorToNormal();
    }

    public void ChangeCursor(Texture2D t)
    {
        if (t == null) return;

        Vector2 hotSpot = new Vector2(t.width / 2, t.height / 2);
        Cursor.SetCursor(t, hotSpot, cursorMode);
    }

    public void ChangeCursorToNormal()
    {
        ChangeCursor(cursorTexture_Normal);
    }

    public void ChangeCursorToWhite()
    {
        ChangeCursor(cursorTexture_White);
    }

    public void ChangeCursorToGreen()
    {
        ChangeCursor(cursorTexture_Green);
    }

    public void ChangeCursorToHighlight()
    {
        ChangeCursor(cursorTexture_Highlight);
    }
}