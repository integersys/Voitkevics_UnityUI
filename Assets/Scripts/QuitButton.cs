using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class QuitButton : MonoBehaviour
{
    void Start()
    {
        // Automātiski atrod pogu, uz kuras šis skripts ir uzlikts
        Button btn = GetComponent<Button>();

        btn.onClick.AddListener(() =>
        {
            // Izsauc mūsu galveno, neiznīcināmo SceneLoader
            if (SceneLoader.Instance != null)
            {
                SceneLoader.Instance.QuitGame();
            }
            else
            {
                Debug.LogError("QuitButton: SceneLoader nav atrasts! Pārliecinies, ka spēle sākta no scēnas, kurā tas atrodas.");
            }
        });
    }
}