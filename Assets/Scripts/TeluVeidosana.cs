using UnityEngine;
using TMPro;

public class TeluVeidosana : MonoBehaviour
{
    [Header("Input Lauki")]
    public TMP_InputField vardsInputField;
    public TMP_InputField gadsInputField;

    [Header("Izvades teksts")]
    public TextMeshProUGUI rezultatsTeksts;

    private const int MAX_BURTI = 15;
    private const int MIN_GADS = 1926;

    void Start()
    {
        // Automātiski atjaunina tekstu, mainoties ievadei
        vardsInputField.characterLimit = MAX_BURTI;
        vardsInputField.onValueChanged.AddListener(_ => Atjauninat());
        gadsInputField.onValueChanged.AddListener(_ => Atjauninat());

        rezultatsTeksts.text = "";
    }

    void Atjauninat()
    {
        string vards = vardsInputField.text;
        string gadsText = gadsInputField.text;

        if (string.IsNullOrEmpty(vards) || string.IsNullOrEmpty(gadsText))
        {
            rezultatsTeksts.text = "";
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
        rezultatsTeksts.text = $"Kvadrātbiksis {vards} ir {vecums} gadus vecs!";
    }
}