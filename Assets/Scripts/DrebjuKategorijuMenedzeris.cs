using UnityEngine;

public class DrebjuKategorijuMenedzeris : MonoBehaviour
{
    public static DrebjuKategorijuMenedzeris Instance;

    private DrebjuKategorijuPoga currentActive = null; // <- Tagad pareizais tips

    void Awake()
    {
        Instance = this;
    }

    public void AizverstVisas()
    {
        if (currentActive != null)
        {
            currentActive.SetOpen(false);
            currentActive = null;
        }
    }

    public void OnCategoryClicked(DrebjuKategorijuPoga clicked)
    {
        if (currentActive == clicked)
        {
            clicked.SetOpen(false);
            currentActive = null;
        }
        else
        {
            if (currentActive != null)
                currentActive.SetOpen(false);

            clicked.SetOpen(true);
            currentActive = clicked;
        }
    }
}