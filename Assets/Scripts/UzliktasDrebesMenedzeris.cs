using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UzliktasDrebesMenedzeris : MonoBehaviour
{
    public static UzliktasDrebesMenedzeris Instance;

    public Canvas canvas;

    private Dictionary<string, GameObject> uzliktasDrebes = new Dictionary<string, GameObject>();


    public void NotirtVisiDrebes()
    {
        foreach (var kvp in uzliktasDrebes)
        {
            if (kvp.Value != null)
                Destroy(kvp.Value);
        }
        uzliktasDrebes.Clear();
    }

    public GameObject IegutUzliktoDrebi(string kategorija)
    {
        if (string.IsNullOrEmpty(kategorija)) return null;

        if (uzliktasDrebes.TryGetValue(kategorija, out GameObject drebe))
        {
            return drebe;
        }
        return null;
    }
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

        if (DrebjuIzmeraMenedzeris.Instance != null)
            DrebjuIzmeraMenedzeris.Instance.PielagotSlaiderusAktivajaiDrebei();

        return kopija;
    }
}