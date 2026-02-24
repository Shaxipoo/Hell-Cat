using UnityEngine;
using UnityEngine.EventSystems;

public class FolderDropHandler : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag;
        if (dragged == null) return;
        var draggedRect = dragged.GetComponent<RectTransform>();
        var folderRect = GetComponent<RectTransform>();
        if (draggedRect == null || folderRect == null) return;

        // Only reparent if the dragged item's four corners are fully inside the folder
        Vector3[] worldCorners = new Vector3[4];
        draggedRect.GetWorldCorners(worldCorners);
        bool fullyInside = true;
        for (int i = 0; i < 4; i++)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, worldCorners[i]);
            if (!RectTransformUtility.RectangleContainsScreenPoint(folderRect, screenPoint, eventData.pressEventCamera))
            {
                fullyInside = false;
                break;
            }
        }

        if (!fullyInside) return;

        // Remember where the dragged item came from (so it can be restored when dragged out)
        var docHandler = dragged.GetComponent<DocumentUIHandler>();
        if (docHandler != null)
        {
            docHandler.SetHomeParent(draggedRect.parent);
        }

        // Reparent to folder
        draggedRect.SetParent(folderRect, true);

        // Position inside folder at pointer
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(folderRect, eventData.position, eventData.pressEventCamera, out localPoint);
        draggedRect.anchoredPosition = localPoint;

        draggedRect.SetAsLastSibling();
    }
}
