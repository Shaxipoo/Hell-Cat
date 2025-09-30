using UnityEngine;
using UnityEngine.EventSystems;

public sealed class UIButtonSlideOut : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum SlideDirection { Up, Down, Left, Right }

    [Header("Slide Settings")]
    [SerializeField] private SlideDirection direction = SlideDirection.Up;
    [SerializeField, Min(0f)] private float slideDistance = 20f;     // UI pixels
    [SerializeField, Min(0.01f)] private float lerpSpeed = 12f;      // higher = faster
    [SerializeField] private bool useUnscaledTime = true;

    private RectTransform rect;
    private Vector2 basePos;
    private Vector2 targetPos;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        if (rect)
        {
            basePos = rect.anchoredPosition;
            targetPos = basePos;
        }
    }

    void OnEnable()
    {
        if (!rect) return;
        rect.anchoredPosition = basePos;
        targetPos = basePos;
    }

    void Update()
    {
        if (!rect) return;
        float dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        float step = 1f - Mathf.Exp(-lerpSpeed * dt);
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, targetPos, step);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!rect) return;
        targetPos = basePos + GetOffset(direction, slideDistance);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!rect) return;
        targetPos = basePos;
    }

    private static Vector2 GetOffset(SlideDirection dir, float dist)
    {
        switch (dir)
        {
            case SlideDirection.Up: return new Vector2(0f, dist);
            case SlideDirection.Down: return new Vector2(0f, -dist);
            case SlideDirection.Left: return new Vector2(-dist, 0f);
            case SlideDirection.Right: return new Vector2(dist, 0f);
            default: return Vector2.zero;
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (slideDistance < 0f) slideDistance = 0f;
        if (lerpSpeed < 0.01f) lerpSpeed = 0.01f;
    }
#endif
}
