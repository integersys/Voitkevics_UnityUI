using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IkonuMaina : MonoBehaviour
{
    [Header("Dropdown (Sūklis / Patriks)")]
    public TMP_Dropdown characterDropdown;

    [Header("Augšas 4 ikonas (GameObjects no hierarhijas)")]
    public GameObject ikona_Krekli;
    public GameObject ikona_Bikses;
    public GameObject ikona_Cepures;
    public GameObject ikona_Kurpes;

    [Header("SpongeBob Sprites")]
    public Sprite sb_Krekli;
    public Sprite sb_Bikses;
    public Sprite sb_Cepures;
    public Sprite sb_Kurpes;

    [Header("Patrick Sprites")]
    public Sprite p_Krekli;
    public Sprite p_Bikses;
    public Sprite p_Cepures;
    public Sprite p_Kurpes;

    [Header("Galvenais Tēls")]
    public GameObject galvenaisTels;

    public Sprite sb_GalvenaisTels;
    public Sprite p_GalvenaisTels;

    [Header("Scroll View Teksts")]
    public TMP_Text scrollTeksts;

    [Header("Background")]
    public GameObject background;
    public Sprite sb_Background;
    public Sprite p_Background;

    [Header("Closet")]
    public GameObject closet;
    public Sprite sb_Closet;
    public Sprite p_Closet;

    [Header("Poga")]
    public GameObject poga;
    public Sprite sb_Poga;
    public Sprite p_Poga;

    [Header("Kategoriju Teksti")]
    public TMP_Text krekliTeksts;

    [Header("")]
    public string sb_KrekliTeksts = "Amuleti";
    public string p_KrekliTeksts = "Krekli";

    [TextArea(5, 10)]
    public string spongeBobTeksts;

    [TextArea(5, 10)]
    public string patriksTeksts;

    void Start()
    {
        characterDropdown.onValueChanged.AddListener(MainitIkonas);
        MainitIkonas(characterDropdown.value);
    }

    void MainitIkonas(int index)
    {
        bool isPatrick = index == 1;

        UzliktasDrebesMenedzeris.Instance.NotirtVisiDrebes();

        NomainitSprite(ikona_Krekli, isPatrick ? p_Krekli : sb_Krekli);
        NomainitSprite(ikona_Bikses, isPatrick ? p_Bikses : sb_Bikses);
        NomainitSprite(ikona_Cepures, isPatrick ? p_Cepures : sb_Cepures);
        NomainitSprite(ikona_Kurpes, isPatrick ? p_Kurpes : sb_Kurpes);
        NomainitSprite(galvenaisTels, isPatrick ? p_GalvenaisTels : sb_GalvenaisTels);
        NomainitSprite(background, isPatrick ? p_Background : sb_Background);
        NomainitSprite(closet, isPatrick ? p_Closet : sb_Closet);
        NomainitSprite(poga, isPatrick ? p_Poga : sb_Poga);

        if (krekliTeksts != null)
            krekliTeksts.text = isPatrick ? p_KrekliTeksts : sb_KrekliTeksts;

        if (scrollTeksts != null)
            scrollTeksts.text = isPatrick ? patriksTeksts : spongeBobTeksts;
    }

    void NomainitSprite(GameObject obj, Sprite jaunaisSprite)
    {
        Image img = obj.GetComponent<Image>();
        if (img == null)
            img = obj.GetComponentInChildren<Image>();

        if (img != null)
        {
            // Unity pats saglabās objekta esošo izmēru un pozīciju.
            // Atliek tikai iedot jauno bildi.
            img.sprite = jaunaisSprite;
        }
        else
        {
            Debug.LogError("Image komponents nav atrasts: " + obj.name);
        }
    }
}