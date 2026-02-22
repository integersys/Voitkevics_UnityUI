using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BubbleTransition : MonoBehaviour
{
    public static BubbleTransition Instance;

    [Header("Transition Settings")]
    public Image bubbleOverlay;       // Melnais UI Image
    public float transitionDuration = 0.6f;

    // Bubble "wipe" efektam izmantojam shader vai vienkārši scale triku
    // Šeit izmantojam RectTransform scale pieeju
    private RectTransform overlayRect;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Paliek starp scenēm
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        overlayRect = bubbleOverlay.GetComponent<RectTransform>();
        bubbleOverlay.gameObject.SetActive(false);
    }

    // Izsauc šo, lai pāriet uz citu scenu
    public void GoToScene(string sceneName)
    {
        StartCoroutine(TransitionCoroutine(sceneName));
    }

    private IEnumerator TransitionCoroutine(string sceneName)
    {
        // --- FADE IN (burbulis aizpilda ekrānu) ---
        bubbleOverlay.gameObject.SetActive(true);
        overlayRect.localScale = Vector3.zero;

        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionDuration;

            // EaseInOut — dabīgāk izskatās
            float scale = EaseInOut(t) * 3f; // 3x lai noteikti aizpildās
            overlayRect.localScale = new Vector3(scale, scale, 1f);

            yield return null;
        }

        overlayRect.localScale = Vector3.one * 3f;

        // --- Ielādē scenu ---
        yield return SceneManager.LoadSceneAsync(sceneName);

        // --- FADE OUT (burbulis pazūd jaunajā scenē) ---
        elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionDuration;

            float scale = (1f - EaseInOut(t)) * 3f;
            overlayRect.localScale = new Vector3(scale, scale, 1f);

            yield return null;
        }

        bubbleOverlay.gameObject.SetActive(false);
    }

    // Smooth easing funkcija
    private float EaseInOut(float t)
    {
        return t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;
    }
}