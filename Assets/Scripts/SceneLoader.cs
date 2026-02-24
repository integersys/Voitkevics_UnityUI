using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("SceneLoader: Scene nosaukums ir tukšs!");
            return;
        }

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.Log($"SceneLoader: Lādē scenu '{sceneName}'...");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"SceneLoader: Scene '{sceneName}' nav atrasta! " +
                           $"Pārliecinies, ka tā ir pievienota Build Settings.");
        }
    }

    public void LoadSceneAsync(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("SceneLoader: Scene nosaukums ir tukšs!");
            return;
        }

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            StartCoroutine(LoadAsync(sceneName));
        }
        else
        {
            Debug.LogError($"SceneLoader: Scene '{sceneName}' nav atrasta!");
        }
    }

    private System.Collections.IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log($"Lādēšana: {progress * 100f:0}%");
            yield return null;
        }
    }

    public void ReloadCurrentScene()
    {
        string current = SceneManager.GetActiveScene().name;
        LoadScene(current);
    }

    public void LoadNextScene()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIndex);
        else
            Debug.LogWarning("SceneLoader: Nav nākamās scenes!");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("SceneLoader: Iziet no spēles.");
    }
}