using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IzveletaDrebjuKopija : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public string kategorija;
    private Canvas canvas;
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        GetComponent<Image>().raycastTarget = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling(); // Vienmēr virs pārējā
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPoint
        );
        rectTransform.anchoredPosition = localPoint;
    }
}