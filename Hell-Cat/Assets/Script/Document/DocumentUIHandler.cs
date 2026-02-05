using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DocumentUIHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    [SerializeField] private RectTransform dragArea;
    private Graphic[] graphics;
    private bool[] originalRaycastStates;
    private Transform originalParent;
    private Transform homeParent; // parent to restore to when dragging out of a folder

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        graphics = GetComponentsInChildren<Graphic>(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dragArea = GameObject.Find("DragArea").GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        if (canvas != null)
            transform.SetParent(canvas.transform, true);
        transform.SetAsLastSibling();

        if (graphics != null && graphics.Length > 0)
        {
            originalRaycastStates = new bool[graphics.Length];
            for (int i = 0; i < graphics.Length; i++)
            {
                originalRaycastStates[i] = graphics[i].raycastTarget;
                graphics[i].raycastTarget = false;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        ClampToArea();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (graphics != null && originalRaycastStates != null)
        {
            for (int i = 0; i < graphics.Length && i < originalRaycastStates.Length; i++)
                graphics[i].raycastTarget = originalRaycastStates[i];
        }

        // If the dragged item was not reparented by a drop target, restore original parent
        if (canvas != null && originalParent != null && transform.parent == canvas.transform)
        {
            // If the item originally came from a folder and we recorded a homeParent (where it lived before being put into the folder),
            // restore to homeParent when the item was dragged out of the folder.
            if (originalParent.GetComponent<FolderDropHandler>() != null && homeParent != null)
            {
                transform.SetParent(homeParent, true);
            }
            else
            {
                transform.SetParent(originalParent, true);
            }
        }
    }

    // Called by FolderDropHandler when the item is placed into a folder to remember where it came from
    public void SetHomeParent(Transform parent)
    {
        homeParent = parent;
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
