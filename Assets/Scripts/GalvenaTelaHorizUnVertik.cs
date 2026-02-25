using UnityEngine;
using UnityEngine.UI;

public class TelaIzmers : MonoBehaviour
{
    public static TelaIzmers Instance; 

    [Header("Tēla attēls")]
    public RectTransform telsRect; 

    [Header("Slideri")]
    public Slider garumsSlider;
    public Slider platumsSlider;

    private Vector2 sakumaIzmers;
    private bool ignoreSliderChange = false; 

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        sakumaIzmers = telsRect.sizeDelta;

        PielagotSlaiderus();


        garumsSlider.onValueChanged.AddListener(MainitGarumu);
        platumsSlider.onValueChanged.AddListener(MainitPlatumu);
    }

    public void PielagotSlaiderus()
    {
        if (telsRect != null)
        {
            ignoreSliderChange = true; 

            platumsSlider.value = telsRect.sizeDelta.x;
            garumsSlider.value = telsRect.sizeDelta.y;

            ignoreSliderChange = false;
        }
    }

    void MainitGarumu(float vertiba)
    {
        if (ignoreSliderChange) return;
        telsRect.sizeDelta = new Vector2(telsRect.sizeDelta.x, vertiba);
    }

    void MainitPlatumu(float vertiba)
    {
        if (ignoreSliderChange) return;
        telsRect.sizeDelta = new Vector2(vertiba, telsRect.sizeDelta.y);
    }
}