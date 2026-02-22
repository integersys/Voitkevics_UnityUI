using UnityEngine;
using UnityEngine.UI;

public class TelaIzmers : MonoBehaviour
{
    [Header("Spongebob tēls")]
    public RectTransform spongebobRect;

    [Header("Slideri")]
    public Slider garumsSlider;
    public Slider platumsSlider;

    private Vector2 sakumaIzmers;

    void Start()
    {
        sakumaIzmers = spongebobRect.sizeDelta;

        garumsSlider.onValueChanged.AddListener(MainitGarumu);
        platumsSlider.onValueChanged.AddListener(MainitPlatumu);
    }

    void MainitGarumu(float vertiba)
    {
        spongebobRect.sizeDelta = new Vector2(spongebobRect.sizeDelta.x, vertiba);
    }

    void MainitPlatumu(float vertiba)
    {
        spongebobRect.sizeDelta = new Vector2(vertiba, spongebobRect.sizeDelta.y);
    }
}