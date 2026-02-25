using UnityEngine;
using UnityEngine.UI;

public class DrebjuIzmeraMenedzeris : MonoBehaviour
{
    public static DrebjuIzmeraMenedzeris Instance;

    [Header("Slaideri")]
    public Slider platumsSlider;
    public Slider garumsSlider;

    private bool ignoreSliderChange = false; // Novērš bezgalīgos ciklus

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        platumsSlider.onValueChanged.AddListener(MainitPlatumu);
        garumsSlider.onValueChanged.AddListener(MainitGarumu);
    }

    // Šo izsauksim, kad atver jaunu kategoriju vai uzliek jaunu drēbi
    public void PielagotSlaiderusAktivajaiDrebei()
    {
        GameObject aktivaDrebe = IegutAktivoDrebi();

        if (aktivaDrebe != null)
        {
            ignoreSliderChange = true; // Īslaicīgi atslēdzam klausītājus
            RectTransform rect = aktivaDrebe.GetComponent<RectTransform>();
            platumsSlider.value = rect.sizeDelta.x;
            garumsSlider.value = rect.sizeDelta.y;
            ignoreSliderChange = false;

            // Padara slaiderus aktīvus, ja ir ko mainīt
            platumsSlider.interactable = true;
            garumsSlider.interactable = true;
        }
        else
        {
            // Ja drēbe nav uzlikta, atslēdzam slaiderus
            platumsSlider.interactable = false;
            garumsSlider.interactable = false;
        }
    }

    void MainitPlatumu(float vertiba)
    {
        if (ignoreSliderChange) return;

        GameObject aktivaDrebe = IegutAktivoDrebi();
        if (aktivaDrebe != null)
        {
            RectTransform rect = aktivaDrebe.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(vertiba, rect.sizeDelta.y);
        }
    }

    void MainitGarumu(float vertiba)
    {
        if (ignoreSliderChange) return;

        GameObject aktivaDrebe = IegutAktivoDrebi();
        if (aktivaDrebe != null)
        {
            RectTransform rect = aktivaDrebe.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, vertiba);
        }
    }

    private GameObject IegutAktivoDrebi()
    {
        // 1. Uzzina, kurš Toggle atvērts
        string aktivaKategorija = DrebjuKategorijuMenedzeris.Instance.IegutAktivoKategoriju();

        // 2. Paprasa Uzlikto drēbju menedžerim to drēbi
        return UzliktasDrebesMenedzeris.Instance.IegutUzliktoDrebi(aktivaKategorija);
    }
}