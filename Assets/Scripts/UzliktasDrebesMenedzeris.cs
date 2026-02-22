using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UzliktasDrebesMenedzeris : MonoBehaviour
{
    public static UzliktasDrebesMenedzeris Instance;

    public Canvas canvas;

    private Dictionary<string, GameObject> uzliktasDrebes = new Dictionary<string, GameObject>();

    void Awake()
    {
        Instance = this;
    }

    public GameObject IzveidotKopiju(DrebjuDragAndDrop source)
    {
        string kat = source.kategorija;

        // Izdzēš iepriekšējo no šīs kategorijas
        if (uzliktasDrebes.ContainsKey(kat) && uzliktasDrebes[kat] != null)
        {
            Destroy(uzliktasDrebes[kat]);
            uzliktasDrebes.Remove(kat);
        }

        // Izveido kopiju uz canvas
        GameObject kopija = Instantiate(source.gameObject, canvas.transform);

        // Noņem DrebjuDragAndDrop no kopijas — tā nedragojas pati
        Destroy(kopija.GetComponent<DrebjuDragAndDrop>());

        // Pievieno IzveletaDrebjuKopija tikai kā markeri (nav vajadzīgs IDrag vairs)
        IzveletaDrebjuKopija k = kopija.AddComponent<IzveletaDrebjuKopija>();
        k.kategorija = kat;

        kopija.transform.SetAsLastSibling();
        uzliktasDrebes[kat] = kopija;

        return kopija;
    }
}