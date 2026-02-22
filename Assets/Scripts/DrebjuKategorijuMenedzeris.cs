// DrebjuKategorijuMenedzeris.cs
using UnityEngine;
using TMPro;

public class DrebjuKategorijuMenedzeris : MonoBehaviour
{
    public static DrebjuKategorijuMenedzeris Instance;

    [Header("Dropdown")]
    public TMP_Dropdown characterDropdown;

    private DrebjuKategorijuPoga currentActive = null;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        characterDropdown.onValueChanged.AddListener(OnCharacterChanged);
    }

    public bool IsPatrick()
    {
        return characterDropdown.value == 1;
    }

    void OnCharacterChanged(int index)
    {
        // Ja kāda kategorija ir atvērta, atjauno drēbes pēc jaunā tēla
        if (currentActive != null)
            currentActive.RefreshCharacter();
    }

    public void OnSetClosed(DrebjuKategorijuPoga closed)
    {
        if (currentActive == closed)
            currentActive = null;
    }

    public void OnCategoryClicked(DrebjuKategorijuPoga clicked)
    {
        if (currentActive == clicked)
        {
            // Ja uzspiež uz jau atvērto — aizver
            clicked.SetOpen(false);
            currentActive = null;
        }
        else
        {
            // Aizver iepriekšējo, atver jauno
            if (currentActive != null)
                currentActive.SetOpen(false);

            clicked.SetOpen(true);
            currentActive = clicked;
        }
    }
}