using UnityEngine;
using UnityEngine.EventSystems;

public class DisableScrollInput : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IScrollHandler
{
    public void OnBeginDrag(PointerEventData eventData) { }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData) { }

    public void OnScroll(PointerEventData eventData) { }
}