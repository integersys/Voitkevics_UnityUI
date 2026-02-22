using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "JaunsTels", menuName = "DressUp/Tela Konfiguracija")]
public class TeluIzmaina : ScriptableObject
{
    [Header("Vispārīgi")]
    public string telaNosaukums;

    [Header("Attēli")]
    public Sprite skapis;           // Skapja bilde
    public Sprite fons;             // Background bilde
    public Sprite iesniegtsPogas;   // "Iesniegt" pogas bilde
    public Sprite telaSprite;       // Tēla attēls

    [Header("Kategoriju ikonu sprites (Krekli, Bikses, Cepures, Kurpes)")]
    public Sprite krekladIkona;
    public Sprite biksesIkona;
    public Sprite cepuresIkona;
    public Sprite kurpesIkona;

    [Header("Scroll View teksts")]
    [TextArea(3, 6)]
    public string apraksts;

    [Header("Drēbju prefabi šim tēlam")]
    public GameObject[] krekladPrefabi;
    public GameObject[] biksesPrefabi;
    public GameObject[] cepuresPrefabi;
    public GameObject[] kurpesPrefabi;
}