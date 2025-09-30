using UnityEngine;
using UnityEngine.EventSystems;

public sealed class UIButtonPopOut : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Pop Out Settings")]
    [SerializeField, Min(1f)] private float hoverScale = 1.1f;     // how large when hovered
    [SerializeField, Min(0.01f)] private float lerpSpeed = 12f;    // how fast to scale
    [SerializeField] private bool useUnscaledTime = true;

    private Vector3 baseScale;
    private Vector3 targetScale;

    void Awake()
    {
        baseScale = transform.localScale;
        targetScale = baseScale;
    }

    void OnEnable()
    {
        transform.localScale = baseScale;
        targetScale = baseScale;
    }

    void Update()
    {
        float dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        float step = 1f - Mathf.Exp(-lerpSpeed * dt);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, step);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = baseScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = baseScale;
    }
}
