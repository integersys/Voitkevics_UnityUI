using UnityEngine;
using UnityEngine.UI;

public class DrebjuKategorijuPoga : MonoBehaviour
{
    [Header("Šīs kategorijas drēbes")]
    public GameObject[] clothingItems;

    private Toggle toggle;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleChanged);
        SetOpen(false);
        toggle.isOn = false;
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn)
            DrebjuKategorijuMenedzeris.Instance.OnCategoryClicked(this);
        else
            SetOpen(false);
    }

    public void SetOpen(bool open)
    {
        // Mainām toggle vizuāli bez eventa triggerēšanas
        toggle.onValueChanged.RemoveListener(OnToggleChanged);
        toggle.isOn = open;
        toggle.onValueChanged.AddListener(OnToggleChanged);

        foreach (var item in clothingItems)
        {
            if (item != null)
                item.SetActive(open);
        }
    }
}