using UnityEngine;
using UnityEngine.EventSystems;

public class DrebjuDragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public string kategorija;

    private GameObject aktivaKopija;
    private RectTransform kopijasRect;
    private Canvas canvas;

    public void OnPointerDown(PointerEventData eventData)
    {
        eventData.Use(); // Bloķē Toggle

        canvas = GetComponentInParent<Canvas>();

        // Izveido kopiju uzreiz
        aktivaKopija = UzliktasDrebesMenedzeris.Instance.IzveidotKopiju(this);
        kopijasRect = aktivaKopija.GetComponent<RectTransform>();

        // Novieto kopiju zem peles uzreiz
        ParvietotUzPeli(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (kopijasRect == null) return;
        eventData.Use();
        ParvietotUzPeli(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Kopija paliek tur kur nomesta
        aktivaKopija = null;
        kopijasRect = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        eventData.Use(); // Bloķē Toggle klikšķi
    }

    private void ParvietotUzPeli(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPoint
        );
        kopijasRect.anchoredPosition = localPoint;
    }
}