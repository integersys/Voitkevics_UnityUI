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
    public GameObject galvenaisTels; // Ievelc "GalvenaisTels" no hierarhijas

    public Sprite sb_GalvenaisTels; // SpongeBob sprite
    public Sprite p_GalvenaisTels;  // Patrick sprite


    [Header("Scroll View Teksts")]
    public TMP_Text scrollTeksts; // Ievelc "Text (TMP)" no InfoParTelu → Viewport → Content

    [Header("Background")]
    public GameObject background;
    public Sprite sb_Background;
    public Sprite p_Background;

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

        NomainitSprite(ikona_Krekli, isPatrick ? p_Krekli : sb_Krekli);
        NomainitSprite(ikona_Bikses, isPatrick ? p_Bikses : sb_Bikses);
        NomainitSprite(ikona_Cepures, isPatrick ? p_Cepures : sb_Cepures);
        NomainitSprite(ikona_Kurpes, isPatrick ? p_Kurpes : sb_Kurpes);
        NomainitSprite(galvenaisTels, isPatrick ? p_GalvenaisTels : sb_GalvenaisTels);
        NomainitSprite(background, isPatrick ? p_Background : sb_Background);

        if (scrollTeksts != null)
            scrollTeksts.text = isPatrick ? patriksTeksts : spongeBobTeksts;
    }


    void NomainitSprite(GameObject obj, Sprite jaunaisSprite)
    {
        Image img = obj.GetComponent<Image>();
        if (img == null)
            img = obj.GetComponentInChildren<Image>();

        if (img != null)
            img.sprite = jaunaisSprite;
        else
            Debug.LogError("Image komponents nav atrasts: " + obj.name);
    }
}