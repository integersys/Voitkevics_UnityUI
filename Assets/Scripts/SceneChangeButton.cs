using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneChangeButton : MonoBehaviour
{
    [Tooltip("Ieraksti scēnas nosaukumu, uz kuru šai pogai jāaizved")]
    public string sceneToLoad;

    void Start()
    {
        Button btn = GetComponent<Button>();

        btn.onClick.AddListener(() =>
        {
            if (SceneLoader.Instance != null)
            {
                SceneLoader.Instance.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogError("SceneLoader nav atrasts spēlē!");
            }
        });
    }
}