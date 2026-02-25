// DrebjuKategorijuPoga.cs
using UnityEngine;
using UnityEngine.UI;

public class DrebjuKategorijuPoga : MonoBehaviour
{
    [Header("SpongeBob drēbes")]
    public GameObject sb_Drebe1;
    public GameObject sb_Drebe2;
    public GameObject sb_Drebe3;

    [Header("Patrick drēbes")]
    public GameObject p_Drebe1;
    public GameObject p_Drebe2;
    public GameObject p_Drebe3;

    [Header("Kategorijas iestatījumi")]
    public string kategorijasNosaukums; // Šeit iekš Unity inspektora ieraksti, piemēram, "Bikses"

    private Toggle toggle;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleChanged);

        // Sākumā viss slēpts
        SleptVisas();
        toggle.isOn = false;
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn)
            DrebjuKategorijuMenedzeris.Instance.OnCategoryClicked(this);
        else
            SetOpen(false); // Šis tagad vienmēr slēpj kad toggle izslēdzas
    }

    void SleptVisas()
    {
        sb_Drebe1?.SetActive(false);
        sb_Drebe2?.SetActive(false);
        sb_Drebe3?.SetActive(false);
        p_Drebe1?.SetActive(false);
        p_Drebe2?.SetActive(false);
        p_Drebe3?.SetActive(false);
    }

    public void SetOpen(bool open)
    {
        toggle.onValueChanged.RemoveListener(OnToggleChanged);
        toggle.isOn = open;
        toggle.onValueChanged.AddListener(OnToggleChanged);

        if (!open)
        {
            SleptVisas();
            // Arī paziņo menedzerim ka šī ir aizvērta
            DrebjuKategorijuMenedzeris.Instance.OnSetClosed(this);
            return;
        }

        bool isPatrick = DrebjuKategorijuMenedzeris.Instance.IsPatrick();

        sb_Drebe1?.SetActive(!isPatrick);
        sb_Drebe2?.SetActive(!isPatrick);
        sb_Drebe3?.SetActive(!isPatrick);

        p_Drebe1?.SetActive(isPatrick);
        p_Drebe2?.SetActive(isPatrick);
        p_Drebe3?.SetActive(isPatrick);
    }

    public void RefreshCharacter()
    {
        if (toggle.isOn) SetOpen(true);
    }
}