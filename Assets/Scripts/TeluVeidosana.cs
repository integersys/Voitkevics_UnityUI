using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeluVeidosana : MonoBehaviour
{
    [Header("Input Lauki")]
    public TMP_InputField vardsInputField;
    public TMP_InputField gadsInputField;

    [Header("Izvades teksts")]
    public TextMeshProUGUI rezultatsTeksts;

    [Header("Dropdown")]
    public TMP_Dropdown characterDropdown; // Ievelc IzveletiesTelu dropdown

    private const int MIN_GADS = 1926;

    void Start()
    {
        vardsInputField.characterLimit = 15;
        vardsInputField.onValueChanged.AddListener(FiltretVardu);
        gadsInputField.characterLimit = 4;
        gadsInputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        gadsInputField.ForceLabelUpdate();
        rezultatsTeksts.text = "";
    }

    void FiltretVardu(string ievade)
    {
        string tikai_burti = "";
        foreach (char c in ievade)
        {
            if (char.IsLetter(c))
                tikai_burti += c;
        }
        if (tikai_burti != ievade)
        {
            vardsInputField.text = tikai_burti;
            vardsInputField.caretPosition = tikai_burti.Length;
        }
    }

    public void UzSpiestPogu()
    {
        string vards = vardsInputField.text;
        string gadsText = gadsInputField.text;

        if (string.IsNullOrEmpty(vards) || string.IsNullOrEmpty(gadsText))
        {
            rezultatsTeksts.text = "Lūdzu ievadiet vārdu un dzimšanas gadu!";
            return;
        }

        if (!int.TryParse(gadsText, out int dzimsanasGads))
        {
            rezultatsTeksts.text = "Dzimšanas gadam jābūt skaitlim!";
            return;
        }

        if (dzimsanasGads < MIN_GADS)
        {
            rezultatsTeksts.text = $"Dzimšanas gads nevar būt mazāks par {MIN_GADS}!";
            return;
        }

        if (dzimsanasGads > System.DateTime.Now.Year)
        {
            rezultatsTeksts.text = "Dzimšanas gads nevar būt nākotnē!";
            return;
        }

        int vecums = System.DateTime.Now.Year - dzimsanasGads;

        bool isPatrick = characterDropdown.value == 1;
        string tituls = isPatrick ? "Zvaigzne" : "Kvadrātbiksis";

        rezultatsTeksts.text = $"{tituls} {vards} ir {vecums} gadus vecs!";
    }
}