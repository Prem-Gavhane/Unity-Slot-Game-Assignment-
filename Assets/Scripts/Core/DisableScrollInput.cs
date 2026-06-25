using UnityEngine;
using UnityEngine.EventSystems;

public class DisableDrag : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IScrollHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        eventData.Use();
    }

    public void OnDrag(PointerEventData eventData)
    {
        eventData.Use();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        eventData.Use();
    }

    public void OnScroll(PointerEventData eventData)
    {
        eventData.Use();
    }
}