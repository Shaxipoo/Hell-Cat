using UnityEngine;
using UnityEngine.EventSystems;

public sealed class UIButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum HoverEffect { PopOut, Slide }
    public enum MoveDirection { Up, Down, Left, Right }

    [Header("Effect Mode")]
    [SerializeField] private HoverEffect effect = HoverEffect.PopOut;

    [Header("Pop Out Settings")]
    [SerializeField, Min(1f)] private float hoverScale = 1.1f;   // scale when hovered

    [Header("Slide Settings")]
    [SerializeField] private MoveDirection moveDirection = MoveDirection.Up;
    [SerializeField, Min(0f)] private float movePixels = 20f;    // UI pixels to slide on hover

    [Header("General")]
    [SerializeField, Min(0.01f)] private float lerpSpeed = 10f;  // higher = faster
    [SerializeField] private bool ignoreTimeScale = true;

    Vector3 baseScale;
    Vector3 targetScale;

    RectTransform rect;
    Vector2 basePos;
    Vector2 targetPos;

    void Awake()
    {
        rect = GetComponent<RectTransform>();

        baseScale = transform.localScale;
        targetScale = baseScale;

        if (rect)
        {
            basePos = rect.anchoredPosition;
            targetPos = basePos;
        }
    }

    void OnEnable()
    {
        // reset on enable
        transform.localScale = baseScale;
        targetScale = baseScale;

        if (rect)
        {
            rect.anchoredPosition = basePos;
            targetPos = basePos;
        }
    }

    void Update()
    {
        float dt = ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
        float step = 1f - Mathf.Exp(-lerpSpeed * dt);

        // scale
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, step);

        // position
        if (rect)
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, targetPos, step);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (effect == HoverEffect.PopOut)
        {
            targetScale = baseScale * hoverScale;
            if (rect) targetPos = basePos;
        }
        else // Slide
        {
            targetScale = baseScale;
            if (rect) targetPos = basePos + GetOffset(moveDirection, movePixels);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = baseScale;
        if (rect) targetPos = basePos;
    }

    static Vector2 GetOffset(MoveDirection dir, float dist)
    {
        switch (dir)
        {
            case MoveDirection.Up: return new Vector2(0f, dist);
            case MoveDirection.Down: return new Vector2(0f, -dist);
            case MoveDirection.Left: return new Vector2(-dist, 0f);
            case MoveDirection.Right: return new Vector2(dist, 0f);
            default: return Vector2.zero;
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (hoverScale < 1f) hoverScale = 1f;
        if (lerpSpeed < 0.01f) lerpSpeed = 0.01f;
        if (movePixels < 0f) movePixels = 0f;
    }
#endif
}
