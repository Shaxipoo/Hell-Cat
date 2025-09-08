using UnityEngine;
using UnityEngine.EventSystems;

public class DocumentUIHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    [SerializeField] private RectTransform dragArea;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dragArea = GameObject.Find("DragArea").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        ClampToArea();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    private void ClampToArea()
    {
        if (dragArea == null) return;

        Vector3[] areaCorners = new Vector3[4];
        Vector3[] objCorners = new Vector3[4];

        dragArea.GetWorldCorners(areaCorners);
        rectTransform.GetWorldCorners(objCorners);

        Vector3 pos = rectTransform.position;

        if (objCorners[0].x < areaCorners[0].x)
            pos.x += areaCorners[0].x - objCorners[0].x;
        if (objCorners[2].x > areaCorners[2].x)
            pos.x -= objCorners[2].x - areaCorners[2].x;
        if (objCorners[0].y < areaCorners[0].y)
            pos.y += areaCorners[0].y - objCorners[0].y;
        if (objCorners[2].y > areaCorners[2].y)
            pos.y -= objCorners[2].y - areaCorners[2].y;

        rectTransform.position = pos;
    }


}
